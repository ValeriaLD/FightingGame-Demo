using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [SerializeField]
    NavMeshAgent agent;

    private Animator enemyAnimator;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float payerHeath;

    private float distance;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float seeingRadius;

    [SerializeField]
    private Text winText;

    public float health = 100;

    public bool enemyNear = false;

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

    private void Start()
    {
        // agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        enemyAnimator = GetComponentInChildren<Animator>();
        winText.gameObject.SetActive(false);
    }

    private void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);

        if (distance <= seeingRadius && health > 0)
        {
            EnemyMovment();
            // agent.SetDestination(player.transform.position);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }

        if (health <= 0)
        {
            StartCoroutine(Died());
            winText.gameObject.SetActive(true);
        }
    }

    private void EnemyMovment()
    {
        enemyAnimator.SetBool("isRunning", true);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        transform.position = Vector3.Lerp(transform.position, player.position , 0.002f);
    }

    private void OnTriggerStay(Collider other)
    {
        enemyNear = true;

        if (other.gameObject.tag == "Player")
        {
            if (!PlayerController.instance.blockAttack)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyNear = false;
    }

    private IEnumerator AttackPlayer()
    {
        enemyAnimator.SetTrigger("attacking");

        if (PlayerController.instance.health > 0)
        {
            if (PlayerController.instance.blockAttack)
            {
                PlayerController.instance.health -= 0;
            }
            else
            {
                PlayerController.instance.health -= 0.05f;
            }
        }

        yield return new WaitForSeconds(2f);
    }

    private void OnCollisionStay(Collision collision)
    {
        agent.isStopped = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        agent.isStopped = false;
    }


    private IEnumerator Died()
    {
        agent.isStopped = true;
        health = 0;
        transform.position -= transform.up;
        enemyAnimator.SetTrigger("death");

        yield return new WaitForSeconds(2f);
        winText.gameObject.SetActive(true);
    }
}
