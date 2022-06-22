using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public GameObject AnimalPrefab; // animal available to instantiate
    private List<GameObject> animals; // reference to instantiated animals

    // hardcoded for now
    private const float hexWidth = 13.64f;
    private const float hexHeight = 1.5f;
    

    void Start()
    {
        animals = new List<GameObject>();
        Spawn(AnimalPrefab);
    }

    void Spawn(GameObject prefab)
    {
        GameObject newAnimal;
        Hex location = new Hex(1, 1, -2);
        Vector3 animalSize = prefab.GetComponent<Renderer>().bounds.size;
        
        newAnimal = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        newAnimal.transform.position = AnimalPosition(location, animalSize.y);

        newAnimal.GetComponent<Animal>().Location = location;

        animals.Add(newAnimal);
    }

    public void MakeSelectable()
    {
        if(animals != null)
        {
            foreach(GameObject animal in animals)
            {
                animal.tag = "Selectable";
            }
        }
    }

    public void MakeUnselectable()
    {
        if(animals != null)
        {
            foreach(GameObject animal in animals)
            {
                animal.tag = "Untagged";
            }
        }
    }

    private Vector3 AnimalPosition(Hex location, float animalHeight)
    {
        Vector3 position = Vector3.zero;
        position.z = hexWidth/2 * (Mathf.Sqrt(3) * location.q + Mathf.Sqrt(3)/2 * location.r);
        position.x = hexWidth * 3/4 * (3 / 2 * location.r);
        position.y = (animalHeight + hexHeight) / 2;


        return position;
    }
}
