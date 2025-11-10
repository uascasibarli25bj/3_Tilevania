using UnityEngine;

public class EszenaIraunkorra : MonoBehaviour
{
    void Awake()
    {//SingletonPattern
        int numEszenaIraunkor = FindObjectsByType<EszenaIraunkorra>(FindObjectsSortMode.None).Length;

        if (numEszenaIraunkor > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
    
    public void ResetEszenaIraunkorra()
    {
        Destroy(gameObject);
    }
}