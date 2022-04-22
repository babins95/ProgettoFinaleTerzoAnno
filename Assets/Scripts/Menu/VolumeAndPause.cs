using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeAndPause : MonoBehaviour
{
    public AudioMixer musicAudioMixer;
    public AudioMixer effectsAudioMixer;
    public Canvas pauseMenu;

    //Gestione dei volumi e del pulsante di pausa
    public void SetMusicVolume(float volume)
    {
        musicAudioMixer.SetFloat("volume", volume);
    }
    public void SetEffectsVolume(float volume)
    {
        effectsAudioMixer.SetFloat("volume", volume);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }
}
