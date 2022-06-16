using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public GameObject AnimalPrefab; // animal available to instantiate
    private GameObject currentAnimal; // reference to instantiated animal
    private Vector3 animalSize;

    // hardcoded for now
    private const float hexWidth = 13.64f;
    

    void Start()
    {
        Spawn(AnimalPrefab);
    }

    void Spawn(GameObject prefab)
    {
        Hex location = new Hex(1, 1, -2);
        animalSize = prefab.GetComponent<Renderer>().bounds.size;

        currentAnimal = Instantiate(prefab, AnimalPosition(location), Quaternion.identity);
        currentAnimal.GetComponent<Animal>().Location = location;
    }

    Vector3 AnimalPosition(Hex location)
    {
        Vector3 position = Vector3.zero;
        position.z = hexWidth/2 * (Mathf.Sqrt(3) * location.q + Mathf.Sqrt(3)/2 * location.r);
        position.x = hexWidth * 3/4 * (3 / 2 * location.r);
        position.y = animalSize.y / 2;

        return position;
    }
}
