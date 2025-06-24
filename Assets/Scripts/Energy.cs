// change "secondsToDeplete" and "secondsOfSleep" 

using System;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Slider energySlider;

    private float energy;
    private int sleep;
    private string fileName = "EnergyLeft.dat";
    private float secondsToDeplete = 16f; // 16f * 3600f = 16 hours in seconds
    private float energyDecreasePerSecond;

    void Start() 
    {
        // load or assign energy value
        SaveFloat data = SaveLoadFloat.Load(fileName);
        if (data == null)
        {
            energy = 100;
        }
        else
        {
            energy = data.floatData;
        }

        energySlider.value = energy / 100f;
        energyDecreasePerSecond = 100f / secondsToDeplete;

        // start repeating energy decrease/increase
        InvokeRepeating("UpdateEnergy", 1f, 1f);
    }

    private void UpdateEnergy()
    {
        // check current cleeping status
        if (PlayerPrefs.HasKey("SleepingStatus"))
        {
            sleep = PlayerPrefs.GetInt("SleepingStatus");
        }
        else
        {
            sleep = 0;
        }

        if (sleep == 0 && energy > 0) // decrease energy
        {
            energy -= energyDecreasePerSecond;
        }
        else if (sleep == 1 && energy < 100) // increase energy
        {
            float secondsOfSleep = 8f; // 8f * 3600f = 8 hours in seconds
            float energyIncreasePerSecond = 100f / secondsOfSleep;
            energy += energyIncreasePerSecond;
        }

        // save energy value
        energy = Mathf.Clamp(energy, 0f, 100f);
        energySlider.value = energy / 100f;
        SaveLoadFloat.Save(energy, fileName);
    }
}



/*using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Energy : MonoBehaviour
{
    public Slider energySlider;

    private float energy;
    private float energyRemainingTime = 100; //  CHANGE HERE
    private float divisibleBy = 1; //  CHANGE HERE

    private int sleep;

    private string fileName = "EnergyLeft.dat";
    void Start() // it should start before Sleep script to load SleepRealTime correctly
    {
        // set energy level = 100 at the beginning OR load from file
        SaveFloat data = SaveLoadFloat.Load(fileName);
        if (data == null)
        {
            energy = 100;
        }
        else
        {
            energy = data.floatData;
        }

        // set energy value to slider on screen
        energySlider.value = energy / 100;

        //start
        Invoke("DecreaseEnergy", 1f);
    }

    private void DecreaseEnergy()
    {
        //get SLEEP integer to check if player is asleep (0 - awake, 1 - sleeping)
        if (PlayerPrefs.HasKey("SleepingStatus"))
        {
            sleep = PlayerPrefs.GetInt("SleepingStatus"); 
        }
        else
        {
            sleep = 0; // player is awake
        }

        // there is still time AND time divided by "number" == 0 AND player is not sleeping
        // decrease time
        if (energy > 0 && (energyRemainingTime % divisibleBy == 0) && sleep == 0)
        {
            energy--;
            energySlider.value = energy / 100; // set energy value to slider on screen
            SaveLoadFloat.Save(energy, fileName); //save new energy value
        }

        // there is still time AND time divided by "number" == 0 AND player is sleeping
        // increase time
        if (energy < 100 && (energyRemainingTime % divisibleBy == 0) && sleep == 1)
        {
            energy++;
            energySlider.value = energy / 100; // set energy value to slider on screen
            SaveLoadFloat.Save(energy, fileName); //save new energy value
        }

        if (sleep == 0)
        {
            energyRemainingTime--;
        }
        if (sleep == 1)
        {
            energyRemainingTime++;
        }

        Invoke("DecreaseEnergy", 1f);
    }

}*/


// ДОБАВИТЬ ПОТОМ: если персонаж работает, то спуск быстрее

//энергия отмечается разным цветом