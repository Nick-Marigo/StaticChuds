public class PlayerInitializer {
    private PlayerInstance _player;

    public PlayerInitializer(PlayerInstance player) {
        this._player = player;
    }

    public PlayerInitializer SetUIElements() {
        _player.healthui.SetHealth(_player.hp);
        _player.manaui.SetSpellCaster(_player.spellCaster);
        _player.spellUIContainer.SpellCaster = _player.spellCaster;
        _player.spellUIContainer.RefreshSpells();
        _player.spellui.SetSpell(_player.spellCaster.GetSelectedSpell());
        return this;
    }

    public PlayerInitializer SetSpellCaster() {
        _player.spellCaster = new SpellCaster(_player.AttributePackage, Hittable.Team.PLAYER);
        _player.relicInventory = new RelicInventory(_player.AttributePackage);
        _player.StartCoroutine(_player.spellCaster.ManaRegeneration());
        return this;
    }

    public PlayerInitializer SetHP() {
        _player.hp = new Hittable(_player.PlayerClass.CalculateHP(GameManager.Instance.currentWave), Hittable.Team.PLAYER, _player.gameObject);
        _player.hp.OnDeath += () => {
            _player.isDead = true;
            GameManager.Instance.state = GameManager.GameState.GAMEOVER;
        };
        _player.hp.team = Hittable.Team.PLAYER;
        return this;
    }

    public PlayerInitializer SetEventWrapper() {
        _player.eventWrapper = new PlayerEventWrapper();
       _player.PlayerController.unit.unitMoved += _player.eventWrapper.InvokePlayerMoved;
        return this;
    }
}
