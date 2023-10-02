using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private GameObject mainLight;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private GameObject generator;

    private static LightManager instance;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        float f = generator.GetComponent<FloatingHealthBar>().currentHealth;
        ChangeLightIntensity(f/100);
    }

    public void ChangeLightIntensity(float intensity)
    {
        mainLight.GetComponent<Light2D>().intensity = intensity;
        pointLight.GetComponent<Light2D>().intensity = 1 - intensity;
    }
}
