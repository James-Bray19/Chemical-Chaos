using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionSelector : MonoBehaviour
{
    public List<PotionController> potions;
    public List<PotionShake> potionShakes;
    public List<Slider> ingredientSliders;
    public List<Slider> thermometers;
    public List<ParticleSystem> drips;
    public List<ParticleSystem> bubbles;
    public List<GameObject> pipettes;
    public float ingredientLoss;
    public float ingredientRegen;
    public float temperatureIncreaseRate;
    public float temperatureDecreaseRate;
    public float temperatureMax;
    public static int gainedPotionCount;
    public static int lostPotionCount;
    public static PotionController activePotion;
    public AudioManager audio;
    public SceneSelector scene;
    public static float[] temps;

    void Start() { activePotion = potions[0]; temps = new float[6]; }
    void Update()
    {
        for (int i = 0; i < 6; i++) { if (UIController.potionCount == i * 3) { potions[i].transform.parent.transform.parent.gameObject.SetActive(true); potions[i].isActive = true; } }
        
        // selects active potion
        PotionController temp = activePotion;
        if (Input.GetKeyDown("u")) { activePotion = potions[0]; }
        else if (Input.GetKeyDown("i")) { activePotion = potions[1]; }
        else if (Input.GetKeyDown("o")) { activePotion = potions[2]; }
        else if (Input.GetKeyDown("j")) { activePotion = potions[3]; }
        else if (Input.GetKeyDown("k")) { activePotion = potions[4]; }
        else if (Input.GetKeyDown("l")) { activePotion = potions[5]; }
        if(temp != activePotion && activePotion.transform.parent.transform.parent.gameObject.active == true) { audio.Select(); }

        foreach (Slider s in ingredientSliders) { s.value += ingredientRegen * Time.deltaTime; }

        AudioManager.isDripping = true;
        int activePotionIndex = potions.IndexOf(activePotion);
        pipettes[activePotionIndex].SetActive(true);

        if(activePotion.transform.parent.transform.parent.gameObject.active == true)
        {
            if (Input.GetKey("q") && Input.GetKey("w") && Input.GetKey("e"))
            {
                var em = drips[activePotionIndex].emission;
                var main = drips[activePotionIndex].main;
                main.startColor = new Color(1f, 1f, 1f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1 && ingredientSliders[2].value > 1)
                {
                    temps[activePotionIndex] -= temperatureDecreaseRate * Time.deltaTime;
                    AddColour(new Color(1f, 1f, 1f, 1f), activePotion.potionColor); // colour change
                    em.enabled = true; // particle system
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime; // slider
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime; // slider
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime; // slider
                }
                else { AudioManager.isDripping = false; em.enabled = false; }
            }
            else if (Input.GetKey("q") && Input.GetKey("w"))
            {
                var em = drips[activePotionIndex].emission;
                var main = drips[activePotionIndex].main;
                main.startColor = new Color(1f, 1f, 0f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1)
                {
                    temps[activePotionIndex] -= temperatureDecreaseRate * Time.deltaTime;
                    AddColour(new Color(1f, 1f, 0f, 1f), activePotion.potionColor); // colour change
                    em.enabled = true; // particle system
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime; // slider
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime; // slider
                }
                else { AudioManager.isDripping = false; em.enabled = false; }
            }
            else if (Input.GetKey("w") && Input.GetKey("e"))
            {
                var em = drips[activePotionIndex].emission;
                var main = drips[activePotionIndex].main;
                main.startColor = new Color(0f, 1f, 1f, 1f);
                if (ingredientSliders[1].value > 1 && ingredientSliders[2].value > 1)
                {
                    temps[activePotionIndex] -= temperatureDecreaseRate * Time.deltaTime;
                    AddColour(new Color(0f, 1f, 1f, 1f), activePotion.potionColor); // colour change
                    em.enabled = true; // particle system
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime; // slider
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime; // slider
                }
                else { AudioManager.isDripping = false; em.enabled = false; }
            }
            else if (Input.GetKey("q") && Input.GetKey("e"))
            {
                var em = drips[activePotionIndex].emission;
                var main = drips[activePotionIndex].main;
                main.startColor = new Color(1f, 0f, 1f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[2].value > 1)
                {
                    temps[activePotionIndex] -= temperatureDecreaseRate * Time.deltaTime;
                    AddColour(new Color(1f, 0f, 1f, 1f), activePotion.potionColor); // colour change
                    em.enabled = true; // particle system
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime; // slider
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime; // slider
                }
                else { AudioManager.isDripping = false; em.enabled = false; }
            }
            else if (Input.GetKey("q"))
            {
                var em = drips[activePotionIndex].emission;
                var main = drips[activePotionIndex].main;
                main.startColor = new Color(1f, 0f, 0f, 1f);
                if (ingredientSliders[0].value > 1)
                {
                    temps[activePotionIndex] -= temperatureDecreaseRate * Time.deltaTime;
                    AddColour(Color.red, activePotion.potionColor); // colour change
                    em.enabled = true; // particle system
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime; // slider
                }
                else { AudioManager.isDripping = false; em.enabled = false;}
            }
            else if (Input.GetKey("w"))
            {
                var em = drips[activePotionIndex].emission;
                var main = drips[activePotionIndex].main;
                main.startColor = new Color(0f, 1f, 0f, 1f);
                if (ingredientSliders[1].value > 1) 
                {
                    temps[activePotionIndex] -= temperatureDecreaseRate * Time.deltaTime;
                    AddColour(Color.green, activePotion.potionColor); // colour change
                    em.enabled = true; // particle system
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime; // slider
                }
                else { AudioManager.isDripping = false; em.enabled = false; }
            }
            else if (Input.GetKey("e"))
            {
                var em = drips[activePotionIndex].emission;
                var main = drips[activePotionIndex].main;
                main.startColor = new Color(0f, 0f, 1f, 1f);
                if (ingredientSliders[2].value > 1) 
                {
                    temps[activePotionIndex] -= temperatureDecreaseRate * Time.deltaTime;
                    AddColour(Color.blue, activePotion.potionColor); // colour change
                    em.enabled = true; // particle system
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime; // slider
                }
                else { AudioManager.isDripping = false; em.enabled = false; }
            }
            else
            {
                var em = drips[activePotionIndex].emission;
                AudioManager.isDripping = false;
                foreach (ParticleSystem ps in drips) { em.enabled = false; }
                foreach (GameObject obj in pipettes) { obj.SetActive(false); }
            }
        }
        else
        {
            var em = drips[activePotionIndex].emission;
            AudioManager.isDripping = false;
            foreach (ParticleSystem ps in drips) { em.enabled = false; }
            foreach (GameObject obj in pipettes) { obj.SetActive(false); }
        }

        foreach (PotionController p in potions)
        {
            if (activePotion != p)
            {
                var em = drips[potions.IndexOf(p)].emission;
                em.enabled = false;
                pipettes[potions.IndexOf(p)].SetActive(false);
            }
            if (p.isComplete) { temps[potions.IndexOf(p)] = 0; p.isComplete = false; }

        }

        float maxTempVal = 0;
        for (int i = 0; i < 6; i++)
        {
            if (temps[i] < 0) { temps[i] = 0; }
            
            if (potions[i].isActive) { temps[i] += temperatureIncreaseRate * FireLightScript.heat * Time.deltaTime; }
            float tempValue = temps[i] / temperatureMax;
            if (tempValue > maxTempVal) { maxTempVal = tempValue; }
            thermometers[i].value = tempValue;
            if (temps[i] >= temperatureMax) 
            { 
                potions[i].Vanish();
                if (lostPotionCount == 2) { lostPotionCount += 3; }
                else if (lostPotionCount == 5) { lostPotionCount += 5; }
                else if (lostPotionCount == 9) { lostPotionCount += 12; }
                else { lostPotionCount += 1; }
                temps[i] = 0; audio.Smash();
            }

            if (tempValue < 0.75f) { potionShakes[i].magnitude = 0; }
            else { potionShakes[i].magnitude = 4 * (tempValue - 0.75f); }

            var em = bubbles[i].emission;
            var main = bubbles[i].main;
            float expoTemp = 0;
            if (tempValue > 0.75f) { expoTemp = 4 * (tempValue - 0.75f); }
            em.rate = Mathf.Lerp(10f, 30f, tempValue) + Mathf.Lerp(0f, 30f, expoTemp);
            main.startLifetime = Mathf.Lerp(0.5f, 3f, tempValue);
            main.startColor = potions[i].potionColor;
        }

        if (maxTempVal < 0.75f) { audio.whistleVolume = 0; }
        else { audio.whistleVolume = 4*(maxTempVal-0.75f); }

        bool dead = true;
        for (int i = 0; i < 6; i++) { if (potions[i].transform.parent.transform.parent.gameObject.active == true) { dead = false; break; } }
        if (dead) 
        {
            StartCoroutine(WaitASecond());
            HighscoreManager.UpdateHighscore(UIController.potionCount); 
            scene.playHighscores();
        }
    }

    public void AddColour(Color colorToAdd, Color currentColor)
    // adds colour to a potion each frame
    {
        activePotion.potionColor = Color.Lerp(currentColor, colorToAdd, 0.8f * Time.deltaTime);
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(3);
    }
}