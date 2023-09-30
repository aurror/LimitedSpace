using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    public string resourceName;
    public int amount;
    public GameObject counter;
    public Sprite sprite;
    
    public Resource(string name, int amount, GameObject counter, Sprite sprite)
    {
        this.resourceName = name;
        this.amount = amount;
        this.counter = counter;
        this.sprite = sprite;
    }
}
