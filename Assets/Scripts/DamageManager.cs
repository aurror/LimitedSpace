using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Ship Objects")]
    [SerializeField] private List<GameObject> shipObjects = new List<GameObject>();
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject shield;

    [Header("Events")]
    [SerializeField] private string currentEvent;
    [SerializeField] private List<StringIntPair> eventDamage = new List<StringIntPair>();
    [SerializeField] private int solarFlairTime;

    public static DamageManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (StringIntPair pair in eventDamage)
            {
                if (pair.stringValue == "SolarFlair")
                {
                    SolarFlair(solarFlairTime, pair.intValue, generator);
                }
            }
        }
  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (shipObjects.Count > 0)
        {
            if(currentEvent == "Meteore")
            {
                foreach(StringIntPair pair in eventDamage)
                {
                    if(pair.stringValue == "Meteore")
                    {
                        MeteoreEvent(collision.gameObject, pair.intValue);
                    }
                }
            }
            foreach (StringIntPair pair in eventDamage)
            {
                if (pair.stringValue == "PirateAttack")
                {
                    EnemyAttack(collision.gameObject, pair.intValue);
                }
            }
        }
    }

    public void SetCurrentEvent(string newEvent)
    {
        currentEvent = newEvent;
    }

    private void MeteoreEvent(GameObject gameObject, int damage)
    {
        if(shield.GetComponent<FloatingHealthBar>().GetHealth() > 0)
        {
            shield.GetComponent<FloatingHealthBar>().GetDamage(damage);

        }
        else
        {
            // Generate a random index
            int randomIndex = Random.Range(0, shipObjects.Count);

            // Get the random GameObject
            GameObject randomGameObject = shipObjects[randomIndex];

            randomGameObject.GetComponent<FloatingHealthBar>().GetDamage(damage);
        }

        if (gameObject.tag == "Enemy")
            Destroy(gameObject);
    }

    public void SolarFlair(int time, int damage, GameObject gameObject)
    {
        StartCoroutine(IncrementDamage(time, damage, gameObject));
    }

    IEnumerator IncrementDamage(int time, int damage, GameObject gameObject)
    {
        while(time > 0)
        {
            gameObject.GetComponent<FloatingHealthBar>().GetDamage(damage);
            time -= 1;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void EnemyAttack(GameObject gameObject, int damage)
    {
        if (shield.GetComponent<FloatingHealthBar>().GetHealth() > 0)
        {
            shield.GetComponent<FloatingHealthBar>().GetDamage(damage);
        }
        else
        {
            // Generate a random index
            int randomIndex = Random.Range(0, shipObjects.Count);

            // Get the random GameObject
            GameObject randomGameObject = shipObjects[randomIndex];

            randomGameObject.GetComponent<FloatingHealthBar>().GetDamage(damage);
        }

        if (gameObject.tag == "Enemy")
            Destroy(gameObject);
    }
}
