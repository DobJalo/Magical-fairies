using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class ClothesAd : MonoBehaviour
{
    public GameObject BodyParent;
    int index;

    //rewarded
    private RewardedAd rewardedAd;
    // Заменить на свой Ad Unit ID
    [SerializeField] private string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // тестовый
    private bool isRewardEarned = false;


    void Start()
    {
        // get this button component in order to access OnClick
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

        //Initialize ad
        MobileAds.Initialize(initStatus =>
        {
            LoadRewardedAd();
        });

        PlayerPrefs.DeleteKey("Lips_" + 0);

        //load clothes value 
        //0 - blocked
        //1 - shown
        index = transform.GetSiblingIndex(); // get this object's index as a child

        if (BodyParent.CompareTag("Lips"))
        {
            /*for (int i = 0; i < BodyParent.transform.childCount; i++) // check every child in BodyParent
            {
                if (PlayerPrefs.HasKey("Lips_" + i)) //if PlayerPrefs already has saved data
                {
                    LipsArray.SetValue(i, PlayerPrefs.GetInt("Lips_" + i));
                }
                else //if there is no saved data
                {
                    LipsArray.SetValue(i, 0); //CHANGE LOGIC HERE
                }
            }*/

            if (PlayerPrefs.HasKey("Lips_" + 0)) //if PlayerPrefs already has saved data
            {
                LipsArray.SetValue(1, PlayerPrefs.GetInt("Lips_" + 0));
                Debug.Log("value has been loaded with " + PlayerPrefs.GetInt("Lips_" + 0));
            }
            else //if there is no saved data
            {
                LipsArray.SetValue(1, 0);
                Debug.Log("value has been assigned");
            }
        }
    }

    void OnClick()
    {
        Debug.Log("click");
        index = transform.GetSiblingIndex(); // get this object's index as a child

        if (BodyParent.CompareTag("Lips") && LipsArray.GetValue(index) == 0)
        {
            if (rewardedAd != null)
            {
                //rewarded
                rewardedAd.Show((Reward reward) =>
                {
                    isRewardEarned = true;
                });
            }
        }

    }

    //rewarded ad
    #region
    private void LoadRewardedAd()
    {
        AdRequest adRequest = new AdRequest();

        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }

            rewardedAd = ad; //ad was loaded

            // close ad
            rewardedAd.OnAdFullScreenContentClosed += () => {

                //load
                LoadRewardedAd();

                //if player watched ad - reward
                if (isRewardEarned)
                {
                    UnlockItem();
                    isRewardEarned = false;
                }
            };
        });
    }


    //CHANGE IT 
    private void UnlockItem()
    {
        index = transform.GetSiblingIndex(); // get this object's index as a child

        if (BodyParent.CompareTag("Lips"))
        {
            LipsArray.SetValue(index, 1);
            PlayerPrefs.SetInt(("Lips_" + index), 1);
            PlayerPrefs.Save();

            Debug.Log("New saved value: " + PlayerPrefs.GetInt("Lips_" + 0));
        }
    }

    /*public bool IsUnlocked()
    {
        return PlayerPrefs.GetInt("Unlocked_dress01", 0) == 1;
    }*/
    #endregion
}
