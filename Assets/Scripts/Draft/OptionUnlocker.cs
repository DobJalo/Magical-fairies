using UnityEngine;
using UnityEngine.UI;

public class OptionUnlocker : MonoBehaviour
{
    public string optionKey; 
    public GameObject lockIcon;
    private Button optionButton;
    private bool isUnlocked;

    private void Start()
    {
        optionKey = gameObject.name;
        optionButton = GetComponent<Button>();

        isUnlocked = PlayerPrefs.GetInt(optionKey + "_unlocked", 0) == 1;
        UpdateUI();

        optionButton.onClick.AddListener(OnOptionClicked);
    }

    void Update()
    {
        // Reset when pressing D
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteKey(optionKey + "_unlocked");
            isUnlocked = false;
            UpdateUI();
            Debug.Log("Reset unlock state for {optionKey}");
        }
    }

    private void UpdateUI()
    {
        if (isUnlocked)
        {
            lockIcon.SetActive(false);
            //optionButton.interactable = true;
        }
        else
        {
            lockIcon.SetActive(true);
            //optionButton.interactable = true;
        }
    }

    private void OnOptionClicked()
    {
        if (isUnlocked)
        {
            ApplyOption();
        }
        else
        {
            TryUnlockWithAd();
        }
    }

    private void TryUnlockWithAd()
    {
        if (AdManager.Instance != null && AdManager.Instance.IsRewardedReady)
        {
            AdManager.Instance.ShowRewarded(() =>
            {
                isUnlocked = true;
                PlayerPrefs.SetInt(optionKey + "_unlocked", 1);
                PlayerPrefs.Save();
                UpdateUI();
            });
        }
        else
        {
            Debug.Log("Rewarded ad not ready.");
        }
    }

    private void ApplyOption()
    {
        Debug.Log("Option applied: " + optionKey);
    }
}
