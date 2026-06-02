using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    [Header("Physics")]
    [SerializeField] float ForceX = 100f;
    public LayerMask groundLayer;

    [Header("References")]
    [SerializeField] Animator Animator;

    private Rigidbody2D Physics;

    [Header("Read Only Fields")]
    public float ForceDirection;
    private Vector3 direction;
    public Vector3 playerOffset;
    public float groundLength = 0.55f;
    public bool jumping;
    public bool jumpPressed;


    [Header("Physics Zone")]

    public float jumpSpeed  = 6f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    public float linearDrag; 

    public float jumpDelay = 0.25f;
    public float jumpTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(ForceDirection > 0)
        {
            // Vamos hacia la derecha
            direction = Vector3.zero;
            Animator.SetBool("RunTrigger", true);
            
        }
        else if (ForceDirection < 0)
        {
            // Vamos hacia la izquierda
            direction = new Vector3(0, 180, 0);
            Animator.SetBool("RunTrigger", true);
        }
        else
        {
            // No nos movemos
            Animator.SetBool("RunTrigger", false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rotationTarget = Quaternion.Euler(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationTarget, Time.fixedDeltaTime * 10);
        Physics.AddForce(new Vector2(ForceX * ForceDirection * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
        jumping = !Physics2D.Raycast(transform.position + playerOffset, Vector2.down, groundLength, groundLayer);
        ModifyPlayerPhysics();
        if (jumpTimer > Time.time && !jumping)
        {
            Jump();
        }
       
    }

    public void RunControl(InputAction.CallbackContext context)
    {
        Debug.Log($"Contexto: {context.phase} -> {context.ReadValue<float>()}");
        if (context.performed)
        {
            ForceDirection = context.ReadValue<float>();
        }
        else if (context.canceled)
        {
            ForceDirection = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        // Gizmos.DrawLine(transform.position + playerOffset, transform.position + playerOffset + Vector3.down * groundLength);
        // Gizmos.DrawLine(transform.position - playerOffset, transform.position - playerOffset + Vector3.down * groundLength);
        Gizmos.DrawLine( transform.position + playerOffset, transform.position + playerOffset + Vector3.down * groundLength);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpTimer = Time.time + jumpDelay;
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    void Jump()
    {
        Physics.linearVelocity = new Vector2(Physics.linearVelocity.x, 0);
        Physics.AddForce(transform.up * jumpSpeed , ForceMode2D.Impulse);
    }

    void ModifyPlayerPhysics()
    {
        if (!jumping)
        {
            Physics.gravityScale = 0;
        }
        else
        {
            Physics.gravityScale = gravity;
            Physics.linearDamping = linearDrag * 0.15f;
            if (Physics.linearVelocity.y  > 0) // Va hacia arriba
            {
                Physics.gravityScale = gravity * (fallMultiplier /2);
            }
            else
            {
                Physics.gravityScale  = gravity * fallMultiplier;
            }
        }
    }
}
