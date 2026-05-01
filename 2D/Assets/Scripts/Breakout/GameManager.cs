using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance = null;


   public int Lives {set; get;}
   [SerializeField] public TextMeshProUGUI guiLives; 


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Lives = 3;
    } 

    void Update()
    {
        if(Lives == 0)
        {
            Debug.Log("Perdio :(");
        }
        guiLives.text = $"Lives: {Lives}";
    }

}
