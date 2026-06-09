using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public Unit unit { get; private set; }
    private PlayerInstance _player;

    private Vector2 movement;
    public Vector3 position{ get { return transform.position; } }
    public int speed;

    void Start() {
       unit = GetComponent<Unit>(); 
       _player = GetComponent<PlayerInstance>();
       
    }

    void OnAttack(InputValue value) {
        GameManager.GameState gameState = GameManager.Instance.state;
        if (!(gameState == GameManager.GameState.INWAVE || gameState == GameManager.GameState.COUNTDOWN)) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        Vector3 mouseWorld = UnityEngine.Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;
        StartCoroutine(_player.spellCaster.Cast(position, mouseWorld));
    }

    void OnMove(InputValue value) {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
        movement = value.Get<Vector2>();
    }

    void Update() {
        if (GameManager.Instance.state != GameManager.GameState.INWAVE && GameManager.Instance.state != GameManager.GameState.COUNTDOWN) {
            unit.movement = Vector2.zero;
        }
        else {
            unit.movement = movement * speed;
        }
    }

}
