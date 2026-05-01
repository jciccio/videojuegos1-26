
using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    [Header("Physics")]
    [SerializeField] Vector2 Velocity = new Vector2(1f, 3f);
    [SerializeField] float _collisionFloatMargin = 0.45f;
    [SerializeField] float XMultiplier = 1f;


    bool playing =false;
    [SerializeField] Vector2 paddleToBallDistance = new Vector2(0f, 2f);
    [SerializeField] Pad pad;



    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void LockToPaddle()
    {
        if (!playing)
        {
            Vector2 paddleRef = pad.transform.position;
            Vector2 paddlePos = new Vector2(paddleRef.x, paddleRef.y);
            transform.position = paddlePos + paddleToBallDistance;
        }
    }

    void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playing = true;
            rb.linearVelocity = new Vector2(0f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LockToPaddle();
        LaunchBall();
    }

    void FixedUpdate() // Fisicas del juego -> 50 veces por segundo
    {
        rb.linearVelocity = Velocity;        
    }

    void OnHorizontalCollision()
    {
        Velocity = new Vector2(Velocity.x, Velocity.y * -1);
    }
    void OnVerticalCollision()
    {
        Velocity = new Vector2(Velocity.x * -1, Velocity.y);
    }

    void OnPaddleCollision(Collision2D collision){
        float xCollisionPoint = collision.contacts[0].point.x - collision.transform.position.x;
        Velocity = new Vector2(xCollisionPoint * XMultiplier, Velocity.y * -1);
        rb.linearVelocity = Velocity;
    }

    void OnBlockCollision(Collision2D elementBlock)
    {
        // Donde colisiona en el bloque
        //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/ContactPoint2D.html
        Vector2 collision = elementBlock.GetContact(0).point; 
        float xColPoint = collision.x - elementBlock.transform.position.x;
        float yColPoint = collision.y - elementBlock.transform.position.y;
        if(MathF.Abs(yColPoint) > _collisionFloatMargin)
        {
            OnHorizontalCollision();
        }
        else if (MathF.Abs(xColPoint) > _collisionFloatMargin)
        {
            OnVerticalCollision();
        }
    }

    void OnLost()
    {
        GameManager.instance.Lives--;
        playing = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Colisiona con {collision.gameObject.tag}");
        // Contra quien colisiona?
        // Pared
            // - Horizontal
            // - Vertical
        // Caja (block)
        // Paleta del jugador
        string collisionTag = collision.gameObject.tag;
        if(collisionTag == Constants.HORIZONTAL_WALL_TAG)
        {
            OnHorizontalCollision();
        }
        if(collisionTag == Constants.VERTICAL_WALL_TAG)
        {
            OnVerticalCollision();
        }
        if(collisionTag == Constants.BLOCK_TAG)
        {
            OnBlockCollision(collision);
        }
        else if(collisionTag == Constants.PADDLE_TAG)
        {
            OnPaddleCollision(collision);
        }
        else if(collisionTag == Constants.LOST_TAG)
        {
           OnLost();
        }

    }
}
