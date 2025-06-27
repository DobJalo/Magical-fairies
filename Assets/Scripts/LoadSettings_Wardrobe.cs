using UnityEngine;

public class LoadSettings_Wardrobe : MonoBehaviour
{
    public AudioSource audioSource;
    private const string VolumeKey = "Volume";

    void Start() 
    {
        //volume
        if (PlayerPrefs.HasKey(VolumeKey)) // if there is a saved volume value
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey); //get the value from save
            audioSource.volume = savedVolume; //change volume
        }
  
    }
}
