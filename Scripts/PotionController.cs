using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public Color potionColor;
    public Color goalColor;
    public GameObject obj;
    public GameObject goal;
    public ParticleSystem ps;
    public bool isActive;
    public bool isComplete;
    public AudioManager audio;

    void Start()
    {
        goalColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        goal.GetComponent<SpriteRenderer>().color = goalColor;
        potionColor = new Color(1f, 1f, 1f, 0.2f);
        isActive = true;
    }

    void Update()
    {
        var main = ps.main;
        GetComponent<SpriteRenderer>().color = potionColor;
        if (PotionSelector.activePotion == this) { obj.transform.localScale = new Vector3(1f, 1f, 1f); }
        else 
        {
            potionColor = Color.Lerp(potionColor, new Color(0.5f, 0.5f, 0.5f, 0.2f), 0.05f * Time.deltaTime);
            obj.transform.localScale = new Vector3(.9f, .9f, .9f); 
        }

        float minPotionRGB = Mathf.Min(potionColor.r, potionColor.g, potionColor.b);
        float minGoalRGB = Mathf.Min(potionColor.r, potionColor.g, potionColor.b);
        float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
              g = Mathf.Abs((potionColor.g) - (goalColor.g)),
              b = Mathf.Abs((potionColor.b) - (goalColor.b));
        if ((r + g + b) <= 0.2f) 
        {
            isComplete = true;
            audio.Coin();
            if (goalColor == Color.white) { PotionSelector.temps = new float[6]; audio.Sizzle(); }
            main.startColor = goalColor;
            ps.Play();
            UIController.potionCount += 1;
            potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);

            int temp = Random.Range(0, 20);
            if (temp == 1) { goalColor = Color.white; }
            else { goalColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f); }
            goal.GetComponent<SpriteRenderer>().color = goalColor;
        }
    }

    public void Vanish()
    {
        var main = ps.main;
        main.startColor = new Color(0.42f, 0.5f, 0.5f, 1f);
        ps.transform.SetParent(transform.parent.transform.parent.transform.parent);
        ps.Play();
        transform.parent.transform.parent.gameObject.SetActive(false);
        isActive = false;
    }
}
