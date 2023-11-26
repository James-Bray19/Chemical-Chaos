using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class TutorialController : MonoBehaviour
{
    public Text tutorialText;
    public int tutorialStage;
    public bool spaceLock;

    public float ingredientLoss;
    public float ingredientRegen;
    public float temperatureIncreaseRate;
    public float temperatureDecreaseRate;
    public float temperatureMax;
    public float temperature;

    public List<Slider> ingredientSliders;
    public Slider thermometer;
    public Light2D goalLight;
    public Light2D potionLight;
    public ParticleSystem drips;
    public ParticleSystem bubbles;
    public Color potionColor;
    public Color goalColor;
    public GameObject obj;
    public GameObject liquid;
    public GameObject goal;
    public GameObject ingr;
    public GameObject ingrLights;
    public GameObject pipette;
    public GameObject timer;
    public GameObject potCount;
    public GameObject heat;
    public GameObject space;
    public ParticleSystem ps;
    public bool isActive;
    public AudioManager audio;
    public PotionShake potionShakes;
    public SceneSelector scene;

    void Update()
    {
        foreach (Slider s in ingredientSliders) { s.value += ingredientRegen * Time.deltaTime; }

        if (Input.GetKeyDown("space") && !spaceLock) { tutorialStage += 1; audio.Select(); }
        if (spaceLock) { space.SetActive(false); }
        else { space.SetActive(true); }

        if (tutorialStage == 1) { tutorialText.text = "Let's learn some chemistry!"; }
        else if (tutorialStage == 2)
        {
            spaceLock = true;
            tutorialText.text = "First, select your potion by pressing the letter above it";
            if (Input.GetKey("u")) { obj.transform.localScale = new Vector3(1f, 1f, 1f); audio.Select(); spaceLock = false; tutorialStage += 1; }
        }
        else if (tutorialStage == 3) { tutorialText.text = "Great, now you're free to make some colours!"; }
        else if (tutorialStage == 4) { tutorialText.text = "These are your ingredients. They refill, but be careful how much you use!"; ingr.SetActive(true); ingrLights.SetActive(true); }
        else if (tutorialStage == 5)
        {
            spaceLock = true;
            goalColor = new Color(0f, 0f, 1f, 1f);
            tutorialText.text = "To add a colour, hold the corresponding button. Try and make a blue potion";
            if (Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 0f, 1f, 1f);
                if (ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.blue, potionColor);
                    em.enabled = true;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = new Color(1f, 0f, 0f, 1f);
                tutorialStage += 1;
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 6)
        {
            spaceLock = true;
            goalColor = new Color(1f, 0f, 0f, 1f);
            tutorialText.text = "Now try and make a red potion";
            if (Input.GetKey("q"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 0f, 0f, 1f);
                if (ingredientSliders[0].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.red, potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = new Color(1f, 1f, 0f, 1f);
                tutorialStage += 1; spaceLock = false;
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 7) { tutorialText.text = "Great! Now let's try something a bit more difficult"; }
        else if (tutorialStage == 8)
        {
            spaceLock = true;
            goalColor = new Color(1f, 1f, 0f, 1f);
            tutorialText.text = "To make yellow, you need to hold down both green and red at the same time";
            if (Input.GetKey("q") && Input.GetKey("w"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 1f, 0f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 1f, 0f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = new Color(0f, 1f, 1f, 1f);
                tutorialStage += 1;
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 9)
        {
            spaceLock = true;
            goalColor = new Color(0f, 1f, 1f, 1f);
            tutorialText.text = "Green and blue make cyan";
            if (Input.GetKey("w") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 1f, 1f, 1f);
                if (ingredientSliders[1].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(0f, 1f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = new Color(1f, 0f, 1f, 1f);
                tutorialStage += 1;
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 10)
        {
            spaceLock = true;
            goalColor = new Color(1f, 0f, 1f, 1f);
            tutorialText.text = "You're getting the hang of this! Guess which two colours make magenta";
            if (Input.GetKey("q") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 0f, 1f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 0f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = new Color(1f, 0.5f, 0f, 1f);
                tutorialStage += 1;
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 11)
        {
            spaceLock = true;
            goalColor = new Color(1f, 0.5f, 0f, 1f);
            tutorialText.text = "Amazing, if you know how to make yellow, and you have red, try to make orange";
            if (Input.GetKey("q") && Input.GetKey("w"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 0.5f, 0f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 1f, 0f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("q"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 0f, 0f, 1f);
                if (ingredientSliders[0].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.red, potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = new Color(1f, 1f, 1f, 1f);
                tutorialStage += 1; spaceLock = false;
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 12) { tutorialText.text = "White is a bonus colour, and it's rarer than any colour"; }
        else if (tutorialStage == 13) { tutorialText.text = "White potions have a special ability, but we will learn about that later"; }
        else if (tutorialStage == 14)
        {
            spaceLock = true;
            goalColor = new Color(1f, 1f, 1f, 1f);
            tutorialText.text = "Complete the potion by adding all the ingredients at once";
            if (Input.GetKey("q") && Input.GetKey("w") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 1f, 1f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 1f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
                tutorialStage += 1; spaceLock = false;
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 15) { tutorialText.text = "Now is your chance to practice your potion making skills..."; }
        else if (tutorialStage == 16) { tutorialText.text = "A series of random colours will appear, do your best to match them!"; }
        else if (tutorialStage == 17)
        {
            spaceLock = true;
            tutorialText.text = "";
            if (Input.GetKey("q") && Input.GetKey("w") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 1f, 1f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 1f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("q") && Input.GetKey("w"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 1f, 0f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 1f, 0f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("w") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 1f, 1f, 1f);
                if (ingredientSliders[1].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(0f, 1f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("q") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 0f, 1f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 0f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("q"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 0f, 0f, 1f);
                if (ingredientSliders[0].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.red, potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("w"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 1f, 0f, 1f);
                if (ingredientSliders[1].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.green, potionColor);
                    em.enabled = true;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 0f, 1f, 1f);
                if (ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.blue, potionColor);
                    em.enabled = true;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                spaceLock = false;
                tutorialStage += 1;
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 18)
        {
            tutorialText.text = "Press space when you're done practicing";
            if (Input.GetKey("q") && Input.GetKey("w") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 1f, 1f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 1f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("q") && Input.GetKey("w"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 1f, 0f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[1].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 1f, 0f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("w") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 1f, 1f, 1f);
                if (ingredientSliders[1].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(0f, 1f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("q") && Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 0f, 1f, 1f);
                if (ingredientSliders[0].value > 1 && ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(new Color(1f, 0f, 1f, 1f), potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("q"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(1f, 0f, 0f, 1f);
                if (ingredientSliders[0].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.red, potionColor);
                    em.enabled = true;
                    ingredientSliders[0].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("w"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 1f, 0f, 1f);
                if (ingredientSliders[1].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.green, potionColor);
                    em.enabled = true;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else if (Input.GetKey("e"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 0f, 1f, 1f);
                if (ingredientSliders[2].value > 1)
                {
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.blue, potionColor);
                    em.enabled = true;
                    ingredientSliders[2].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float r = Mathf.Abs((potionColor.r) - (goalColor.r)),
                  g = Mathf.Abs((potionColor.g) - (goalColor.g)),
                  b = Mathf.Abs((potionColor.b) - (goalColor.b));
            if ((r + g + b) <= 0.2f)
            {
                spaceLock = false;
                AudioManager.isDripping = false;
                audio.Coin();
                var main = ps.main;
                main.startColor = goalColor;
                var em = drips.emission;
                em.enabled = false; pipette.SetActive(false);
                ps.Play();
                potionColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                goalColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;

            var bubmain = bubbles.main;
            bubmain.startColor = potionColor;
        }
        else if (tutorialStage == 19) { tutorialText.text = "The final thing you need to know about is a potion's temperature"; }
        else if (tutorialStage == 20) { tutorialText.text = "There's a thermometer to the right of each potion"; }
        else if (tutorialStage == 21) { tutorialText.text = "Dont let potions overboil!"; temperature = 0; spaceLock = false; }
        else if (tutorialStage == 22)
        {
            spaceLock = true;
            goalColor = new Color(0f, 1f, 0f, 1f);
            tutorialText.text = "Cool a potion down by adding ingredients";
            if (Input.GetKey("w"))
            {
                pipette.SetActive(true);
                var em = drips.emission;
                var main = drips.main;
                main.startColor = new Color(0f, 1f, 0f, 1f);
                if (ingredientSliders[1].value > 1)
                {
                    temperature -= temperatureDecreaseRate * Time.deltaTime;
                    AudioManager.isDripping = true;
                    potionColor = AddColour(Color.green, potionColor);
                    em.enabled = true;
                    ingredientSliders[1].value -= ingredientLoss * Time.deltaTime;
                }
                else { AudioManager.isDripping = false; em.enabled = false; pipette.SetActive(false); }
            }
            else
            {
                var em = drips.emission; em.enabled = false;
                AudioManager.isDripping = false;
                pipette.SetActive(false);
            }

            float maxTempVal = 0;
            if (temperature < 0) 
            {
                spaceLock = false;
                var em = drips.emission;
                var main = drips.main;
                temperature = 0; tutorialStage += 1; 
                AudioManager.isDripping = false; 
                em.enabled = false; 
                pipette.SetActive(false); 
            }

            temperature += temperatureIncreaseRate * Time.deltaTime;
            float tempValue = temperature / temperatureMax;
            thermometer.value = tempValue;
            if (temperature >= temperatureMax)
            {
                temperature = 0f; audio.Smash();
                tutorialStage -= 1;
                tempValue = 0;
                var main = ps.main;
                main.startColor = new Color(0.42f, 0.5f, 0.5f, 1f);
                ps.Play();
            }

            var bubem = bubbles.emission;
            var bubmain = bubbles.main;
            float expoTemp = 0;
            if (tempValue > 0.75f) { expoTemp = 4 * (tempValue - 0.75f); }
            audio.whistleVolume = expoTemp;
            potionShakes.magnitude = expoTemp;
            bubem.rate = Mathf.Lerp(10f, 30f, tempValue) + Mathf.Lerp(0f, 30f, expoTemp);
            bubmain.startLifetime = Mathf.Lerp(0.5f, 3f, tempValue);
            bubmain.startColor = potionColor;

            potionLight.color = potionColor;
            potionLight.intensity = Mathf.Lerp(0f, 8f, potionColor.a);
            goalLight.color = goalColor;
            goalLight.intensity = Mathf.Lerp(0f, 8f, goalColor.a);
            liquid.GetComponent<SpriteRenderer>().color = potionColor;
            goal.GetComponent<SpriteRenderer>().color = goalColor;
        }
        else if (tutorialStage == 23) { tutorialText.text = "Remember the white potion's special ability?"; }
        else if (tutorialStage == 24) { tutorialText.text = "White potions cool down all of your potions instantly!"; }
        else if (tutorialStage == 25) { tutorialText.text = "The time is how long your game is"; timer.SetActive(true); }
        else if (tutorialStage == 26) { tutorialText.text = "The potion count is how many potions you've completed"; potCount.SetActive(true); }
        else if (tutorialStage == 27) { tutorialText.text = "The heat is how fast the temperature increases"; heat.SetActive(true); }
        else if (tutorialStage == 28) { tutorialText.text = "The more potions you let overboil, the higher the heat will be"; }
        else if (tutorialStage == 29) { tutorialText.text = "Thank you for playing the tutorial! Good luck!"; }
        else if (tutorialStage == 30) { scene.playMenu(); }
    }

    public Color AddColour(Color colorToAdd, Color currentColor)
    {
        return Color.Lerp(currentColor, colorToAdd, 0.8f * Time.deltaTime);
    }
}
