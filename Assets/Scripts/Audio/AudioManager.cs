using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioIdentifier {
	SOUND_HURT,
	SOUND_CAST,
};

public class AudioManager : MonoBehaviour {
    
	private Dictionary<AudioIdentifier, AudioClip> _lookup;

	[SerializeField]
	private AudioSource _sourcePrefab;
	private Dictionary<int, AudioSource> _sources;
	
	public static AudioManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
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

		StartCoroutine(RemoveSource(newSource.clip.length, sourceHandle));
	}

	private IEnumerator RemoveSource(float delay, int sourceHandle) {
		yield return new WaitForSeconds(delay);

		_sources.Remove(sourceHandle);
	}

}
