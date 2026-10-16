using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Camera camera;
    public Transform target;


    public Slider slider;

    public float enemyHealth;
    public float enemyMaxHealth;

    public void UpdateHealthbar(float currentHealth, float maxHealth){
        slider.value = currentHealth / maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position  - camera.transform.position);
    }
}
