using Unity.VisualScripting;
using UnityEngine;

public class Pad : MonoBehaviour
{

    [SerializeField] float ScreenSizeInUnits = 17.776f;
    [SerializeField] float minX = 1.25f;
    [SerializeField] float maxX = 16.51f;

    // Update is called once per frame
    void Update()
    {
        // Cómo normalizamos el valor de la pantalla para tener un valor entre 0 y 1?
        float paddleX = Input.mousePosition.x / Screen.width * ScreenSizeInUnits;
        paddleX = Mathf.Clamp(paddleX, minX, maxX);
        transform.position = new Vector2(paddleX, transform.position.y);
        
    }
}
