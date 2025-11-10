using Unity.VisualScripting;
using UnityEngine;

public class EtsaiaMugitzen : MonoBehaviour
{
    [SerializeField] float speed = 1;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        speed *= -1;
        AldatuEtsaiaNoranzkoa();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void AldatuEtsaiaNoranzkoa()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.linearVelocity.x)), 1f);
    }
}