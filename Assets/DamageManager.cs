using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> shipObjects = new List<GameObject>();

    [SerializeField] private string currentEvent;

    public static DamageManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (shipObjects.Count > 0)
        {
            if(currentEvent == "Meteore")
            {
                // Generate a random index
                int randomIndex = Random.Range(0, shipObjects.Count);

                // Get the random GameObject
                GameObject randomGameObject = shipObjects[randomIndex];

                randomGameObject.GetComponent<FloatingHealthBar>().GetDamage(5);
                Destroy(collision.gameObject);
            }

        }
    }

    public void SetCurrentEvent(string newEvent)
    {
        currentEvent = newEvent;
    }
}
