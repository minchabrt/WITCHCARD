using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public float playerMaxHealth = 3f;
    public float playerHealth;

    public Slider slider;

    public float reduceSpeed = 2;


    void Start()
    {
        playerHealth = playerMaxHealth;
        UpdateHealthbar();
    }

    void Update()
    {
        UpdateHealthbar();
    }

    public void TakeDamage(float damage)
    {
        playerHealth = Mathf.Max(playerHealth - damage, 0f);
        UpdateHealthbar();


    }

    void UpdateHealthbar()
    {
        // Smoothly interpolate the slider value
        slider.value = Mathf.MoveTowards(slider.value, playerHealth / playerMaxHealth, reduceSpeed * Time.deltaTime);
        whiteSlider.value = Mathf.MoveTowards(whiteSlider.value, playerHealth / playerMaxHealth, whiteReduceSpeed * Time.deltaTime);
    }

    public Slider whiteSlider;
    public float whiteReduceSpeed = 5f;
    

   

}
