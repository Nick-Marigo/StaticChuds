using UnityEngine;

public class TakeDamageTrigger : Trigger {
    public TakeDamageTrigger(Relic owner, string description, string type) {
        this.relic = owner;
        this.description = description;
        this.type = type;

        EventBus.Instance.OnDamage += CatchSubscription;
    }

    void CatchSubscription(Vector3 pos, Damage dmg, Hittable target) {
        if (target.team == Hittable.Team.PLAYER) {
            InvokeEffect();
        }
    }

    override protected void Unsuscribe() {
        EventBus.Instance.OnDamage += CatchSubscription;
    }
}
