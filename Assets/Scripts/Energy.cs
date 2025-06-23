using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Slider energySlider;

    private float energy;
    private float decresingTime;
    private float divisibleBy;

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

        decresingTime = 5 * 60; // 5 times * 60 seconds = 5 minutes
        divisibleBy = decresingTime / energy;

        //start
        Invoke("DecreaseEnergy", 1f);
    }

    private void DecreaseEnergy()
    {
        // there is still time AND time divided by "number" == 0
        if (decresingTime > 0 && (decresingTime % divisibleBy == 0))
        {
            energy--;
            energySlider.value = energy / 100; // set energy value to slider on screen
            SaveLoadFloat.Save(energy, fileName); //save new energy value
        }

        decresingTime--;
        Invoke("DecreaseEnergy", 1f);
    }

}


// ЕСЛИ персонаж спит, то приостановить спуск
// ДОБАВИТЬ ПОТОМ: если персонаж работает, то спуск быстрее

//при низкой энергии нельзя ничего делать
//энергия отмечается разным цветом