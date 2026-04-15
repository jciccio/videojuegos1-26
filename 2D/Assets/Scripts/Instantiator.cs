using UnityEngine;

public class Instantiator : MonoBehaviour
{


    [SerializeField] private GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject go = Instantiate(prefab);
        go.transform.position = new Vector3(5f, 5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
