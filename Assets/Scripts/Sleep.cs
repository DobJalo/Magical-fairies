// change "secondsOfSleep"

using System;
using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
    public GameObject sleeping;
    public GameObject MainAreaMenu;
    public GameObject sleepButton;
    public GameObject loadWardrobeButton;
    public Text timer;

    private float sleepRemainingTime;
    private float energy;
    private int sleep;
    private float secondsOfSleep = 8f;  // 8f * 3600f = 8 hours in seconds
    private string energyFileName = "EnergyLeft.dat";

    void Awake()
    {
        // load sleep status
        if (PlayerPrefs.HasKey("SleepingStatus"))
        {
            sleep = PlayerPrefs.GetInt("SleepingStatus");
        }
        else
        {
            sleep = 0;
        }

        if (sleep == 1)
        {
            // load buttons
            sleeping.SetActive(true);
            MainAreaMenu.SetActive(false);
            sleepButton.SetActive(false);
            loadWardrobeButton.SetActive(false);

            // calculate the difference in time (before leaving the game and after opening it again)
            if (PlayerPrefs.HasKey("SleepRealTime"))
            {
                string savedTimeStr = PlayerPrefs.GetString("SleepRealTime");
                DateTime savedTime = DateTime.Parse(savedTimeStr);
                TimeSpan secondsPassedDraft = DateTime.Now - savedTime;
                int secondsPassed = (int)secondsPassedDraft.TotalSeconds;

                SaveFloat data = SaveLoadFloat.Load(energyFileName);
                if (data == null)
                {
                    energy = 100;
                }
                else
                {
                    energy = data.floatData;
                }

                float sleepProgressPercent = Mathf.Clamp01((float)secondsPassed / secondsOfSleep);
                energy += (100f - energy) * sleepProgressPercent;
                energy = Mathf.Clamp(energy, 0f, 100f);
                SaveLoadFloat.Save(energy, energyFileName);

                sleepRemainingTime = Mathf.Max(0f, secondsOfSleep - secondsPassed);
                PlayerPrefs.DeleteKey("SleepRealTime");
            }

            CalculateTimeOfSleep();
        }
    }

    private void CalculateTimeOfSleep()
    {
        // save current system time
        PlayerPrefs.SetString("SleepRealTime", DateTime.Now.ToString("o"));
        PlayerPrefs.Save();

        // load energy
        SaveFloat data = SaveLoadFloat.Load(energyFileName);
        if (data == null)
        {
            energy = 100;
        }
        else
        {
            energy = data.floatData;
        }

        // calculate the remaining time of sleep
        sleepRemainingTime = (100f - energy) / 100f * secondsOfSleep;
    }

    public void OnSleepPressed()
    {
        CalculateTimeOfSleep();
        sleep = 1;
        PlayerPrefs.SetInt("SleepingStatus", 1);
        PlayerPrefs.Save();
    }

    public void WakeUpPressed()
    {
        PlayerPrefs.DeleteKey("SleepRealTime");
        sleep = 0;
        PlayerPrefs.SetInt("SleepingStatus", 0);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (sleep == 1)
        {
            if (sleepRemainingTime > 0) // update timer
            {
                sleepRemainingTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else // when energy is 100 and timer is 00
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



/*using System;
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

    void Awake()
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

                if (secondsOfSleep - secondsPassed >= 0) // continue timer with a new value
                {
                    sleepRemainingTime = secondsOfSleep - secondsPassed;
                }
                else if (secondsOfSleep - secondsPassed < 0) // check for errors when time < 0
                {
                    sleepRemainingTime = 0;
                }

                // calculate the percentage of how much the time has changed
                float sleepProgressPercent = Mathf.Clamp01((float)secondsPassed / secondsOfSleep) * 100f;
                Debug.Log(sleepProgressPercent);
                // change based on percentage
                energy += (100f - energy) * (sleepProgressPercent / 100f);
                energy = Mathf.Clamp(energy, 0f, 100f);
                //save changed energy
                SaveLoadFloat.Save(energy, "EnergyLeft.dat");


                PlayerPrefs.DeleteKey("SleepRealTime");
            }

            CalculateTimeOfSleep();

        }
    }

    private void CalculateTimeOfSleep()
    {
        PlayerPrefs.SetString("SleepRealTime", DateTime.Now.ToString());
        PlayerPrefs.Save();

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

*/


// remainingTime должно присылать уведомления где timer.text = "00:00:00";