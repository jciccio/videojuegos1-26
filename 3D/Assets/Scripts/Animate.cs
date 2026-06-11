using UnityEngine;

public class Animate : MonoBehaviour
{
    Renderer renderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.SetFloat("_Modifier" , Time.time);
    }
}
