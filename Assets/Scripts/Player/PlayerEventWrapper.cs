using System;

public class PlayerEventWrapper {
    public event Action playerMoved;
    public void InvokePlayerMoved() {
        playerMoved?.Invoke();
    }
}
