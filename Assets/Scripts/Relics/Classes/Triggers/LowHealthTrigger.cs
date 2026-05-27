using UnityEngine;

public class LowHealthTrigger : Trigger {

    bool triggeredThisWave = false;

    public LowHealthTrigger(string description, string type) {
        this.description = description;
        this.type = type;
    }

    override public void Activate() {
        base.Activate();
        EventBus.Instance.OnDamage += CheckLowHealth;
        EventBus.Instance.OnWaveStart += ResetTrigger;
    }

    void CheckLowHealth(Vector3 pos, Damage dmg, Hittable target) {

        if (triggeredThisWave) return;

        if (target.team == Hittable.Team.PLAYER) {

            int healthAfterDamage = target.hp - dmg.amount;

            float halfHealth = target.max_hp * 0.5f;

            if (healthAfterDamage < halfHealth)
            {
                triggeredThisWave = true;
                InvokeEffect();
            }
        }
    }

    void ResetTrigger(int waveNum)
    {
        triggeredThisWave = false;
    }
}