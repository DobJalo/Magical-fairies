using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
    public GameObject sleeping;
    

    public void OnSleepPressed()
    {
        sleeping.SetActive(true);

        //start timer 8 hours
        //player's energy recover
    }
}
