using UnityEngine;
using System.Collections.Generic;

public class StandStillTrigger : Trigger {
    MovementTimer movementTimer;

    public StandStillTrigger(Relic owner, string description, string type, string amount) {
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

    override public void ChangeOwner(GameObject owner) {
        // Delete any old movementTimers on obj
        if (relic.Owner != null) {
            MovementTimer mt = relic.Owner.GetComponent<MovementTimer>();
            if (mt != null) {
                Object.Destroy(mt);
            }
        }

        if (owner == null) return;
        // Add a MovementTimer to the owner GameObject and save a ref to it
        owner.AddComponent<MovementTimer>();
        movementTimer = owner.GetComponent<MovementTimer>();

        // Set the stand still time
        int waveNum = GameManager.Instance.currentWave;
        int timeAmount = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int> {
                {"wave", waveNum}
        });
        movementTimer.standStillTime = timeAmount;
    }
}
