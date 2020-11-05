using UnityEngine.Audio;
using UnityEngine;



/*This script creates the sound class that is used by the audiomanager to play sounds, 
 * it takes the name, 
 * sound that is meant to be played, 
 * category which the sound belongs to (used by the audiomanager to adjust volume)
 * Volume that defines the  volume of the sound 
 * pitch, determines pitch (default is 0) 
 * loop (which is only used for music)  
 * and if we wished to use mixer groups which are not necessary on 22.9.2020 
 * source is where the sound is coming from, as default the sound will be coming from the audiomanager*/

[System.Serializable]
public class Sound {
	public string name;

	public AudioClip clip;

	public string category;

	[Range(0f, 1f)]
	public float volume = .75f;

	[Range(.1f, 3f)]
	public float pitch = 1f;

	public bool loop = false;

	//No mixer groups have been implemented yet
	//public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

}
