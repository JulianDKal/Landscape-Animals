using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public GameObject AnimalPrefab; // animals available to instantiate
    private List<GameObject> animals; // reference to instantiated animals

    // hardcoded for now
    private const float hexWidth = 13.64f;
    private const float hexHeight = 1.9f;
    

    void Start()
    {
        animals = new List<GameObject>();
        StartCoroutine(SpawnAt(AnimalPrefab, 0, 4));
        StartCoroutine(SpawnAt(AnimalPrefab, 1, 3));
        StartCoroutine(SpawnAt(AnimalPrefab, 2, 6));
    }


    public IEnumerator SpawnAt(GameObject prefab, Hexagon hex)
    {
        //probably should check if given hex belongs to the grid
        yield return new WaitUntil(() => HexagonGrid.instance.hexagons != null);

        Vector3 animalSize = prefab.GetComponent<Renderer>().bounds.size;
        
        GameObject newAnimal = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        newAnimal.transform.parent = hex.transform;
        newAnimal.transform.position = hex.transform.position + Vector3.up * (animalSize.y + hexHeight) / 2;

        animals.Add(newAnimal);
    }


    public IEnumerator SpawnAt(GameObject prefab, int q, int r)
    {
        yield return new WaitUntil(() => HexagonGrid.instance.hexagons != null);

        Vector3 animalSize = prefab.GetComponent<Renderer>().bounds.size;
        GameObject hex = HexagonGrid.instance.GetHexagon(q, r);
        
        if(hex != null)
        {
            GameObject newAnimal = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newAnimal.transform.parent = hex.transform;
            newAnimal.transform.position = hex.transform.position + Vector3.up * (animalSize.y + hexHeight) / 2;

            animals.Add(newAnimal);
        }
        else
        {
            Debug.LogWarning("Acessing null hexagon!");
        }
    }

    public void MakeActors()
    {
        if(animals != null)
        {
            foreach(GameObject animal in animals)
            {
                animal.layer = LayerMask.NameToLayer("Actor");
            }
        }
    }

    public void MakeDefault()
    {
        if(animals != null)
        {
            foreach(GameObject animal in animals)
            {
                animal.layer = LayerMask.NameToLayer("Default");
            }
        }
    }

    /*
    private Vector3 AnimalPosition(Hex location, float animalHeight)
    {
        Vector3 position = Vector3.zero;
        position.z = hexWidth/2 * (Mathf.Sqrt(3) * location.q + Mathf.Sqrt(3)/2 * location.r);
        position.x = hexWidth * 3/4 * (3 / 2 * location.r);
        position.y = (animalHeight + hexHeight) / 2;


        return position;
    }
    */
}
