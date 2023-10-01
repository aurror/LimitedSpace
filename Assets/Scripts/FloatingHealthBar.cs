using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private int maxValue;
    [SerializeField] private List<StringIntPair> myPairs = new List<StringIntPair>();

    public int currentHealth;
    public static FloatingHealthBar instance;
    public Animator animator;


    private void Awake()
    {
        instance = this;
        currentHealth = 100;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetHealth(5);
        }

        animator.SetFloat("Health",currentHealth);
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public void GetHealth(int health)
    {
        currentHealth += health;
        if(currentHealth > maxValue)
        {
            currentHealth = maxValue;
        }
    }


    public float GetHealth()
    {
        return currentHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerArrived();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FloatingLabelController.instance.ActivateLabe(false);
            FloatingLabelController.instance.SetInRange(false);
        }
    }  
    

    private void PlayerArrived()
    {
        FloatingLabelController.instance.ActivateLabe(true);
        FloatingLabelController.instance.SetInRange(true);
        string allNecessaryResources = "";
        foreach(var pair in myPairs)
        {
            allNecessaryResources =  allNecessaryResources + pair.stringValue + " x " + pair.intValue + "\n";
        }
        FloatingLabelController.instance.SetStringObject("Need: " + "\n", allNecessaryResources);
    }
}

