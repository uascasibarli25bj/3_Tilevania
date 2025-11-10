using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PertsonaiMugimendua : MonoBehaviour
{
    // Korri
    [SerializeField] float playerSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D myRB;
    Animator animator;


    // Salto
    [SerializeField] float jumpForce = 10f;
    CapsuleCollider2D gorputzaCollider;
    BoxCollider2D oinakCollider;


    // Eskailerak
    [SerializeField] float climbSpeed = 5f;
    float gravityScaleAtStart;
    bool eskaileretan = false;


    // Bizirik
    public bool bizirikDago = true;
    [SerializeField] Vector2 deathKick = new Vector2(25, 25);


    // Izartxoa
    [SerializeField] GameObject izarraPrefab;
    [SerializeField] Transform botatzekoPuntua;


    // Kodea
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gorputzaCollider = GetComponent<CapsuleCollider2D>();
        oinakCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRB.gravityScale;
    }

    void OnMove(InputValue value)
    {
        if (!bizirikDago) return; // hilda badago kontrolak desgaitu
        // mugitzean teklak consolan erakutsi
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!bizirikDago) return; // hilda badago kontrolak desgaitu 
        // "Ground" ukitzen ez badago ez du saltatzen utziko
        if (!oinakCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed) // baldin eta saltatzeko tekla sakatu...
        {
            myRB.linearVelocity = new Vector2(myRB.linearVelocity.x, jumpForce); // ...salto egin
        }
    }
    
    void OnAttack(InputValue value)
    {
        if (!bizirikDago) return;
        Instantiate(izarraPrefab, botatzekoPuntua.position, botatzekoPuntua.rotation);
    }

    void Update()
    {
        if (!bizirikDago) return;
        // ezker-eskubi mugitzeko
        Run();
        // sprite-a ezker-eskubi norabidea aldatzeko
        FlipSprite();
        // eskalatzeko
        EskailerakIgo();
        // Boo ikutzean
        Hil();
    }

    void Run()
    {
        // ezker-eskubi mugitu
        myRB.linearVelocity = new Vector2(moveInput.x * playerSpeed, myRB.linearVelocity.y);

        // korrika animazioa
        bool korrika = Mathf.Abs(moveInput.x) != 0; // geldik ez badago...
        animator.SetBool("KorrikaDago", korrika); // ...korrika animazioa egin
    }

    void FlipSprite()
    {
        // Raul
        //
        //bool playerHasHorizontalSpeed = Mathf.Abs(myRB.linearVelocity.x) > Mathf.Epsilon;
        //if (playerHasHorizontalSpeed)
        //{
        //    transform.localScale = new Vector2(Mathf.Sign(myRB.linearVelocity.x), myRB.linearVelocity.y);
        //}

        if (moveInput.x > 0) // baldin eta eskubira joan...
        {
            transform.localScale = new Vector3(1, 1, 1); // ...eskubira biratu
        }
        else if (moveInput.x < 0) // baldin eta ezkerrera joan...
        {
            transform.localScale = new Vector3(-1, 1, 1); // ...ezkerrera biratu
        }
    }

    void EskailerakIgo()
    {
        if (!eskaileretan)
        {
            myRB.gravityScale = gravityScaleAtStart;
            animator.SetBool("IgotzenDago", false);
            return;
        }

        myRB.gravityScale = 0f;
        myRB.linearVelocity = new Vector2(myRB.linearVelocity.x, moveInput.y * climbSpeed);

        bool playerHasVerticalSpeed = Mathf.Abs(moveInput.y) > Mathf.Epsilon;
        animator.SetBool("IgotzenDago", playerHasVerticalSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Climbing"))
        {
            eskaileretan = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Climbing"))
        {
            eskaileretan = false;
        }
    }

    void Hil()
    {
        if (gorputzaCollider.IsTouchingLayers(LayerMask.GetMask("Etsaiak", "Spikes")))
        {
            bizirikDago = false;
            playerSpeed = 0f;
            animator.SetTrigger("Hiltzen");
            myRB.linearVelocity = deathKick;
            FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }
}
