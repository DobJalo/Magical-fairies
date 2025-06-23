using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Sleep : MonoBehaviour
{
    public GameObject sleeping;
    public GameObject MainAreaMenu;
    public GameObject sleepButton;
    public GameObject loadWardrobeButton;

    public Text timer;
    private float sleepRemainingTime; //in seconds
    private float energy;
    private int sleep; // 0 - awake, 1 - sleeping
    private int secondsOfSleep = 100;// total amount of hours that needed for full recovery
    private string energyFileName = "EnergyLeft.dat";

    void Start()
    {
        // get "sleep" value
        if (PlayerPrefs.HasKey("SleepingStatus"))
        {
            sleep = PlayerPrefs.GetInt("SleepingStatus");
        }
        else
        {
            sleep = 0;
        }

        // show buttons according to "sleep" value
        if(sleep == 1)
        {
            sleeping.SetActive(true);
            MainAreaMenu.SetActive(false);
            sleepButton.SetActive(false);
            loadWardrobeButton.SetActive(false);

            if (PlayerPrefs.HasKey("SleepRealTime"))
            {
                // get info of how many seconds have passed 
                string savedTimeStr = PlayerPrefs.GetString("SleepRealTime");
                DateTime savedTime = DateTime.Parse(savedTimeStr); // convert from string
                TimeSpan secondsPassedDraft = DateTime.Now - savedTime;
                int secondsPassed = (int)secondsPassedDraft.TotalSeconds; // convert to integer
                Debug.Log(secondsPassed);

                if (secondsOfSleep - secondsPassed >= 0) // continue timer with a new value
                {
                    secondsOfSleep = secondsOfSleep - secondsPassed;
                }
                else if (secondsOfSleep - secondsPassed < 0) // check for errors when time < 0
                {
                    secondsOfSleep = 0;
                }
            }

            CalculateTimeOfSleep();

        }
    }

    private void CalculateTimeOfSleep()
    {
        PlayerPrefs.SetString("SleepRealTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
        Debug.Log(DateTime.Now);

        SaveFloat data = SaveLoadFloat.Load(energyFileName);
        if (data == null)
        {
            energy = 100;
        }
        else
        {
            energy = data.floatData;
        }

        // calculate remainingTime based on how much energy player has at the moment
        // if energy is 0, then player needs to sleep 8 hours
        sleepRemainingTime = (100 - energy) / 100 * secondsOfSleep;
    }


    public void OnSleepPressed()
    {
        CalculateTimeOfSleep();

        sleep = 1; // sleeping
        PlayerPrefs.SetInt("SleepingStatus", 1);
        PlayerPrefs.Save();
    }

    public void WakeUpPressed()
    {
        PlayerPrefs.DeleteKey("SleepRealTime");
        sleep = 0; // wake up
        PlayerPrefs.SetInt("SleepingStatus", 0);
        PlayerPrefs.Save();
    }


    void Update()
    {
        if (sleep == 1)
        {
            if (sleepRemainingTime > 0) //update timer values
            {
                sleepRemainingTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else // fully recovered, sleep is not needed anymore
            {
                timer.text = "00:00:00"; 
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int hours = Mathf.FloorToInt(sleepRemainingTime / 3600);
        int minutes = Mathf.FloorToInt((sleepRemainingTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(sleepRemainingTime % 60);

        timer.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}



// remainingTime должно идти даже при закрытой игре
// remainingTime должно присылать уведомления где timer.text = "00:00:00";