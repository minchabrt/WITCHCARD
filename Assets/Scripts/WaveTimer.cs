using UnityEngine;
using System.Collections;
using TMPro;

public class WaveTimer : MonoBehaviour
{

    public float timer = 5f;
    public float redTimer = 2f;

    public float moneyDelayTimer = 1f;
    public float moneyGainedTimer = 3f;
    public bool waveActive = false;
    public int currentWave = 0;
    public int enemiesRemaining = 0;
    public int secondsLeftWhenEnemiesKilled = 0;

    public TMP_Text timerText;
    public TMP_Text waveText;
    public TMP_Text moneyGainedText;

    private bool hadEnemiesThisWave;
    private bool moneyGainedTextShown;
    private bool allEnemiesKilledThisWave;
    private float timeRemaining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText.text = "";
        waveText.text = "";
        moneyGainedText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        enemiesRemaining = 0;

        if (enemyLayer == -1)
        {
            Debug.LogError("The Enemy layer does not exist.");
        }
        else
        {
            GameObject[] sceneObjects = FindObjectsByType<GameObject>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

            foreach (GameObject sceneObject in sceneObjects)
            {
                if (sceneObject.layer == enemyLayer)
                {
                    enemiesRemaining++;
                }
            }
        }

        if (waveActive && enemiesRemaining > 0)
        {
            hadEnemiesThisWave = true;
        }

        if (waveActive && hadEnemiesThisWave && enemiesRemaining == 0
            && !moneyGainedTextShown)
        {
            secondsLeftWhenEnemiesKilled = Mathf.Max(0, Mathf.FloorToInt(timeRemaining));
            moneyGainedTextShown = true;
            allEnemiesKilledThisWave = true;
        }

        if(Input.GetKeyDown(KeyCode.T) && !waveActive){
            currentWave++;
            waveText.text = "Wave: " + currentWave;
            waveActive = true;
            hadEnemiesThisWave = false;
            moneyGainedTextShown = false;
            allEnemiesKilledThisWave = false;
            secondsLeftWhenEnemiesKilled = 0;
            moneyGainedText.text = "";
            StartCoroutine(StartWave());

        }

    }

    public int EnemiesRemaining()
    {
        return enemiesRemaining;
    }

    IEnumerator StartWave(){

        float remainingRedTime = redTimer;
        float remainingMoneyDelay = moneyDelayTimer;
        float remainingMoneyGainedTime = moneyGainedTimer;
        timeRemaining = timer;

        while(timeRemaining > 0f && !allEnemiesKilledThisWave){
            timeRemaining -= Time.deltaTime;
            timeRemaining = Mathf.Max(0f, timeRemaining);
            timerText.text = timeRemaining.ToString("F1");
            yield return null;
        }

        if (allEnemiesKilledThisWave)
        {
            timerText.color = Color.green;
        }
        else
        {
            timerText.text = "0.0";
        }

        while(remainingRedTime > 0f){
            remainingRedTime -= Time.deltaTime;
            remainingMoneyDelay -= Time.deltaTime;
            timerText.color = allEnemiesKilledThisWave ? Color.green : Color.red;
            yield return null;
        }

        while(remainingMoneyDelay > 0f){
            remainingMoneyDelay -= Time.deltaTime;
            yield return null;
        }

        moneyGainedText.text = "+ " + secondsLeftWhenEnemiesKilled;

        while(remainingMoneyGainedTime > 0f){
            remainingMoneyGainedTime -= Time.deltaTime;
            yield return null;
        }

        moneyGainedText.text = "";
        timerText.color = Color.white;

        // Funkcija za spawnovanje neprijatelja
        Debug.Log("Proslo je vreme!!!!");

        timerText.text = "";
        waveText.text = "";
        waveActive = false;
    }


}
