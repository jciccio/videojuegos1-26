
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Vector2 OffsetSize;
    [SerializeField] private Vector3 OffsetPosition; 
    [SerializeField] private GameObject ObjectToFollow; // Asignamos desde el editor
    [SerializeField] private float Speed = 3f;


    

    public Rigidbody2D rb;
    public Vector2 threshold;

    private void Start()
    {
        rb = ObjectToFollow.GetComponent<Rigidbody2D>();
        threshold = CalculateCameraThreshold();
    }


    void FixedUpdate()
    {
        Vector3 follow = ObjectToFollow.transform.position - OffsetPosition;
        float xDiff = Vector2.Distance(Vector2.right*transform.position.x, Vector2.right*follow.x );
        float yDiff = Vector2.Distance(Vector2.up*transform.position.y, Vector2.up*follow.y);

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDiff) >= threshold.x)
        {
            newPosition.x = follow.x;
        }

        if (Mathf.Abs(yDiff) >= threshold.y)
        {
            newPosition.y = follow.y;
        }
        // Brinco Abrupto
        //transform.position = newPosition;


        
        float playerSpeed = rb.linearVelocity.magnitude > Speed ? rb.linearVelocity.magnitude : Speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.fixedDeltaTime* playerSpeed);
    }

    private Vector3 CalculateCameraThreshold()
    {
        Rect screenAspect = Camera.main.pixelRect;
        float cameraSize = Camera.main.orthographicSize;
        Vector2 threshold = new Vector2(cameraSize*screenAspect.width/screenAspect.height, cameraSize);
        return threshold - OffsetSize; // Unidades de la camara en Unity (tamanio de unidedes de Unity, no los pixeles)
    }

   private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 rect = CalculateCameraThreshold();
        Gizmos.DrawWireCube(transform.position + OffsetPosition, new Vector3(rect.x*2, rect.y*2, 1));
    }
}
