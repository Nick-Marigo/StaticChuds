using UnityEngine;
using System;

/* This class attaches a timer to a player and invokes an event whenever some number of
 * seconds have passed without the player moving */
public class MovementTimer : MonoBehaviour {
    private float _triggerTime;
    private float _timeLeft;
    private bool _timerSet = false;

    public event Action movementTimerTriggered;

    public float TriggerTime {
        private get { return _timeLeft; }
        set { 
            _triggerTime = value;
            _timeLeft = value; 
            _timerSet = true;
        }
    }

    public void ResetTimer() {
        _timeLeft = _triggerTime;
    }

    void Update() {
        if (!_timerSet) return;

        if (_timeLeft <= 0) {
            movementTimerTriggered?.Invoke();
            ResetTimer();
        }
        _timeLeft -= Time.deltaTime;
    }
}
