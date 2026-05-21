using UnityEngine;

/* This class attaches a timer to a player and invokes an event whenever some number of
 * seconds have passed without the player moving */
/*
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

    void ResetTimer(GameObject player) {
        if (player != gameObject) return;
        _timeLeft = _triggerTime;
    }

    void OnEnable() {
        EventBus.Instance.PlayerMoved += ResetTimer;
    }

    void OnDisable() {
        EventBus.Instance.PlayerMoved -= ResetTimer;
    }

    void Update() {
        if (_timeLeft <= 0) {
            TimerTriggered?.Invoke();
            _timeLeft = _triggerTime;
        }
        _timeLeft -= Time.deltaTime;
    }
}
*/
