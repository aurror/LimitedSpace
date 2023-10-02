using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public GameObject heartContainerPrefab;
    private Image heartImage;
     private Color originalColor;
    public GameObject DeathScreen;
     private float initdelay = 0.5f;
      public float colorDecreaseRate = 0.2f;  // Adjust this value to control the rate of color change

    public float oxygenDepletionRate = 2f;
    private bool isOxygenAvailable = true;
    
    private void Start()
    {
        if (heartContainerPrefab != null)
        {
            heartImage = heartContainerPrefab.GetComponent<Image>();
            if (heartImage != null)
            {
                originalColor = heartImage.color;
            }
            else
            {
                Debug.LogError("No Image component found on heartContainerPrefab.");
            }
        }
        else
        {
            Debug.LogError("heartContainerPrefab not assigned.");
        }
    }
 private void Update()
    {
        if (!isOxygenAvailable && heartImage != null)
        {
            if (heartContainerPrefab.activeSelf == false)
            {
                heartContainerPrefab.SetActive(true);
                StartCoroutine(DecreaseColorOverTime());
            }
        }
        else
        {
            if (heartContainerPrefab.activeSelf == true)
            {
                heartImage.color = originalColor;
                heartContainerPrefab.SetActive(false);
                StopCoroutine(DecreaseColorOverTime());
            }
        }
    }

    private IEnumerator DecreaseColorOverTime()
    {
        yield return new WaitForSeconds(initdelay);
        while (!isOxygenAvailable && heartImage.color.r > 0 && heartImage.color.g > 0 && heartImage.color.b > 0)
        {
            currentHealth--;
            
            if (currentHealth <= 0)
            {
                // Player Dies
                Debug.Log("Ripperonis in pepperonis");

                heartContainerPrefab.GetComponent<Animator>().enabled = false;

                GameObject.Find("Player").GetComponent<Movement>().enabled = false;
                GameObject.Find("Player").GetComponent<Animator>().SetTrigger("PlayerDeath");
                //DeathScreen.SetActive(true);
                StopCoroutine(DecreaseColorOverTime());
                break;
            }
            Color currentColor = heartImage.color;
            currentColor.r -= colorDecreaseRate;
            currentColor.g -= colorDecreaseRate;
            currentColor.b -= colorDecreaseRate;
            heartImage.color = currentColor;
            yield return new WaitForSeconds(1f);
        }
    }


    public void StartSuffocating(){
        isOxygenAvailable = false;
    }
    public void StopSuffocating()
    {
        isOxygenAvailable = true;
        // You may want to add a coroutine to brighten the hearts before fading them out.
    }
}