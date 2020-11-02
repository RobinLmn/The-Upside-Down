using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	
	public static AudioManager instance;
	public AudioMixerGroup mixerGroup;
	public SoundGroup[] triggeredSounds;
	public SoundGroup[] constantlyPlayingSounds;
	public SoundGroup[] randomlyPlayedSounds;
	void Awake()
	{

		foreach (SoundGroup soundGroup in constantlyPlayingSounds) {
			soundGroup.source = gameObject.AddComponent<AudioSource>() as AudioSource;
			StartCoroutine(PlayLoopingSoundGroup(soundGroup));
		}

		foreach (SoundGroup soundGroup in randomlyPlayedSounds) {
			soundGroup.source = gameObject.AddComponent<AudioSource>() as AudioSource;
			StartCoroutine(PlayRandomlyPlayedSoundGroup(soundGroup));
		}

		foreach (SoundGroup soundGroup in triggeredSounds) {
			soundGroup.source = gameObject.AddComponent<AudioSource>() as AudioSource;
		}

	}

	IEnumerator PlayRandomlyPlayedSoundGroup(SoundGroup soundGroup) {
		while (soundGroup.active) {
			int randomIndex = UnityEngine.Random.Range(0, soundGroup.sounds.Length);
			Sound s = soundGroup.sounds[randomIndex];

			soundGroup.source.clip = s.clip;
			soundGroup.source.volume = soundGroup.volume * s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			soundGroup.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

			float randomTime = UnityEngine.Random.Range(soundGroup.minTimeBetweenSounds, soundGroup.maxTimeBetweenSounds);
			soundGroup.source.Play();
			//Debug.Log(randomTime);
			yield return new WaitForSeconds(randomTime);

		}
	}	

	IEnumerator PlayLoopingSoundGroup(SoundGroup soundGroup) 
	{
		while (soundGroup.active) {
			
			int randomIndex = UnityEngine.Random.Range(0, soundGroup.sounds.Length);
			Sound s = soundGroup.sounds[randomIndex];

			soundGroup.source.clip = s.clip;
			soundGroup.source.volume = soundGroup.volume * s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			soundGroup.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

			soundGroup.source.Play();

			yield return new WaitWhile(() => soundGroup.source.isPlaying);
		}
	}

	public AudioSource GetConstantlyPlayingAudioSource(string soundGroup) {
		return Array.Find(constantlyPlayingSounds, item => item.name == soundGroup).source;
	}

	public void StartLoopingSoundGroup(string soundGroup) {
		SoundGroup sg = Array.Find(constantlyPlayingSounds, item => item.name == soundGroup);
		Debug.Log(soundGroup + "start");
		sg.source.mute = !sg.source.mute;
	}

	public void StopLoopingSoundGroup(string soundGroup) {
		SoundGroup sg = Array.Find(constantlyPlayingSounds, item => item.name == soundGroup);
		Debug.Log(soundGroup + "stop");
		sg.source.mute = !sg.source.mute;
	}

	public void StartRandomlyPlayedSoundGroup(string soundGroup) {
		SoundGroup sg = Array.Find(randomlyPlayedSounds, item => item.name == soundGroup);
		sg.source.mute = false;
	}

	public void StopRandomlyPlayedSoundGroup(string soundGroup) {
		SoundGroup sg = Array.Find(randomlyPlayedSounds, item => item.name == soundGroup);
		sg.source.mute = true;
	}

	public void Play(string sound)
	{
		SoundGroup soundGroup = Array.Find(triggeredSounds, item => item.name == sound);
		if (soundGroup == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		int randomIndex = UnityEngine.Random.Range(0, soundGroup.sounds.Length);
		Sound s = soundGroup.sounds[randomIndex];

		soundGroup.source.clip = s.clip;
		soundGroup.source.volume = soundGroup.volume * s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		soundGroup.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if (!soundGroup.source.isPlaying)
		{
			soundGroup.source.Play();
		}

	}

}
