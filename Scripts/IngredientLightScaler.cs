using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientLightScaler : MonoBehaviour
{
    public Slider slider;
    void Update()
    {
        transform.localScale = new Vector3(1.31406f, Mathf.Lerp(0.24f, 3.06f, slider.value/100), 0f);
    }
}
