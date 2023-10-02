using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StateManagerEngine : MachineManager
{
    public GameObject engine;
    public GameObject thruster1;
    public GameObject thruster2;

    public GameObject lightContainer;
    private HealthState currentState = HealthState.Healthy;

    // public float 
    private float currentTravelDistance = 0f;
    public override void OnHealthHealthy()
    {
        ParticleSystem ps = thruster1.GetComponent<ParticleSystem>();
        ParticleSystem ps2 = thruster2.GetComponent<ParticleSystem>();
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightGreen;
        light.color = lightGreen;
        
        var main = ps.main;
        var main2 = ps2.main;
        main.startSpeed = 10f;
        main.loop = true;
        main2.startSpeed = 10f;
        main2.loop = true;
    }
    public override void OnHealthDamaged()
    {
        ParticleSystem ps = thruster1.GetComponent<ParticleSystem>();
        ParticleSystem ps2 = thruster2.GetComponent<ParticleSystem>();
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightYellow;
        light.color = lightYellow;
        var main = ps.main;
        var main2 = ps2.main;
        main.loop = true;
        main2.loop = true;
    }
    public override void OnHealthBarelyOperable()
    {
        ParticleSystem ps = thruster1.GetComponent<ParticleSystem>();
        ParticleSystem ps2 = thruster2.GetComponent<ParticleSystem>();
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightRed;
        light.color = lightRed;
        var main = ps.main;
        var main2 = ps2.main;
        main.loop = true;
        main2.loop = true;
    }
    public override void OnHealthFuckedUp()
    {
        ParticleSystem ps = thruster1.GetComponent<ParticleSystem>();
        ParticleSystem ps2 = thruster2.GetComponent<ParticleSystem>();
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightDead;
        light.color = lightDead;
        var main = ps.main;
        var main2 = ps2.main;
        main.loop = false;
        main2.loop = false;
    }
}