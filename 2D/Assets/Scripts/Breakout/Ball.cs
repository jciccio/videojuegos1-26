
using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    [Header("Physics")]
    [SerializeField] Vector2 Velocity = new Vector2(1f, 3f);
    [SerializeField] float _collisionFloatMargin = 0.45f;


    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Visual   
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

    void OnPaddleCollision()
    {
        // Si la bola rebota hacia un costado, 
        // el angulo del rebote deberia de variar con respecto al punto de colision
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

    }
}
