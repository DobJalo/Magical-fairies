using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class SaveScreenshot : MonoBehaviour
{
    string folderPath;
    string fileName;
    Texture2D tex;
    byte[] bytes;


    public void TakeScreenshot()
    {
        StartCoroutine(TakeScreenShotPC());
        //StartCoroutine(TakeScreenShotAndroid());
    }

    private void CreateTexture()
    {
        tex = new Texture2D(Screen.width / 2, Screen.height, TextureFormat.RGB24, false); // width, height
        tex.ReadPixels(new Rect(0, 0, Screen.width / 2, Screen.height), 0, 0); // x, y, width, height
        tex.Apply();

        bytes = tex.EncodeToPNG();
        UnityEngine.Object.Destroy(tex);

        fileName = "Magical Fairies_" + DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png";
    }


    private System.Collections.IEnumerator TakeScreenShotPC()
    {
        yield return new WaitForEndOfFrame();

        CreateTexture();

        string folder = Path.Combine(Application.persistentDataPath, "Screenshots");

        // Create folder
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        folderPath = Path.Combine(folder, fileName);

        // save
        File.WriteAllBytes(folderPath, bytes);

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        // show folder
        Application.OpenURL("file://" + folder);
#endif
    }


    private IEnumerator TakeScreenShotAndroid()
    {
        yield return new WaitForEndOfFrame();

        CreateTexture();

        folderPath = "/storage/emulated/0/Pictures/MagicalFairies";

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string fullPath = Path.Combine(folderPath, fileName);
        File.WriteAllBytes(fullPath, bytes);

#if UNITY_ANDROID && !UNITY_EDITOR
    // update gallery
    using (AndroidJavaClass mediaScanner = new AndroidJavaClass("android.media.MediaScannerConnection"))
    using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                .GetStatic<AndroidJavaObject>("currentActivity"))
    {
        mediaScanner.CallStatic(
            "scanFile",
            activity,
            new string[] { fullPath },
            null,
            null
        );
    }
#endif
    }


}






