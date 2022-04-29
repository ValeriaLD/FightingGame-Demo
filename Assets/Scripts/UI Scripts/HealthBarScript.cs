using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float maxHealth = 100f;

    [SerializeField]
    private GameObject currentCharacter;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    private void Update()
    {
        if(currentCharacter.gameObject.tag == "Player")
        {
            currentHealth = PlayerController.instance.health;
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            currentHealth = EnemyController.instance.health;
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }
}
