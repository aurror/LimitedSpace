using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    public string name;
    public int amount;
    public GameObject counter;
    public Sprite sprite;
    
    public Resource(string name, int amount, GameObject counter, Sprite sprite)
    {
        this.name = name;
        this.amount = amount;
        this.counter = counter;
        this.sprite = sprite;
    }
}
