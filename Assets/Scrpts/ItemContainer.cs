using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    private Dictionary<string, int> resources = new Dictionary<string, int>();
    [SerializeField] private int maxRessourceAmount;

    [Header("Debug")]
    //später private
    private int currentRessourceAmount;
    private bool canAddResourceToContainer;

    public static ItemContainer instance;

    private void Awake()
    {
        instance = this;
        canAddResourceToContainer = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            GetNewResourceInContainer("Iron", 1);
        if (Input.GetKeyDown(KeyCode.R))
            GetNewResourceInContainer("Water", 1);
        if (Input.GetKeyDown(KeyCode.M))
        {
            GetResourceAsPlayer("Water", 1);
        }

    }

    private void SetMaxRessources(int amount)
    {
        int newRessourceAmoont = (currentRessourceAmount += amount);
        if(newRessourceAmoont > 0 || newRessourceAmoont < maxRessourceAmount)
            currentRessourceAmount = newRessourceAmoont;
        if (newRessourceAmoont > maxRessourceAmount)
            currentRessourceAmount = maxRessourceAmount;
        if (newRessourceAmoont < 0)
            currentRessourceAmount = 0;
        if (currentRessourceAmount == maxRessourceAmount)
            canAddResourceToContainer = false;
        else
            canAddResourceToContainer = true;

    }

    public void GetNewResourceInContainer(string newRessource, int newAmount)
    {
        if (canAddResourceToContainer)
        {
            if (!resources.ContainsKey(newRessource))
            {
                resources.Add(newRessource, newAmount);
            }
            else
            {
                resources[newRessource] += newAmount;
            }
            SetMaxRessources(newAmount);
            DisplayContainerResource(newRessource, newAmount);
        }
    }


    public void GetResourceAsPlayer(string resource, int newAmount)
    {
        if (!resources.ContainsKey(resource))
        {
            Debug.LogError("Resource doesn't exist in Container");
            return;
        }
        resources[resource] -= newAmount;
        if (resources[resource] < 0)
        {
            resources[resource] = 0;
            SetMaxRessources(0);
        }
        else
        {
            SetMaxRessources(-newAmount);
        }


        DisplayPlayerResources(resource, resources[resource]);
    }

    private void DisplayPlayerResources(string resource, int newAmount)
    {
        DebugConole();
    }

    private void DisplayContainerResource(string resource, int newAmount)
    {
        DebugConole();
    }

    private void DebugConole()
    {
        foreach (KeyValuePair<string, int> attachStat in resources)
        {
            //Now you can access the key and value both separately from this attachStat as:
            Debug.Log(attachStat.Key + "/ " + attachStat.Value);
        }
    }
  
}
