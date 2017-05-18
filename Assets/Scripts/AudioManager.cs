using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

	public static AudioManager Instance;

	public AudioMixerGroup mixerGroup;

	public AudioClip[] clips;

	private List<AudioSource> sources = new List<AudioSource>();

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private AudioSource CreateNewSource()
	{
		GameObject go = new GameObject("source");
		go.transform.parent = transform;
		go.AddComponent<AudioSource>();

		AudioSource source = go.GetComponent<AudioSource>();
		source.outputAudioMixerGroup = mixerGroup;

		sources.Add(source);
		return source;
	}

	private AudioSource GetAvailableAudioSource()
	{
		List<AudioSource> available = new List<AudioSource>();
		foreach (AudioSource s in sources)
		{
			if (!s.isPlaying)
			{
				available.Add(s);
			}
		}
		if (available.Count > 0)
		{
			return available[0];
		}
		else
		{
			return CreateNewSource();
		}
	}

	public void PlaySound2D(AudioClip clip, float vol = 1f, float pitch = 1f)
	{
		AudioSource source = GetAvailableAudioSource();
		source.transform.position = Vector3.zero;
		source.clip = clip;
		source.spatialBlend = 0f;
		source.volume = vol;
		source.pitch = pitch;
		source.Play();
	}

	public void PlaySound2D(string sound, float vol = 1f, float pitch = 1f)
	{
		AudioClip clip = GetClip(sound);
		PlaySound2D(clip, vol, pitch);
	}

	public void PlaySound3D(AudioClip clip, Vector3 pos, float vol = 1f, float pitch = 1f)
	{
		AudioSource source = GetAvailableAudioSource();
		source.transform.position = pos;
		source.clip = clip;
		source.spatialBlend = 1f;
		source.volume = vol;
		source.pitch = pitch;
		source.Play();
	}

	public void PlaySound3D(string sound, Vector3 pos, float vol = 1f, float pitch = 1f)
	{
		AudioClip clip = GetClip(sound);
		PlaySound3D(clip, pos, vol, pitch);
	}

	private AudioClip GetClip(string clipName)
	{
		foreach (AudioClip c in clips)
		{
			if (c.name == clipName) { return c; }
		}
		return null;
	}
}
