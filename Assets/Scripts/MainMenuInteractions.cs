using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuInteractions : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button exit;

    [Header("Pertsonaia")]
    [SerializeField] float speed = 5f;
    [SerializeField] float itxaronDenbora = 0.2f;

    Rigidbody2D rb;
    Animator animator;
    bool mugitu = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        start.onClick.AddListener(HasiJokoa);
        exit.onClick.AddListener(IrtenJokotik);
    }

    void HasiJokoa()
    {
        Destroy(start.gameObject);
        Destroy(exit.gameObject);
        
        mugitu = true;
        animator.SetBool("KorrikaDago", true);
    }

    void IrtenJokotik()
    {
        Application.Quit();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            Debug.Log("Puerta detectada ✅");
            StartCoroutine(Frenatu());
        }
    }

    IEnumerator Frenatu()
    {
        yield return new WaitForSeconds(itxaronDenbora);
        mugitu = false;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("KorrikaDago", false);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
    }

    void Update()
    {
        if (mugitu)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
    }
}
