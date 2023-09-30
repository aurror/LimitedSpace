using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private int maxValue;

    private int currentHealt;
    public static FloatingHealthBar instance;


    private void Awake()
    {
        instance = this;
        currentHealt = 100;
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
    }
    private void UpdateHealthBar(float currentValue)
    {
        slider.value = currentValue / maxValue;
        UpdateColor();
    }

    public void GetDamage(int damage)
    {
        currentHealt -= damage;
        if (currentHealt < 0)
        {
            currentHealt = 0;
        }
        UpdateHealthBar(currentHealt);
    }

    public void GetHealth(int health)
    {
        currentHealt += health;
        if(currentHealt > maxValue)
        {
            currentHealt = maxValue;
        }
        UpdateHealthBar(currentHealt);
    }

    private void UpdateColor()
    {
        if(currentHealt > 66)
        {
            fill.color = Color.green;
            return;
        }
        if(currentHealt < 33)
        {
            fill.color = Color.red;
            return;
        }
        fill.color = Color.yellow;
    }

}

