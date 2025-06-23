using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Sleep : MonoBehaviour
{
    public GameObject sleeping;
    public Text timer;
    private float sleepRemainingTime; //in seconds
    private float energy;
    private int sleep; // 0 - awake, 1 - sleeping
    private int hoursOfSleep = 100;// 28 800 seconds - 8 hours - should be the same as energyRemainingTime in Energy script
    private string energyFileName = "EnergyLeft.dat";

    void Start()
    {
        if (PlayerPrefs.HasKey("SleepingStatus"))
        {
            sleep = PlayerPrefs.GetInt("SleepingStatus");
        }
        else
        {
            sleep = 0;
        }

        sleepRemainingTime = 100;
    }



    public void OnSleepPressed()
    {
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
        sleepRemainingTime = (100 - energy) / 100 * hoursOfSleep; 

        sleeping.SetActive(true);
        sleep = 1; // sleeping
        PlayerPrefs.SetInt("SleepingStatus", 1);
    }

    public void WakeUpPressed()
    {
        sleep = 0; // wake up
        PlayerPrefs.SetInt("SleepingStatus", 0);

        sleeping.SetActive(false);
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
