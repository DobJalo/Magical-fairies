using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveInt
{
    public int intData;
}

public static class SaveLoadInt
{
    public static void Save(int integer1, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, fileName);
        using (FileStream file = File.Create(path))
        {
            SaveInt data = new SaveInt();
            data.intData = integer1;

            formatter.Serialize(file, data);
        }
    }

    public static SaveInt Load(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = File.Open(path, FileMode.Open))
            {
                //return formatter.Deserialize(file) as SaveInt; //old
                try
                {
                    return formatter.Deserialize(file) as SaveInt;
                }
                catch (SerializationException) // in case of errors delete file
                {
                    file.Close();
                    File.Delete(path);
                    return null;
                }
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


[System.Serializable]
public class SaveFloat
{
    public float floatData;
}

public static class SaveLoadFloat
{
    public static void Save(float float1, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, fileName);
        using (FileStream file = File.Create(path))
        {
            SaveFloat data = new SaveFloat();
            data.floatData = float1;

            formatter.Serialize(file, data);
        }
    }

    public static SaveFloat Load(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = File.Open(path, FileMode.Open))
            {
                //return formatter.Deserialize(file) as SaveFloat; //old
                try
                {
                    return formatter.Deserialize(file) as SaveFloat;
                }
                catch (SerializationException) // in case of errors delete file
                {
                    file.Close();
                    File.Delete(path);
                    return null;
                }
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
