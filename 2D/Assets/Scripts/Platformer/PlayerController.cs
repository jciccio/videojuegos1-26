using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Animator Animator;

    [Header("Physics")]
    [SerializeField] float ForceX = 100f;

    private Rigidbody2D Physics;

    [Header("Read Only Fields")]
    public float ForceDirection;

    private Vector3 direction;


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
}
