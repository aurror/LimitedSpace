using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingResource : MonoBehaviour
{
 // Define the different resource types
    public enum ResourceType
    {
        Iron,
        
        Organic,
        Water,
        Cable,
        DarkMatter
    }

    [SerializeField]  // This attribute makes the field appear in the inspector
    public ResourceType resourceType = ResourceType.Iron;  // Default value

    // Other fields and properties, if needed
    // ...

    void Start()
    {
 
    }
}
