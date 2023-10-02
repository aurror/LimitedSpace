using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Ship Objects")]
    [SerializeField] private List<GameObject> shipObjects = new List<GameObject>();
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject oxygen;
    private GameObject shield;
    private FloatingHealthBar shieldHealthBar;

    [Header("Events")]
    [SerializeField] private EventManager.GameEvent currentEvent;
    [SerializeField] private List<StringIntPair> eventDamage = new List<StringIntPair>();
    [SerializeField] private int solarFlairTime;

    public static DamageManager instance;

    private GameObject explosionPrefab;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        explosionPrefab = Resources.Load<GameObject>("Explosion");
        shield = GameObject.Find("Shield");
        shieldHealthBar = shield.GetComponent<FloatingHealthBar>();
    }
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.V))
    //     {
    //         SolarFlair(20);
    //     }
  
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (shipObjects.Count > 0)
        {

            if (collision.gameObject.name.Contains("Asteroid")){
                foreach(StringIntPair pair in eventDamage)
                {
                    if(pair.stringValue == "Meteore")
                    {
                        MeteoreEvent(collision.gameObject, pair.intValue);
                    }
                }
            }
            if (collision.gameObject.name.Contains("LaserShotPirate")){
                foreach(StringIntPair pair in eventDamage)
                {
                    if(pair.stringValue == "PirateAttack")
                    {
                        EnemyAttack(collision.gameObject, pair.intValue);
                    }
                }
            }

        }
    }

    public void SetCurrentEvent(EventManager.GameEvent newEvent)
    {
        currentEvent = newEvent;
    }

    private void MeteoreEvent(GameObject gameObject, int damage)
    {
        if (shieldHealthBar.GetHealth() > 0)
        {
            shieldHealthBar.GetDamage(damage);

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
            gameObject.GetComponent<Enemy>().TakeDamage(1000);
            //Destroy(gameObject);
    }

    private Coroutine activeOxygenCoroutine;
    public void StartOxygenDamage()
    {
        activeOxygenCoroutine = StartCoroutine(IncrementDamage(20, 5, oxygen));
    }
    public void StopOxygenDamage()
    {
        if (activeOxygenCoroutine != null){
            StopCoroutine(activeOxygenCoroutine);
        }
    }
    public GameObject solarFlare;
    public void SolarFlair(float time)
    {
        solarFlare.SetActive(true);
        foreach (StringIntPair pair in eventDamage)
        {
            if (pair.stringValue == "SolarFlair")
            {
                StartCoroutine(IncrementDamage(time, pair.intValue, generator));
            }
        }
    }

    IEnumerator IncrementDamage(float time, int damage, GameObject gameObject)
    {
        while (time > 0)
        {
            gameObject.GetComponent<FloatingHealthBar>().GetDamage(damage);
            time -= 1;
            yield return new WaitForSeconds(1.0f);
        }
        solarFlare.SetActive(false);
    }

    public void EnemyAttack(GameObject gameObject, int damage)
    {
        if (shieldHealthBar.GetHealth() > 0)
        {
            shieldHealthBar.GetDamage(damage);
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
        {
            Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
            
    }
}

        
