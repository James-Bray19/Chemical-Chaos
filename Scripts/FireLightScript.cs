using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireLightScript : MonoBehaviour
{
    public List<UnityEngine.Experimental.Rendering.Universal.Light2D> lights;
    static float timer = 0.0f;
    public static float heat;

    void Update()
    {
        timer += Time.deltaTime;
        UIController.heatLevel = (int)(Mathf.InverseLerp(0f, 180, timer) * 100);

        foreach(UnityEngine.Experimental.Rendering.Universal.Light2D light in lights)
        {
            light.intensity = Mathf.Lerp(5f, 15f, Mathf.InverseLerp(0f, 180, timer));
            light.pointLightOuterRadius = Mathf.Lerp(1f, 2f, Mathf.InverseLerp(0f, 300, timer));
        }
        heat = Mathf.InverseLerp(0f, 180, timer) * (0.2f * PotionSelector.lostPotionCount + 1);
    }
}
