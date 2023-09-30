using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public int targetFPS = 144;

    void Start()
    {
        Application.targetFrameRate = targetFPS;
    }
}