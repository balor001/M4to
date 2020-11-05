using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This script receives the sliders values and sends them to audiomanager to change the volume*/
public class SliderReader : MonoBehaviour
{
    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    public GameObject audioHere;
    

    public void Awake()
    {
        /*This sets the sliders in the appropriate spots
         *By first finding the audiomanager that manages the main volume modifiers*/
        audioHere = GameObject.Find("AudioManager");

        //It sets the audiomanager into an audiomanager(script) class
        AudioManager audioHereReal = audioHere.GetComponent<AudioManager>();

        //And then assigns the values to the appropriate values(float)
        masterVolume = audioHereReal.masterVolume;
        musicVolume = audioHereReal.musicVolume;
        SFXVolume = audioHereReal.SFXVolume;

        //And then changing the values of the sliders in scene
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        SFXSlider.value = SFXVolume;
    }

    //Changes the master volume by receiving a float and then sending it to audiomanager
    public void ChangeMasterVol(float volume)
    {
        masterVolume = volume;
        FindObjectOfType<AudioManager>().SetMasterVolume(volume);
    }

    //Changes the music volume by receiving a float and then sending it to audiomanager
    public void ChangeMusicVol(float volume)
    {
        musicVolume = volume;
        FindObjectOfType<AudioManager>().SetMusicVolume(volume);
    }

    //Changes the effects volume by receiving a float and then sending it to audiomanager
    public void ChangeSFXVol(float volume)
    {
        SFXVolume = volume;
        FindObjectOfType<AudioManager>().SetSFXVolume(volume);
    }

}
