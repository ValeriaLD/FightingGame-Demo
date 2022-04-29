using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private bool groundedPlayer, canMove = true;
    private float horizontalInput, verticalInput;
    private Vector3 moveDirection;
    private Vector3 playerVelocity;

    [SerializeField]
    public GameObject weaponInHands = null;

    [SerializeField]
    private float moveSpeed = 2.0f, walkSpeed = 5f, runSpeed = 20f, weaponDamage;
    
    [SerializeField]
    private float jumpHeight = 1f, gravity = -9.81f;

    [SerializeField]
    [Range(0f, 1f)]
    private float rotateSpeed = 0.05f;

    [SerializeField]
    private Text diedText;

    public float health = 100f;
    public bool blockAttack = false, isPlayed = false;

    private CharacterController player;
    private Animator playerAnimator;

    private void Awake()
    {
       if(instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
        diedText.gameObject.SetActive(false);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (canMove)
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!isPlayed)
                {
                    Attack();
                }
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                playerAnimator.SetBool("isBlocking", true);
                blockAttack = true;
            }
            else
            {
                playerAnimator.SetBool("isBlocking", false);
                blockAttack = false;
            }
        }

        if (health <= 0)
        {
            canMove = false;
            StartCoroutine(Died());
        }
    }

    void Move()
    {
        groundedPlayer = player.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotateSpeed);
        }
        else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotateSpeed);
        }
        else if(moveDirection == Vector3.zero)
        {
            Idle();
        }

        moveDirection *= moveSpeed;
        player.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            Jump();
        }

        playerVelocity.y += gravity * Time.deltaTime;
        player.Move(playerVelocity * Time.deltaTime);

    }

    void Idle()
    {
        playerAnimator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    void Walk()
    {
        moveSpeed = walkSpeed;
        playerAnimator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    void Run()
    {
        moveSpeed = runSpeed;
        playerAnimator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }

    void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        playerAnimator.SetTrigger("isJumping");
    }

    private void Attack()
    {
        StartCoroutine(AttackInput());

        isPlayed = false;

        if (weaponInHands == null)
        {
            weaponDamage = 10f;
            playerAnimator.SetTrigger("axeAttack");
        }

        if (weaponInHands.tag == "Axe")
        {
            playerAnimator.SetTrigger("axeAttack");
            weaponDamage = 10f;
        }
        if (weaponInHands.tag == "Spear")
        {
            playerAnimator.SetTrigger("spearAttack");
            weaponDamage = 15f;
        }
        if (weaponInHands.tag == "Mace")
        {
            playerAnimator.SetTrigger("maceAttack");
            weaponDamage = 20f;
        }

        if (EnemyController.instance.enemyNear)
        {
            EnemyController.instance.health -= weaponDamage;
        }
    }

    private IEnumerator Died()
    {
        health = 0;
        playerAnimator.SetTrigger("died");

        yield return new WaitForSeconds(2f);
        diedText.gameObject.SetActive(true);
    }

    private IEnumerator CloseMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(1.2f);
        canMove = true;
    }

    private IEnumerator AttackInput()
    {
        isPlayed = true;
        yield return new WaitForSeconds(2f);
        isPlayed = false;
    }
}
