using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public GameObject character_body;
    public void MoveCharacterCentre()
    {
        character_body.transform.position = new Vector2(0.15f, 0);
    }
    public void MoveCharacterLeft()
    {
        character_body.transform.position = new Vector2(-4, 0);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadWardrobe()
    {
        SceneManager.LoadScene("Wardrobe");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
