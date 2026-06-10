using UnityEngine;
using System;

public class EventBus 
{
    private static EventBus theInstance;
    public static EventBus Instance
    {
        get
        {
            if (theInstance == null)
                theInstance = new EventBus();
            return theInstance;
        }
    }

    public event Action<Vector3, Damage, Hittable> OnDamage;
    
    public void DoDamage(Vector3 where, Damage dmg, Hittable target)
    {
        OnDamage?.Invoke(where, dmg, target);
    }

    public void Clear()
    {
        OnDamage = null;
    }

    public event Action<int> OnWaveStart;
    public void StartWave(int waveNum)
    {
        OnWaveStart?.Invoke(waveNum);
    }

	public event Action<AudioIdentifier> OnPlaySound;
	public void InvokePlaySound(AudioIdentifier soundIdentifier) {
		OnPlaySound?.Invoke(soundIdentifier);
	}

    public event Action<string> enemyKilled;
    public void InvokeEnemyKilled(string enemyName) {
        enemyKilled?.Invoke(enemyName);
    }
}
