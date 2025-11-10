using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bizitzakTestua;
    [SerializeField] TextMeshProUGUI puntuazioaTestua;

    [SerializeField] int jokalariBizitzak = 3;

    int puntuazioa = 0;

    void Awake()
    { // SingletonPattern
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        bizitzakTestua.text = jokalariBizitzak.ToString();
        puntuazioaTestua.text = "0";
    }

    public void ProcessPlayerDeath()
    {
        if (jokalariBizitzak > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        jokalariBizitzak--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        bizitzakTestua.text = jokalariBizitzak.ToString();
    }

    void ResetGameSession()
    {
        FindFirstObjectByType<EszenaIraunkorra>().ResetEszenaIraunkorra();
        SceneManager.LoadScene("EndScore");
        Destroy(gameObject);
    }

    public void AddToScore(int addToScore)
    {
        puntuazioa += addToScore;
        puntuazioaTestua.text = puntuazioa.ToString();
    }
}
