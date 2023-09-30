using TMPro;
using UnityEngine;

public class FloatingLabelController : MonoBehaviour
{
    public Transform characterTransform;  // Drag your character's Transform here
    public TMP_Text floatingLabel;  // Drag your TextMesh Pro UGUI Text here
    public float heightOffset = 2.0f;  // Adjust this to position the label above your character
    private bool isInRange = false;

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
        floatingLabel.transform.position = Camera.main.WorldToScreenPoint(labelPosition);

        // Check for player input
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            ContainerManager.instance.GetResourceAsPlayer(resource, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Assuming the trigger is a pickup item
        floatingLabel.gameObject.SetActive(true);
        isInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        floatingLabel.gameObject.SetActive(false);
        isInRange = false;
    }

    public void ActivateLabe(bool aktivate)
    {
        floatingLabel.gameObject.SetActive(aktivate);
    }

    public void SetInRange(bool inRange)
    {
        isInRange = inRange;
    }

    public void SetResource(string res)
    {
        resource = res;
    }
}
