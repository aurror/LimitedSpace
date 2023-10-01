using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public GameObject heartContainerPrefab;
    public Transform healthBar;
    public float oxygenDepletionRate = 2f;
    private bool isOxygenAvailable = true;
    
    private void Start()
    {
        currentHealth = maxHealth;
        InitializeHealthBar();
    }

    private void InitializeHealthBar()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Instantiate(heartContainerPrefab, healthBar);
        }
    }

    private void Update()
    {
        if (!isOxygenAvailable)
        {
            StartCoroutine(DepleteHealth());
        }
    }

    private IEnumerator DepleteHealth()
    {
        while (!isOxygenAvailable && currentHealth > 0)
        {
            yield return new WaitForSeconds(oxygenDepletionRate);
            currentHealth--;
            UpdateHeartContainer();
        }

        if (currentHealth <= 0)
        {
            // Player Dies
        }
    }

    private void UpdateHeartContainer()
    {
        for (int i = 0; i < healthBar.childCount; i++)
        {
            Animator heartAnimator = healthBar.GetChild(i).GetComponent<Animator>();
            if (i < currentHealth)
            {
                heartAnimator.SetTrigger("Pulsate");
            }
            else
            {
                heartAnimator.SetTrigger("FadeOut");
            }
        }
    }

    public void RecoverOxygen()
    {
        isOxygenAvailable = true;
        // You may want to add a coroutine to brighten the hearts before fading them out.
    }
}