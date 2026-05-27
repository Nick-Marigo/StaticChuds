using UnityEngine;

public class OnKillEnemyTrigger : Trigger {

   public OnKillEnemyTrigger(string description, string type, string amount) {
        this.description = description;
        this.type = type;
        this.amount = amount;
   }

    override public void Activate() {
        base.Activate();
        EventBus.Instance.enemyKilled += OnEnemyKilled;
    }

    void OnEnemyKilled(string enemyName)
    {
        if (enemyName == amount)
        {
            InvokeEffect();   
        }
    }
}
