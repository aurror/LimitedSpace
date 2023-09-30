using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    // Assuming you have prefabs for each resource type,
    // you would set these in the Inspector.
 
    public static ResourceSpawner instance;
    private GameObject ironPrefab;

    private GameObject organicPrefab;

    private GameObject waterPrefab;

    private GameObject cablePrefab;

    private GameObject darkMatterPrefab;

    public void Awake(){
        instance = this;
        ironPrefab = Resources.Load("Iron") as GameObject;
        organicPrefab = Resources.Load("Organic") as GameObject;
        waterPrefab = Resources.Load("Water") as GameObject;
        cablePrefab = Resources.Load("Cable") as GameObject;
        darkMatterPrefab = Resources.Load("DarkMatter") as GameObject;
    }

        public enum ResourceType
    {
        Iron,
        
        Organic,
        Water,
        Cable,
        DarkMatter
    }
    // Public method to spawn a resource of a specified type at a specified location.
    public void SpawnResource(ResourceType resourceType, Vector3 location,  Vector2 initialVelocity)
    {
        GameObject prefabToSpawn = null;

        switch (resourceType)
        {
            case ResourceType.Iron:
                prefabToSpawn = ironPrefab;
                break;
            case ResourceType.Organic:
                prefabToSpawn = organicPrefab;
                break;
            case ResourceType.Water:
                prefabToSpawn = waterPrefab;
                break;
            case ResourceType.Cable:
                prefabToSpawn = cablePrefab;
                break;
            case ResourceType.DarkMatter:
                prefabToSpawn = darkMatterPrefab;
                break;
        }

        if (prefabToSpawn != null)
        {
            GameObject spawnedResource = Instantiate(prefabToSpawn, location, Quaternion.identity);
            Rigidbody2D rb = spawnedResource.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = initialVelocity;
            } else {
                Debug.LogWarning("No Rigidbody2D component found on prefab for resource type: " + resourceType);
            }
        }
        
        else
        {
            Debug.LogWarning("No prefab assigned for resource type: " + resourceType);
        }
    }
}