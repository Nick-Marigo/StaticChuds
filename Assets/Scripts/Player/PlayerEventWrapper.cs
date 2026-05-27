using System;

public class PlayerEventWrapper {
    public event Action playerMoved;
    public void InvokePlayerMoved() {
        playerMoved?.Invoke();
    }

    public event Action spellCast;
    public void InvokeSpellCast() {
        spellCast?.Invoke();
    }
}
