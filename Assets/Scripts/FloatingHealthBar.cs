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
    [SerializeField] private GameObject sign;

    public int currentHealth;
    public float immunityTime = 5;
    public static FloatingHealthBar instance;
    public Animator animator;

    private List<string> necessaryResources = new List<string>();
    private bool playerArrived;

    private bool canGetDamage;


    private void Awake()
    {
        instance = this;
        currentHealth = 100;
        playerArrived = false;
        canGetDamage = true;
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
        if (canGetDamage)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }
   
    }

    public void GetHealth(int health)
    {
        currentHealth += health;
        if(currentHealth > maxValue)
        {
            currentHealth = maxValue;
            canGetDamage = false;
            StartCoroutine(Timer(immunityTime));
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        canGetDamage = true;
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
            sign.GetComponent<SignManager>().DeactivateSign();
            playerArrived = false;
            FloatingLabelController.instance.ActivateLabe(false);
        }
    }  
    

    private void PlayerArrived()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Health_green"))
        {
            sign.GetComponent<SignManager>().ActivateSignEveryThingIsFine("Everything is fine");
        }

        else
        {
            necessaryResources.Clear();

            for(int i = 0; i < myPairs.Count; i++)
            {
                Debug.Log(i);
                sign.GetComponent<SignManager>().SetRow(i, myPairs[i].intValue, myPairs[i].stringValue);
                
                for (int j = 0; j < myPairs[i].intValue; j++)
                {
                    Debug.Log(j);
                    necessaryResources.Add(myPairs[i].stringValue);
                }
            }
            sign.GetComponent<SignManager>().ActivateResourceSign();
         

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
            FloatingLabelController.instance.ActivateLabe(true);
            FloatingLabelController.instance.SetText("Not Enough Resources");

        }
        else
        {
            foreach (string res in necessaryResources)
            {
                ContainerManager.instance.LooseItemAsPlayer(res, 1);
            }
            GetHealth(100);
        }


    }

 
}

