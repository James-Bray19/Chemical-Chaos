using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PotionLightScript : MonoBehaviour
{
    public PotionController potion;

    void Update()
    {
        GetComponent<Light2D>().color = potion.potionColor;
        GetComponent<Light2D>().intensity = Mathf.Lerp(0f, 8f, potion.potionColor.a);
    }
}
