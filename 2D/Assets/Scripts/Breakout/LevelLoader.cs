using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{


    [SerializeField] private GameObject BlockPrefab;
    [SerializeField] private float xMovement;
    [SerializeField] private float yMovement;
    [SerializeField] private Transform BlocksContainer ;
    [SerializeField] private Transform StartingPosition;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       LoadLevel("Assets/Levels/Level1.txt");
    }

    IEnumerator AnimateToPosition(GameObject obj, Vector2 targetPosition)
    {
        Transform objTransform = obj.transform;
        while(obj != null && Vector2.Distance(objTransform.position, targetPosition) > 0.1f)
        {
            objTransform.position = Vector2.Lerp(objTransform.position, targetPosition, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        if(objTransform != null)
            objTransform.position = targetPosition;
        yield return null;
    }

    public bool LoadLevel(string path)
    {
        string data = LoadLevelFile(path);
        // level 1
        // X X X X X 
        // X X X
        string [] line = data.Split("\n");
        Vector2 position = StartingPosition.position;
        int count = 1;
        for(int i = 0; i < line.Length; i++) // Representa las filas
        {
            for(int j = 0; j < line[i].Length ; j++) // Columnas del archivo
            {
                if(line[i][j] == 'X')
                {
                    // Instanciar, colocar y bautizar
                    GameObject block = GameObject.Instantiate(BlockPrefab);
                    //block.transform.position = position;
                    StartCoroutine(AnimateToPosition(block,position));
                    block.name = $"Block {count}";
                    block.transform.SetParent(BlocksContainer);
                    count++;
                }
                position.x += xMovement;
            }
            position.y += yMovement;
            position.x = StartingPosition.position.x;
        }
        return count > 1;
    }

    string LoadLevelFile(string path) // Assets/Levels/Level1.txt
    {
        string data = "";
        try
        {
           using(StreamReader sr = new StreamReader(path))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    data += line + "\n";
                }
            } 
        }
        catch(IOException e)
        {
            Debug.LogError($"File not found {e}");
        }
        return data;
    }

    
}
