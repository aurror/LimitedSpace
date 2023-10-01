using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
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
    private void UpdateHealthBar(float currentValue)
    {
        slider.value = currentValue / maxValue;
        UpdateColor();
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar(currentHealth);
    }

    public void GetHealth(int health)
    {
        currentHealth += health;
        if(currentHealth > maxValue)
        {
            currentHealth = maxValue;
        }
        UpdateHealthBar(currentHealth);
    }

    private void UpdateColor()
    {
        if(currentHealth > 66)
        {
            fill.color = Color.green;
            return;
        }
        if(currentHealth < 33)
        {
            fill.color = Color.red;
            return;
        }
        fill.color = Color.yellow;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Assuming the trigger is a pickup item
            FloatingLabelController.instance.ActivateLabe(true);
            FloatingLabelController.instance.SetInRange(true);
            FloatingLabelController.instance.SetStringObject("Need: ", myPairs[0].stringValue);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FloatingLabelController.instance.ActivateLabe(false);
            FloatingLabelController.instance.SetInRange(false);
        }
    }

}

