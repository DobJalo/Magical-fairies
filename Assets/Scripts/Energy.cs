using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Slider energySlider;

    private float energy;
    private float energyRemainingTime;
    private float divisibleBy;

    private int sleep;

    private string fileName = "EnergyLeft.dat";
    void Start() 
    {
        // set energy level = 100 at the beginning OR load from file
        SaveFloat data = SaveLoadFloat.Load(fileName);
        if (data == null) 
        {
            energy = 100;
        }
        else
        {
            //energy = data.intData;
            energy = 100; // JUST FOR TESTING
        }

        // set energy value to slider on screen
        energySlider.value = energy / 100; 

        energyRemainingTime = 100; // 5 times * 60 seconds = 5 minutes CHANGE HERE
        divisibleBy = energyRemainingTime / energy;

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
        if (energyRemainingTime > 0 && (energyRemainingTime % divisibleBy == 0) && sleep == 0)
        {
            energy--;
            energySlider.value = energy / 100; // set energy value to slider on screen
            SaveLoadFloat.Save(energy, fileName); //save new energy value
        }

        // there is still time AND time divided by "number" == 0 AND player is sleeping
        // increase time
        if (energyRemainingTime < 100 && (energyRemainingTime % divisibleBy == 0) && sleep == 1)
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

}


// ƒќЅј¬»“№ ѕќ“ќћ: если персонаж работает, то спуск быстрее

//при низкой энергии нельз€ ничего делать
//энерги€ отмечаетс€ разным цветом