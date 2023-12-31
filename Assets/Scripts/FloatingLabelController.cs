using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingLabelController : MonoBehaviour
{
    public Transform characterTransform;  // Drag your character's Transform here
    public TMP_Text floatingLabel;  // Drag your TextMesh Pro UGUI Text here
    public float heightOffset = 2.0f;  // Adjust this to position the label above your character
    public bool isInRange = false;
    public GameObject backgroundImage;

    private string resource;

    public static FloatingLabelController instance;

    private void Awake()
    {
        instance = this;
        ActivateLabe(false);
    }
    void Update()
    {
        // Position the label above the character
        Vector3 labelPosition = characterTransform.position;
        labelPosition.y += heightOffset;
        //floatingLabel.transform.position = Camera.main.WorldToScreenPoint(labelPosition);

        // Check for player input
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            ContainerManager.instance.GetResourceAsPlayer(resource, 1);
        }
        if (isInRange && Input.GetKeyDown(KeyCode.X))
        {
            ContainerManager.instance.DeleteSpecificResource(resource);
        }

    }

    public void ActivateLabe(bool aktivate)
    {
        backgroundImage.SetActive(aktivate);
        floatingLabel.gameObject.SetActive(aktivate);
        if (!aktivate)
        {
            floatingLabel.text = "";
        }
    }

    public void SetInRange(bool inRange)
    {
        isInRange = inRange;
    }

    public void SetStringContainer(string res)
    {
        resource = res;
        floatingLabel.text = $"{resource} resource \n [E] Collect \n [X] Delete";
    }

    public void SetStringObject(string text, string res)
    {
        resource = res;
        floatingLabel.text = text + resource;
    }

    public void SetText(string text)
    {
        floatingLabel.text = text;
    }

}
