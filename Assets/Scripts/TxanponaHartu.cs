using UnityEngine;

public class TxanponaHartu : MonoBehaviour
{
    [SerializeField] int txanponBalioa = 100;
    bool dagoenekoHartuta = false;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !dagoenekoHartuta)
        {
            dagoenekoHartuta = true;
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
            FindAnyObjectByType<GameSession>().AddToScore(txanponBalioa);
            Destroy(gameObject);
        }
    }
}
