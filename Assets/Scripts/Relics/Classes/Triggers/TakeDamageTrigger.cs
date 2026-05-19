using UnityEngine;

public class TakeDamageTrigger : Trigger {
    public TakeDamageTrigger(string description, string type) {
        this.description = description;
        this.type = type;
    }

    override protected void InvokeEffect() {
    }
}
