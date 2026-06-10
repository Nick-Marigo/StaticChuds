using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioIdentifier {
	MUSIC_MAIN,
	SOUND_HURT,
	SOUND_CAST,
};

public class AudioManager : MonoBehaviour {
    
	private Dictionary<AudioIdentifier, AudioClip> _lookup = new();

	[SerializeField]
	private AudioSource _sourcePrefab;
	private Dictionary<int, AudioSource> _sources = new();
	
	public static AudioManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;

			EventBus.Instance.OnPlaySound += PlaySound;

			_lookup[ AudioIdentifier.MUSIC_MAIN ] = Resources.Load<AudioClip>("Audio/dungeon");
			_lookup[ AudioIdentifier.SOUND_HURT ] = Resources.Load<AudioClip>("Audio/hurt");
			_lookup[ AudioIdentifier.SOUND_CAST ] = Resources.Load<AudioClip>("Audio/cast");

			_sources[0] = Instantiate<AudioSource>(_sourcePrefab, transform);
			_sources[0].clip = _lookup[AudioIdentifier.MUSIC_MAIN];
			_sources[0].loop = true;

			_sources[0].Play();

			return;
		}

		if(Instance == this) { return; }

		Destroy(this);
	}

	private void PlaySound(AudioIdentifier identifier) {
		AudioSource newSource = Instantiate<AudioSource>(_sourcePrefab, transform);
		newSource.clip = _lookup[identifier];

		int sourceHandle = -1;

		for(int i = 0; i < _sources.Count + 1; i++) {
			if(_sources.TryGetValue(i, out _)) { continue; }

			sourceHandle = i;
			break;
		}

		_sources[sourceHandle] = newSource;

		newSource.Play();

		StartCoroutine(RemoveSource(newSource.clip.length, sourceHandle));
	}

	private IEnumerator RemoveSource(float delay, int sourceHandle) {
		yield return new WaitForSeconds(delay);

		_sources.Remove(sourceHandle);
	}

	private void OnDestroy() {
		EventBus.Instance.OnPlaySound -= PlaySound;
	}

}
