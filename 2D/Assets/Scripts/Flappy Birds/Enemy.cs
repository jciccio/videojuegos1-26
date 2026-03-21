using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float MoveSpeed = 2f;
    private float StartY; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position  += Vector3.left * MoveSpeed * Time.deltaTime;
        // Time.deltaTime => Tiempo transcurrido entre el ultimo llamado del update y el siguiente llamado
        // Vector3.left => Vector3 que indica la dirección de la izquierda (-1,0,0)
        if(this.transform.position.x <= -10f)
        {
            float newY = StartY + Random.Range(-2f, 2f);
            transform.position = new Vector3(10, newY, transform.position.z);
            MoveSpeed += 0.05f;
        }
    }
}
