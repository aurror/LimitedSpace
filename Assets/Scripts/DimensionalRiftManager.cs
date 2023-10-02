using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalRiftManager : MonoBehaviour
{


    [SerializeField] private GameObject blackkHole;
    [SerializeField] private List<GameObject> portals;
    [SerializeField] private float timeIntervallBlackHoleAction;

    private bool eventEnabled = false;
    private float remainingEventTime = 0f;
    public static DimensionalRiftManager instance;
    private bool eventIsRunning;
    void Awake()
    {
        ActivateObjects(false);
        instance = this;
        eventIsRunning = false;
    }
    void Update()
    {
        if (eventEnabled && !eventIsRunning)
        {
                ActivateBlackHole();
        }
        if (eventEnabled)
        {
            remainingEventTime -= Time.deltaTime;
            if (remainingEventTime <= 0f)
            {
                eventEnabled = false;
                DeaktivateBlackHole();
            }
        }

        
    }

    public void EnableEvent(float duration)
    {
        this.remainingEventTime = duration;
        this.eventEnabled = true;
    }

    private void ActivateBlackHole()
    {
        ActivateObjects(true);
        eventIsRunning = true;
        StartCoroutine(TimeBetweenBlackHoleAction());
       
    }

    private IEnumerator TimeBetweenBlackHoleAction()
    {
        while (eventIsRunning)
        {
            ContainerManager.instance.DeleteRandomResource();
            yield return new WaitForSeconds(timeIntervallBlackHoleAction);
        }
    }
        

    private void DeaktivateBlackHole()
    {
        ActivateObjects(false);
        eventIsRunning = false;
        remainingEventTime = 0;
    }

    private void ActivateObjects(bool aktivate)
    {
        blackkHole.SetActive(aktivate);
        foreach (GameObject gO in portals)
        {
            gO.SetActive(aktivate);
        }
    }
}
