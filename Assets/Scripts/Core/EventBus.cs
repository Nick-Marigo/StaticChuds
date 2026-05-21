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
    
    public void DoDamage(Vector3 where, Damage dmg, Hittable target) {
        OnDamage?.Invoke(where, dmg, target);
    }

    public void Clear() {
        OnDamage = null;
    }

    public event Action<int> OnWaveStart;
    public void StartWave(int waveNum) {
        OnWaveStart?.Invoke(waveNum);
    }

    public event Action<GameObject> PlayerMoved;
    public void InvokePlayerMoved(GameObject player) {
        PlayerMoved?.Invoke(player);
    }

    public event Action<GameObject> TimerTriggered;
    public void InvokeTimerTriggered(GameObject owner) {
        TimerTriggered?.Invoke(owner);
    }

    public event Action<SpellCaster> SpellCast;
    public void InvokeSpellCast(SpellCaster caster) {
        SpellCast?.Invoke(caster);
    }
}
