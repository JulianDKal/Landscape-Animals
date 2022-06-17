using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public GameObject AnimalPrefab; // animal available to instantiate
    private GameObject currentAnimal; // reference to instantiated animal

    // hardcoded for now
    private const float hexWidth = 13.64f;
    private const float hexHeight = 1.5f;
    

    void Start()
    {
        Spawn(AnimalPrefab);
    }

    void Spawn(GameObject prefab)
    {
        Hex location = new Hex(1, 1, -2);
        Vector3 animalSize = prefab.GetComponent<Renderer>().bounds.size;
        
        currentAnimal = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        currentAnimal.transform.position = AnimalPosition(location, animalSize.y);

        currentAnimal.GetComponent<Animal>().Location = location;
    }

    Vector3 AnimalPosition(Hex location, float animalHeight)
    {
        Vector3 position = Vector3.zero;
        position.z = hexWidth/2 * (Mathf.Sqrt(3) * location.q + Mathf.Sqrt(3)/2 * location.r);
        position.x = hexWidth * 3/4 * (3 / 2 * location.r);
        position.y = (animalHeight + hexHeight) / 2;


        return position;
    }
}
