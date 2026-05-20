using UnityEngine;

/* This class attaches a timer to a player and invokes an event whenever some number of
 * seconds have passed without the player moving */
public class MovementTimer : MonoBehaviour {
    private float triggerTime;
    private float timeLeft;
    private GameObject owner;

    public float TriggerTime {
        private get { return timeLeft; }
        set { 
            triggerTime = value;
            timeLeft = value; 
        }
    }

    void OnEnable() {
        owner = gameObject;
        EventBus.Instance.PlayerMoved += ResetTimer;
    }

    void OnDisable() {
        EventBus.Instance.PlayerMoved -= ResetTimer;
    }

    void ResetTimer(GameObject player) {
        if (player != owner) return;
        timeLeft = triggerTime;
    }

    void Update() {
        if (timeLeft <= 0) {
            EventBus.Instance.InvokeTimerTriggered(owner);
            Debug.Log(owner);
            timeLeft = triggerTime;
        }
        timeLeft -= Time.deltaTime;
    }
}
