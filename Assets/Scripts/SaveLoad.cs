using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveClass
{
    public int intData;
}

public static class SaveLoadClass
{
    public static void Save(int integer1, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, fileName);
        using (FileStream file = File.Create(path))
        {
            SaveClass data = new SaveClass();
            data.intData = integer1;

            formatter.Serialize(file, data);
        }
    }

    public static SaveClass Load(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = File.Open(path, FileMode.Open))
            {
                return formatter.Deserialize(file) as SaveClass;
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

public class SaveLoad : MonoBehaviour
{

}
