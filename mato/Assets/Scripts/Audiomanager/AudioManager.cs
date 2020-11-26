using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	[Range(0f, 1f)]
	public float masterVolume;
	[Range(0f, 1f)]
	public float musicVolume;
	[Range(0f, 1f)]
	public float SFXVolume;


	public Sound[] sounds;

	//On awake, the script checks if there are any other instances of the audiomanager gameobject, if there are the new one is destroyed.
	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		//Creates the sounds that will be used by the audiomanager
		foreach (Sound SoundInstance in sounds)
		{
			SoundInstance.source = gameObject.AddComponent<AudioSource>();
			SoundInstance.source.clip = SoundInstance.clip;
			SoundInstance.source.loop = SoundInstance.loop;
			SoundInstance.source.volume = SoundInstance.volume;
			SoundInstance.volume = SoundInstance.volume * masterVolume;

		}

		//totally arbitrary values that will be replaced when the saving system is implemented
		//masterVolume = 0.5f;
		//musicVolume = 0.5f;
		//SFXVolume = 0.5f;

		//Plays the current gamemusic
		Play("MenuMusic");
	}

	//Finds the sound that we want it to play
	public void Play(string sound)
	{
		Sound SoundInstance = Array.Find(sounds, item => item.name == sound);
		if (SoundInstance == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		//uses the play function to play the sound if it can be found
		SoundInstance.source.Play();
	}

	//sets the master volume by applying it's multiplier between 0-1 to both of the 
	public void SetMasterVolume(float newMasterVolume){
		//sets the multiplier for all other volumes and then calls their functions to set themselves
		masterVolume = newMasterVolume;
		SetMusicVolume(musicVolume);
		SetSFXVolume(SFXVolume);
	}
	public void SetMusicVolume(float newMusicVolume){

		//sets the multiplier for all volumes that have the category music and then calculates their new volume by multiplying the values of musicvolume and mastervolume
		foreach (Sound SoundInstance in sounds)
		{
			if (SoundInstance.category == "music")
			{
				//SoundInstance.source.volume = SoundInstance.source.volume / oldMusicVolume * newMusicVolume;
				SoundInstance.source.volume = masterVolume * newMusicVolume;
				//lists all the sounds that were affected by this change in the debug log
				//UnityEngine.Debug.LogWarning(SoundInstance.name + " volume changed to " + SoundInstance.source.volume);
			}
			//lists all the sounds that were not affected by this change in the debug log
			//else UnityEngine.Debug.LogWarning(SoundInstance.name + " was not affected and its volume is " + SoundInstance.source.volume);

		}
		musicVolume = newMusicVolume;
	}
	public void SetSFXVolume(float newSFXVolume){

		//sets the multiplier for all volumes that have the category sfx and then calculates their new volume by multiplying the values of sfxvolume and mastervolume
		foreach (Sound SoundInstance in sounds)
		{
			if (SoundInstance.category == "sfx")
			{
				//SoundInstance.source.volume = SoundInstance.source.volume / oldSFXVolume * newSFXVolume;
				SoundInstance.source.volume = masterVolume * newSFXVolume;
				//lists all the sounds that were affected by this change in the debug log
				//UnityEngine.Debug.LogWarning(SoundInstance.name + " volume changed to " + SoundInstance.source.volume);
			}
			//lists all the sounds that were not affected by this change in the debug log
			//else UnityEngine.Debug.LogWarning(SoundInstance.name + " was not affected and its volume is " + SoundInstance.source.volume);

			SFXVolume = newSFXVolume;
		}
	}


}
