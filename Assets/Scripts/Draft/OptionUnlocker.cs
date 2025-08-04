using UnityEngine;
using UnityEngine.UI;

public class OptionUnlocker : MonoBehaviour
{
    public string optionKey; // Пример: skin_option_2
    public Button optionButton;
    public GameObject lockIcon;

    private bool isUnlocked;

    void Start()
    {
        optionButton = GetComponent<Button>();
        lockIcon = transform.Find("LockIcon")?.gameObject;
        optionKey = gameObject.name; // например, "skin_option_1"

        isUnlocked = PlayerPrefs.GetInt(optionKey + "_unlocked", 0) == 1;
        UpdateUI();

        optionButton.onClick.AddListener(OnOptionClicked);
    }

    private void UpdateUI()
    {
        lockIcon?.SetActive(!isUnlocked);
    }

    private void OnOptionClicked()
    {
        if (isUnlocked)
        {
            ApplyOption();
        }
        else
        {
            AdManager adManager = FindFirstObjectByType<AdManager>();

            adManager.ShowRewarded(() =>
            {
                PlayerPrefs.SetInt(optionKey + "_unlocked", 1);
                PlayerPrefs.Save();
                isUnlocked = true;
                UpdateUI();
                ApplyOption();
            });
        }
    }

    private void ApplyOption()
    {
        Debug.Log("Option применён: " + optionKey);
        // Здесь можно активировать скин или вызвать нужный метод
    }
}
