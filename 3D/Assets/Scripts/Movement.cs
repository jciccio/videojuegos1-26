
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Vector3 direction;

    private Rigidbody rb;


    /*
        4 tipos de movimiento:
        Translate -> no usa fisicas de Unity
        
        Los demás si usan fisicas de Unity
        Add Force
        Velocity -> Hace un override de las físicas
        MovePosition -> Mueve el objeto a una posición específica usando físicas
    */


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move para el translate
        //Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        Move(direction);   
    }

    void Move(Vector3 direction)
    {
        // Ejemplo 1: Usando transform translate
        //transform.Translate(direction * speed * Time.deltaTime);

        // Ejemplo 2: AddForce
        //rb.AddForce(direction * speed, ForceMode.Force);

        // Ejemplo 3: velocity
        //rb.linearVelocity = direction * speed;

        // Ejemplo 4: Move Position
        rb.MovePosition(transform.position + (direction*speed*Time.fixedDeltaTime));
    }
}
