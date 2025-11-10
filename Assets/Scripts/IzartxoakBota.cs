using UnityEngine;

public class IzartxoakBota : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Rigidbody2D rb;
    PertsonaiMugimendua pertsonaiMugimendua;
    float xMugimendua;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pertsonaiMugimendua = FindFirstObjectByType<PertsonaiMugimendua>();
        xMugimendua = pertsonaiMugimendua.transform.localScale.x * speed;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(xMugimendua, 0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boo")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
