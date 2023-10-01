using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public enum GameEvent
    {
        None,
        AsteroidField,
        SolarFlare,
        PirateAttack,
        RareTreasureFind,
        DimensionalRift
    }

    TextMeshProUGUI warningHeader;
    TextMeshProUGUI warningSubHeader;
    public float minEventInterval = 15f;
    public float maxEventInterval = 25f;

    private float nextEventTime;
    private GameEvent lastEvent = GameEvent.None;
    private int eventsSinceLastTreasure = 0;



    private void Start()
    {
        ScheduleNextEvent();
        warningHeader = GameObject.Find("WarningHeader").GetComponent<TextMeshProUGUI>();
        warningSubHeader = GameObject.Find("WarningSubHeader").GetComponent<TextMeshProUGUI>();
    }

    private void FlashWarning(string header, string subHeader)
    {
        warningHeader.text = header;
        warningSubHeader.text = subHeader;
        StartCoroutine(FlashWarningCoroutine());
    }
    private IEnumerator FlashWarningCoroutine()
    {
        float transitionDuration = 0.7f;  // The duration of each fade in/out
        float targetAlpha = 0.3f;  // The alpha value for "not so visible"
        int flashCount = 3;  // The number of times to flash the text

        for (int i = 0; i < flashCount; i++)
        {
            // Fade out
            for (float t = 0; t < transitionDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1f, targetAlpha, t / transitionDuration);
                warningHeader.color = new Color(warningHeader.color.r, warningHeader.color.g, warningHeader.color.b, alpha);
                warningSubHeader.color = new Color(warningSubHeader.color.r, warningSubHeader.color.g, warningSubHeader.color.b, alpha);
                yield return null;
            }

            // Ensure the alpha is exactly the target alpha at the end of the fade out
            warningHeader.color = new Color(warningHeader.color.r, warningHeader.color.g, warningHeader.color.b, targetAlpha);
            warningSubHeader.color = new Color(warningSubHeader.color.r, warningSubHeader.color.g, warningSubHeader.color.b, targetAlpha);

            // Fade in
            for (float t = 0; t < transitionDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(targetAlpha, 1f, t / transitionDuration);
                warningHeader.color = new Color(warningHeader.color.r, warningHeader.color.g, warningHeader.color.b, alpha);
                warningSubHeader.color = new Color(warningSubHeader.color.r, warningSubHeader.color.g, warningSubHeader.color.b, alpha);
                yield return null;
            }

            // Ensure the alpha is exactly 1 at the end of the fade in
            warningHeader.color = new Color(warningHeader.color.r, warningHeader.color.g, warningHeader.color.b, 1f);
            warningSubHeader.color = new Color(warningSubHeader.color.r, warningSubHeader.color.g, warningSubHeader.color.b, 1f);
        }

        // Final fade out to invisible
        for (float t = 0; t < transitionDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / transitionDuration);
            warningHeader.color = new Color(warningHeader.color.r, warningHeader.color.g, warningHeader.color.b, alpha);
            warningSubHeader.color = new Color(warningSubHeader.color.r, warningSubHeader.color.g, warningSubHeader.color.b, alpha);
            yield return null;
        }

        // Ensure the alpha is exactly 0 at the end of the final fade out
            warningHeader.color = new Color(warningHeader.color.r, warningHeader.color.g, warningHeader.color.b, 0f);
            warningSubHeader.color = new Color(warningSubHeader.color.r, warningSubHeader.color.g, warningSubHeader.color.b, 0f);
   
    }

    private void ScheduleNextEvent()
    {
        nextEventTime = Time.time + Random.Range(minEventInterval, maxEventInterval);
        StartCoroutine(EventCoroutine());
    }

    private IEnumerator EventCoroutine()
    {
        yield return new WaitForSeconds(nextEventTime - Time.time);
        TriggerRandomEvent();
        ScheduleNextEvent();
    }

    private void TriggerRandomEvent()
    {
        List<GameEvent> possibleEvents = new List<GameEvent>()
        {
            GameEvent.AsteroidField,
            GameEvent.SolarFlare,
            GameEvent.PirateAttack
        };

        if (eventsSinceLastTreasure >= 5)
        {
            possibleEvents.Add(GameEvent.RareTreasureFind);
        }

        // Remove the last event from possible events to avoid repetition
        if (lastEvent != GameEvent.None)
        {
            possibleEvents.Remove(lastEvent);
        }

        // Randomly select a new event from the remaining possible events
        GameEvent randomEvent = possibleEvents[Random.Range(0, possibleEvents.Count)];

        switch (randomEvent)
        {
            case GameEvent.AsteroidField:
                // Trigger Asteroid Field event
                FlashWarning("! Warning !", "Asteroids incoming");
                GetComponent<AsteroidSpawner>().EnableEvent(Random.Range(minEventInterval,maxEventInterval));
                break;
            case GameEvent.SolarFlare:
                // Trigger Solar Flare event
               // GetComponent<SolarFlare>().EnableEvent(Random.Range(minEventInterval,maxEventInterval));
                break;
            case GameEvent.PirateAttack:
                // Trigger Pirate Attack event
               // GetComponent<PirateAttackers>().EnableEvent(Random.Range(minEventInterval,maxEventInterval));
                break;
            case GameEvent.RareTreasureFind:
                // Reset the counter when a treasure event occurs
                eventsSinceLastTreasure = 0;
                // Trigger Rare Treasure Find event
                break;
            case GameEvent.DimensionalRift:

                break;
        }

        lastEvent = randomEvent;  // Update the last event
        eventsSinceLastTreasure++;  // Increment the counter for each event
    }
}