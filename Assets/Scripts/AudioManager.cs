using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer generalAudioMixer;
    public Slider generalSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    private void Start()
    {

        generalSlider.value = PlayerPrefs.GetFloat("generalVolume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1);
        //generalAudioMixer.SetFloat("GeneralVolume", Mathf.Log10(PlayerPrefs.GetFloat("generalVolume") * 20f));
        //generalAudioMixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume") * 20f));
        //generalAudioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume") * 20f));
    }
    //Gestione dei volumi,Mathf perchè l'audio è gestito in maniera diversa rispetto agli slider
    public void SetMusicVolume(float volume)
    {
        generalAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetEffectsVolume(float volume)
    {
        generalAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void SetGeneralVolume(float volume)
    {
        generalAudioMixer.SetFloat("GeneralVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("generalVolume", volume);
    }
}
