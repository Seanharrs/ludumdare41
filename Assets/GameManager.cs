using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string GameOverScene = "GameOver";

    public int HealthValue;

    [SerializeField]
    public int m_LivesLeft = 20;
        
    public void LoseLife()
    {
        --m_LivesLeft;
        if(m_LivesLeft <= 0)
        {
            SceneManager.LoadScene(GameOverScene);
        }
    }

    void Update()
    {
        HealthValue = m_LivesLeft;
    }
}
