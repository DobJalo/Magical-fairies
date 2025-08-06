using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }

    private RewardedAd rewardedAd;
    private System.Action onRewardEarned;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        MobileAds.Initialize(initStatus => { });
        RequestRewarded();
    }

    public void RequestRewarded()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // test ID
        AdRequest request = new AdRequest();

        RewardedAd.Load(adUnitId, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load: " + error);
                return;
            }

            rewardedAd = ad;
            rewardedAd.OnAdFullScreenContentClosed += () => RequestRewarded();
        });
    }

    public void ShowRewarded(System.Action onRewardSuccess)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            onRewardEarned = onRewardSuccess;
            rewardedAd.Show(reward =>
            {
                Debug.Log("Reward earned: " + reward.Amount);
                onRewardEarned?.Invoke();
            });
        }
        else
        {
            Debug.LogWarning("Rewarded ad not ready.");
        }
    }

    public bool IsRewardedReady => rewardedAd != null && rewardedAd.CanShowAd();
}
