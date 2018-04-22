using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int m_LivesLeft = 10;

    public void LoseLife()
    {
        --m_LivesLeft;
        if(m_LivesLeft <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
