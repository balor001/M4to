using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data_Settings
{
	// Sound Volumes
	public float SoundMaster;
	public float SoundMusic;
	public float SoundSFX;

	public	Data_Settings (AudioManager audioManager)
    {
		SoundMaster = audioManager.masterVolume;
		SoundMusic = audioManager.musicVolume;
		SoundSFX = audioManager.SFXVolume;
    }
}
