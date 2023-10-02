using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private int maxValue;
    [SerializeField] private List<StringIntPair> myPairs = new List<StringIntPair>();

    public int currentHealth;
    public static FloatingHealthBar instance;
    public Animator animator;

    private List<string> necessaryResources = new List<string>();
    private bool playerArrived;


    private void Awake()
    {
        instance = this;
        currentHealth = 100;
        playerArrived = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerArrived)
            {
                FixObject(necessaryResources);
            }
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
            playerArrived = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FloatingLabelController.instance.ActivateLabe(false);
            FloatingLabelController.instance.SetInRange(false);
            playerArrived = false;
        }
    }  
    

    private void PlayerArrived()
    {
        FloatingLabelController.instance.ActivateLabe(true);
        FloatingLabelController.instance.SetInRange(true);
        string allNecessaryResources = "";

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Health_green"))
            FloatingLabelController.instance.SetStringObject("Everything is fine", "");
        else
        {
            necessaryResources.Clear();
            foreach (var pair in myPairs)
            {
                allNecessaryResources = allNecessaryResources + pair.stringValue + " x " + pair.intValue + "\n";
                for (int i = 0; i < pair.intValue; i++)
                {
                    necessaryResources.Add(pair.stringValue);
                }
                
            }
            FloatingLabelController.instance.SetStringObject("Need: " + "\n", allNecessaryResources);

        }
    }

    private void FixObject(List<string> necessaryResources)
    {
        List<string> temp = new List<string>(ContainerManager.instance.playerInventoryList);
        bool hasAllResources = true;
        foreach (var item in necessaryResources)
        {
            if (temp.Contains(item))
            {
                temp.Remove(item);
            } else
            {
                hasAllResources = false;
            }
        }

        if (!hasAllResources)
        {
            Debug.Log("Not Enough Resources");

        }
        else
        {
            Debug.Log("Deleting " + necessaryResources.Count + " Resources");
            foreach (string res in necessaryResources)
            {
                ContainerManager.instance.LooseItemAsPlayer(res, 1);
            }
            GetHealth(100);
        }


    }

 
}

