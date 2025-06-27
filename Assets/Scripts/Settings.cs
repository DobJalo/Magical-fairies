using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
    private const string VolumeKey = "Volume"; // slot to save volume settings

    void Start() //show accurate slider values on screen when settings scene is open
    {
        //volume
        if (PlayerPrefs.HasKey(VolumeKey)) // if there is a saved volume value
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey); //get the value from save
            audioSource.volume = savedVolume; //change volume
            volumeSlider.value = savedVolume; //change slider as well
        }
        else // if there is no save
        {
            volumeSlider.value = audioSource.volume; // set slider volume value the same as current volume value 
        }
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float value)
    {
        //set value
        audioSource.volume = value;
        Debug.Log(audioSource.volume); //check if it works

        //save value
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
}
