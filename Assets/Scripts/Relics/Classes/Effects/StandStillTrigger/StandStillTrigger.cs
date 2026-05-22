using UnityEngine;
using System.Collections.Generic;

public class StandStillTrigger : Trigger {
    MovementTimer _movementTimer;

    public StandStillTrigger(string description, string type, string amount) {
        this.description = description;
        this.type = type;
        this.amount = amount;

        _InitializeMovementTimer();
    }

    protected void CatchSubscription() {
        InvokeEffect();
    }

    void _InitializeMovementTimer() {
        InvokeAttributePackageRequested(); 
        PlayerEventWrapper eventWrapper = (PlayerEventWrapper)attributePackage["event_wrapper"].Get();
        
        GameObject timerContainer = new GameObject("timer_container", typeof(MovementTimer));
        _movementTimer = timerContainer.GetComponent<MovementTimer>();
        _movementTimer.movementTimerTriggered += CatchSubscription;
        eventWrapper.playerMoved += _movementTimer.ResetTimer;
        EventBus.Instance.OnWaveStart += _UpdateTimer;
        
    }

    // Update the timer when a new wave starts
    void _UpdateTimer(int waveNum) {
        int timeAmount = RPNEvaluator.RPNEvaluator.Evaluate(amount, 
                new Dictionary<string, int> {
                {"wave", waveNum}
                });
        _movementTimer.TriggerTime = timeAmount;
    }
    //FIX add unsubscription (for movement timer too)
    //TODO add player switching support
}
