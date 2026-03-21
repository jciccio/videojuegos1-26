using UnityEngine;

public class UpdateScript : MonoBehaviour
{
    private float counter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Ejecución del start");
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        Debug.Log("Ejecución del update " + counter);
    }
}
