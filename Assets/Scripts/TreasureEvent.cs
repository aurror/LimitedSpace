using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureEvent : MonoBehaviour
{

    private int resourcesToSpawn = 20;
    private List<ResourceSpawner.ResourceType> resources = new List<ResourceSpawner.ResourceType>();
    private bool eventEnabled = false;
    private float remainingEventTime = 0f;
    public static DimensionalRiftManager instance;
    private bool eventIsRunning;
    void Awake()
    {

        eventIsRunning = false;
    }
    void Start(){
        this.resources.Add(ResourceSpawner.ResourceType.Organic);
        this.resources.Add(ResourceSpawner.ResourceType.Cable);
    }


    public void EnableEvent()
    {
        int resourceIndex = 0;
        for (int i = 0; i < resourcesToSpawn; i++)
        {
            ResourceSpawner.instance.SpawnResource((ResourceSpawner.ResourceType)resources[resourceIndex], new Vector3(-15,55 + i * 2,1), Vector2.zero);
            resourceIndex++;
            if (resourceIndex >= resources.Count)
            {
                resourceIndex = 0;
            }
        }
        
    }
}
