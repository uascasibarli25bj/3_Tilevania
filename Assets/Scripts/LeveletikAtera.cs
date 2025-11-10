using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LeveletikAtera : MonoBehaviour
{
    [SerializeField] float itxaronDenbora = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(itxaronDenbora);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            nextSceneIndex = 0;
        }
        
        FindFirstObjectByType<EszenaIraunkorra>().ResetEszenaIraunkorra();

        SceneManager.LoadScene(nextSceneIndex);
    }
}
