using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool isDripping;
    public float whistleVolume;
    public AudioSource drips;
    public AudioSource bubbles;
    public AudioSource select;
    public AudioSource smash;
    public AudioSource whistle;
    public AudioSource coin;
    public AudioSource sizzle;
    public AudioSource theme;

    bool themePlayed = false;

    void Update()
    {
        if (isDripping)
        {
            if (!drips.isPlaying) { drips.Play(); }
        }
        else { drips.Stop(); }

        whistle.volume = whistleVolume;
    }

    public void Select() { select.Play(); }

    public void Smash() { smash.Play(); }
    public void Coin() { coin.Play(); }
    public void Sizzle() { sizzle.Play(); }
    public void PlayTheme() { theme.Play(); bubbles.Play(); }
}