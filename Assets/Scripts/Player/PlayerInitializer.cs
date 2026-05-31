public class PlayerInitializer {
    private PlayerInstance _player;

    public PlayerInitializer(PlayerInstance player) {
        this._player = player;
    }

    public PlayerInstance SetUIElements() {
        _player.healthui.SetHealth(_player.hp);
        _player.manaui.SetSpellCaster(_player.spellCaster);
        _player.spellUIContainer.SpellCaster = _player.spellCaster;
        _player.spellUIContainer.RefreshSpells();
        _player.spellui.SetSpell(_player.spellCaster.GetSelectedSpell());
        return _player;
    }

    /*
    PlayerInstance SetUIElements() {
        return player;
    }

    PlayerInstance SetUIElements() {
        return player;
    }

    PlayerInstance SetUIElements() {
        return player;
    }
    */
}
