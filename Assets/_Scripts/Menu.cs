using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject creditsMenu;

    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void ActiveCredits()
    {
        if(creditsMenu == null)
        {
            return;
        }
        if(creditsMenu.activeSelf)
        {
            creditsMenu.SetActive(false);
        }
        else
        {
            creditsMenu.SetActive(true);
        }
    }
}
