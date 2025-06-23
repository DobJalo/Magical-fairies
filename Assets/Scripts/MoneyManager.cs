using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


/*[System.Serializable]
public class SaveMoney
{
    public int money;
}

public static class SaveLoadMoney
{
    public static void Save(int moneyAmount, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, fileName);
        using (FileStream file = File.Create(path))
        {
            SaveMoney data = new SaveMoney();
            data.money = moneyAmount;

            formatter.Serialize(file, data);
        }
    }

    public static SaveMoney Load(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = File.Open(path, FileMode.Open))
            {
                return formatter.Deserialize(file) as SaveMoney;
            }
        }
        else
        {
            return null;
        }
    }

    public static void ResetData(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
*/
public class MoneyManager : MonoBehaviour
{
    private string fileName = "MoneyAmount.dat";
    public Text moneyText;
    private int currentMoney;

    private void Start()
    {
        SaveClass data = SaveLoadClass.Load(fileName);
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
