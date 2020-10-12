using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundGroup {
    public Sound[] sounds;

    public AudioSource source;

    public bool active = true; 

    [Range(0f, 1f)]
	public float volume = .75f;

    public float minTimeBetweenSounds = 5f;
    public float maxTimeBetweenSounds = 15f;

    public string name;
}
