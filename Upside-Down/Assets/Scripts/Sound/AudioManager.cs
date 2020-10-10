using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

/* TODO: Separate sounds into different categories:
- Ambient and constantly playing (Change audio files to different format so can have fully looping)
- Ambient and played at random times
- Called to play by event in game

*/
public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
	private ArrayList ambienceSounds = new ArrayList();

	public int maxTimeBetweenAmbientSounds;
	public int minTimeBetweenAmbientSounds;

	private bool playAmbienceSounds = true;

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

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>() as AudioSource;
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.pitch = 1f;
			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.tag == "ambience") {
				ambienceSounds.Add(s.name);
			}

			if (s.loop) {
				s.source.volume = s.volume;
				s.source.Play();
			}
		}

		StartCoroutine(RandomlyPlayAmbienceSounds());
	}

	IEnumerator RandomlyPlayAmbienceSounds() 
	{
		while (playAmbienceSounds) {
			int randomTime = UnityEngine.Random.Range(minTimeBetweenAmbientSounds, maxTimeBetweenAmbientSounds);
			int randomIndex = UnityEngine.Random.Range(0, ambienceSounds.Count);
			yield return new WaitForSeconds(randomTime);
			// Debug.Log(ambienceSounds[randomIndex]);
			Play((string) ambienceSounds[randomIndex]);
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		Debug.Log(sound);
		s.source.Play();

	}

}
