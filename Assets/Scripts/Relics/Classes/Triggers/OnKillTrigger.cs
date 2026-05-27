using UnityEngine;

public class OnKillTrigger : Trigger {

   public OnKillTrigger(string description, string type) {
        this.description = description;
        this.type = type;
   }

    override public void Activate() {
        base.Activate();
        EventBus.Instance.enemyKilled += InvokeEffect;
    }
}
