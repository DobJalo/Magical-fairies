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
            energy = data.floatData;
        }

        // set energy value to slider on screen
        energySlider.value = energy / 100; 

        energyRemainingTime = 100; //  CHANGE HERE
        divisibleBy = 1; //  CHANGE HERE

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

}


// ДОБАВИТЬ ПОТОМ: если персонаж работает, то спуск быстрее

//энергия отмечается разным цветом