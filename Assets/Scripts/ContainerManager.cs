using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class ContainerManager : MonoBehaviour
{
    [SerializeField] public int maxRessourceAmount;



    [Header("Counter")]
    [SerializeField] private GameObject currentResourcesCounter;

    [Header("PlayerInventory")]
    [SerializeField] private GameObject playerInventory;
    public List<string> playerInventoryList = new List<string>();
    [SerializeField] Sprite defaultInventorySprite;


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
        if (Input.GetKeyDown(KeyCode.U))
        {
            DeleteAllResourcesInShip();
        }
        if (Input.GetKeyDown(KeyCode.X) && !FloatingLabelController.instance.isInRange)
        {
            DeleteLastPlayerItem();
        }
    }

    private void SetMaxRessources(int amount)
    {
        int newRessourceAmoont = (currentResourceAmount += amount);
        if (newRessourceAmoont > 0 || newRessourceAmoont < maxRessourceAmount)
        {
            currentResourceAmount = newRessourceAmoont;
            currentResourcesCounter.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = currentResourceAmount.ToString() + "/" + maxRessourceAmount;
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
        DisplayPlayerResources();
        DisplayContainerResource(resource);
    }
public void LooseItemAsPlayer(string resource, int amount)
{
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
                res.counter.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = (res.amount.ToString());
            }
        }
    }

    private void ClearPlayerInventoryDisplay()
    {
        for(int i = 0; i < playerInventory.transform.childCount; i++)
        {
            playerInventory.transform.GetChild(i).transform.GetComponent<Image>().sprite = defaultInventorySprite;
        }
    }

    private void DeleteAllResourcesInShip()
    {
        currentResourceAmount = 0;
        currentResourcesCounter.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = currentResourceAmount.ToString() + "/" + maxRessourceAmount;
        foreach (Resource res in resourcesList)
        {
            res.amount = 0;
            res.counter.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = (res.amount.ToString());
        }
    }

    public void DeleteSpecificResource(string resource)
    {
        foreach (Resource res in resourcesList)
        {
            if (res.resourceName == resource)
            {
                res.amount -= 1;
                if (res.amount < 0)
                {
                    res.amount = 0;
                    SetMaxRessources(0);
                }
                else
                {
                    SetMaxRessources(-1);
                }
            }
        }
        DisplayContainerResource(resource);
    }

    private void DeleteLastPlayerItem()
    {
        string res = playerInventoryList.Last();
        LooseItemAsPlayer(res, 1);
    }

    public void DeleteRandomResource()
    {
        Debug.Log("DeleteRandomResource");
        // Initialisiere einen Zufallszahlengenerator
        System.Random zufallszahlengenerator = new System.Random();

        // Wähle ein zufälliges Element aus der Liste aus
        int zufälligerIndex = zufallszahlengenerator.Next(0, resourcesList.Count);
        string randomResource = resourcesList[zufälligerIndex].name;
        foreach (Resource res in resourcesList)
        {
            if (res.resourceName == randomResource)
            {
                res.amount -= 1;
                if (res.amount < 0)
                {
                    res.amount = 0;
                    SetMaxRessources(0);
                }
                else
                {
                    SetMaxRessources(-1);
                }
            }
        }
        DisplayContainerResource(randomResource);
    }
}
