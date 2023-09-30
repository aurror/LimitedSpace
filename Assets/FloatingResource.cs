using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingResource : MonoBehaviour
{
 // Define the different resource types


    [SerializeField]  // This attribute makes the field appear in the inspector
    public ResourceSpawner.ResourceType resourceType = ResourceSpawner.ResourceType.Iron;  // Default value

    // Other fields and properties, if needed
    // ...

    void Start()
    {
 
    }
}
