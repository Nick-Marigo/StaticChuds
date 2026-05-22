using UnityEngine;
using System;

/* This class attaches a timer to a player and invokes an event whenever some number of
 * seconds have passed without the player moving */
public class MovementTimer : MonoBehaviour {
    private float _triggerTime;
    private float _timeLeft;

    public event Action movementTimerTriggered;

    public float TriggerTime {
        private get { return _timeLeft; }
        set { 
            _triggerTime = value;
            _timeLeft = value; 
        }
    }

    public void ResetTimer() {
        _timeLeft = _triggerTime;
    }

    void Update() {
        if (_timeLeft <= 0) {
            movementTimerTriggered?.Invoke();
            _timeLeft = _triggerTime;
        }
        _timeLeft -= Time.deltaTime;
    }
}
