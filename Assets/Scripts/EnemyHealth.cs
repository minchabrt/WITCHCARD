using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Transform healthBarCanvas; // Canvas iznad glave (dete neprijatelja)
    public Slider slider;

    public float target = 1;
    public float reduceSpeed = 2;
    public float enemyMaxHealth = 10f;
    private float enemyHealth;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        enemyHealth = enemyMaxHealth;
        UpdateHealthbar();
    }

    void Update()
    {
        UpdateHealthbar();
    }

    public void TakeDamage(float damage)
    {
        enemyHealth = Mathf.Max(enemyHealth - damage, 0f);
        UpdateHealthbar();

        if (enemyHealth <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    void UpdateHealthbar()
    {
        // Smoothly interpolate the slider value
        slider.value = Mathf.MoveTowards(slider.value, enemyHealth / enemyMaxHealth, reduceSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        // Bar uvek gleda u istom smeru kao kamera (ne utiče na neprijatelja)
        healthBarCanvas.rotation = cam.transform.rotation;
    }
}