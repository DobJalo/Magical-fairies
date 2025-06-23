using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterLoad : MonoBehaviour
{
    public static CharacterLoad Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
