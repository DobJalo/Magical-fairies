using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    //interstitial
    private InterstitialAd interstitial;

    // reward
    private RewardedAd rewardedAd;
    private System.Action onRewardEarned;



    void Awake()
    {
        //interstitial
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
    }

    public void RequestRewarded()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // test ID

        AdRequest request = new AdRequest();

        RewardedAd.Load(adUnitId, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded failed to load: " + error);
                return;
            }

            rewardedAd = ad;

            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                RequestRewarded();
            };


            rewardedAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Rewarded failed to show: " + error);
            };

        });
    }


    public void ShowRewarded(System.Action onRewardSuccess)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            onRewardEarned = onRewardSuccess;

            rewardedAd.Show(reward =>
            {
                Debug.Log("Reward received: " + reward.Amount);
                onRewardEarned?.Invoke();
            });
        }
        else
        {
            Debug.LogWarning("Rewarded ad not ready.");
        }
    }



    //interstitial
    #region
    public void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //test

        AdRequest request = new AdRequest();

        InterstitialAd.Load(adUnitId, request,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Interstitial failed to load: " + error);
                    return;
                }

                interstitial = ad; // loaded

                interstitial.OnAdFullScreenContentClosed += HandleOnAdClosed;
            });
    }

    public void ShowInterstitial() // Interstitial shown. Attached to button
    {
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
        }
    }

    private void HandleOnAdClosed() // Interstitial closed. Requesting new
    {
        RequestInterstitial();
    }
    #endregion
}