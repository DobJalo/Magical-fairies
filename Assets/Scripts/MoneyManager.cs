using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class MoneyManager : MonoBehaviour
{
    private string fileName = "MoneyAmount.dat";
    public Text moneyText;
    private int currentMoney;

    private void Start()
    {
        SaveInt data = SaveLoadInt.Load(fileName);
        if (data == null)
        {
            currentMoney = 10;
        }
        else
        {
            currentMoney = data.intData;
        }

        moneyText.text = currentMoney.ToString();

        // Пример сохранения
        // SaveLoad.Save(currentMoney, fileName);
    }




    void ResetData(string fileName)
    {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            File.Delete(Application.persistentDataPath + fileName);
        }
    }
}
