/*
public class StandStillTrigger : Trigger {

    public StandStillTrigger(Relic owner, string description, string type, int amount) {
        this.relic = owner;
        this.description = description;
        this.type = type;
        this.amount = amount;

        EventBus.Instance.OnDamage += CatchSubscription;
    }

    void CatchSubscription(Vector3 pos, Damage dmg, Hittable target) {
        if (target.team == Hittable.Team.PLAYER) {
            InvokeEffect();
        }
    }

}
*/
