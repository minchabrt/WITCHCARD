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
        RestartWhiteHealthDelay();


    }

    void UpdateHealthbar()
    {
        // Smoothly interpolate the slider value
        slider.value = Mathf.MoveTowards(slider.value, playerHealth / playerMaxHealth, reduceSpeed * Time.deltaTime);
    }

    public float whiteHealthTime = 1f;
    public Slider whiteSlider;
    private Coroutine whiteHealthCoroutine;

    void RestartWhiteHealthDelay()
    {
        if (whiteHealthCoroutine != null)
        {
            StopCoroutine(whiteHealthCoroutine);
        }

        whiteHealthCoroutine = StartCoroutine(WhiteHealthTime());
    }

    IEnumerator WhiteHealthTime()
    {
        yield return new WaitForSeconds(whiteHealthTime);

        while (whiteSlider.value != slider.value)
        {
            whiteSlider.value = Mathf.MoveTowards(whiteSlider.value, slider.value, reduceSpeed * Time.deltaTime);
            yield return null;
        }

        whiteHealthCoroutine = null;
    }

}
