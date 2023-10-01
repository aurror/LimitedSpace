using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ContainerManager : MonoBehaviour
{
    [SerializeField] public int maxRessourceAmount;



    [Header("Counter")]
    [SerializeField] private GameObject currentResourcesCounter;

    [Header("PlayerInventory")]
    [SerializeField] private GameObject playerInventory;
    public List<string> playerInventoryList = new List<string>();


    [Header("Resources")]
    [SerializeField] private List<Resource> resourcesList = new List<Resource>();

    [NonSerialized]
    public int currentResourceAmount;
    private int currentResourcesAmountInPlayerInventory;
    private bool canAddResourceToContainer;



    public static ContainerManager instance;

    private void Awake()
    {
        instance = this;
        canAddResourceToContainer = true;
        SetMaxRessources(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            GetNewResourceInContainer("Iron", 1);
        if (Input.GetKeyDown(KeyCode.R))
            GetNewResourceInContainer("Water", 1);
        if (Input.GetKeyDown(KeyCode.I))
            GetNewResourceInContainer("Organic", 1);
  
        if (Input.GetKeyDown(KeyCode.U))
            GetNewResourceInContainer("Cable", 1);
        if (Input.GetKeyDown(KeyCode.X))
        {
            LooseItemAsPlayer("Iron", 1);
        }
    }

    private void SetMaxRessources(int amount)
    {
        int newRessourceAmoont = (currentResourceAmount += amount);
        if (newRessourceAmoont > 0 || newRessourceAmoont < maxRessourceAmount)
        {
            currentResourceAmount = newRessourceAmoont;
            currentResourcesCounter.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "All Resources: " + currentResourceAmount.ToString() + "/" + maxRessourceAmount;
        }
        if (newRessourceAmoont > maxRessourceAmount)
            currentResourceAmount = maxRessourceAmount;
        if (newRessourceAmoont < 0)
            currentResourceAmount = 0;
        if (currentResourceAmount == maxRessourceAmount)
            canAddResourceToContainer = false;
        else
            canAddResourceToContainer = true;

    }

    public void GetNewResourceInContainer(string newRessource, int newAmount)
    {
        if (canAddResourceToContainer)
        {
            foreach(Resource res in resourcesList)
            {

                if(res.resourceName == newRessource)
                {
                    res.amount += newAmount;
                }
            }
            SetMaxRessources(newAmount);
            DisplayContainerResource(newRessource);
        }
    }


    public void GetResourceAsPlayer(string resource, int newAmount)
    {
        
        if (currentResourcesAmountInPlayerInventory == playerInventory.transform.childCount)
        {
            Debug.LogError("Inventory full");
            return;
        }
        foreach (Resource res in resourcesList)
        {
            if (res.resourceName == resource)
            { 
                res.amount -= newAmount;
                if (res.amount < 0)
                {
                    res.amount = 0;
                    SetMaxRessources(0);
                }
                else
                {
                    SetMaxRessources(-newAmount);
                    playerInventoryList.Add(resource);
                    currentResourcesAmountInPlayerInventory += 1;
                }
            }
        }
        Debug.Log("get: " + currentResourcesAmountInPlayerInventory);
        DisplayPlayerResources();
        DisplayContainerResource(resource);
    }
public void LooseItemAsPlayer(string resource, int amount)
{
    Debug.Log(resource + "/" + amount);
    int currentResourceAmount = 0;
    List<string> itemsToRemove = new List<string>();
    foreach (var item in playerInventoryList)
    {
        if (item == resource)
        {
            currentResourceAmount++;
            if (itemsToRemove.Count < amount)
            {
                itemsToRemove.Add(item);
            }
        }
    }

    foreach (var item in itemsToRemove)
    {
        playerInventoryList.Remove(item);
        currentResourcesAmountInPlayerInventory--;
    }

    if (currentResourceAmount < amount)
    {
        Debug.Log("Not enough " + resource);
    }
    DisplayPlayerResources();
    Debug.Log("RemainingItems: " + currentResourcesAmountInPlayerInventory);
}

private void DisplayPlayerResources()
{
    ClearPlayerInventoryDisplay();
    int displayIndex = 0;
    foreach (var item in playerInventoryList)
    {
        Resource matchingResource = resourcesList.Find(res => res.resourceName == item);
        if (matchingResource != null)
        {
            Debug.Log("DisplayPlayerRes: " + matchingResource.resourceName);
            playerInventory.transform.GetChild(displayIndex).transform.GetComponent<Image>().sprite = matchingResource.sprite;
            displayIndex++;
        }
    }
}
    private void DisplayContainerResource(string resource)
    { 
        foreach (Resource res in resourcesList)
        {
            if (res.resourceName == resource)
            {
                res.counter.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = (res.resourceName +  ": " + res.amount.ToString());
            }
        }
    }

    private void ClearPlayerInventoryDisplay()
    {
        for(int i = 0; i < playerInventory.transform.childCount; i++)
        {
            playerInventory.transform.GetChild(i).transform.GetComponent<Image>().sprite = null;
        }
    }
}
