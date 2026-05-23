using UnityEngine;

public class TakeDamageTrigger : Trigger {
    public TakeDamageTrigger(string description, string type) {
        this.description = description;
        this.type = type;
    }

    override public void Activate() {
        base.Activate();
        EventBus.Instance.OnDamage += CatchSubscription;
    }

    void CatchSubscription(Vector3 pos, Damage dmg, Hittable target) {
        if (target.team == Hittable.Team.PLAYER) {
            InvokeEffect();
        }
    }
}
