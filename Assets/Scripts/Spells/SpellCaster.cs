using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class SpellCaster : iNodeSystem {
    private Dictionary<string, Dictionary<string, Func<SpellCaster, Spell, Spell>>> _potentialSpellModsByType;
    public Hittable.Team team;

    private int _mana = -1;
    public int Mana {
        set {
            value = Mathf.Clamp(value, 0, max_mana);
            _mana = value;
            manaChanged?.Invoke(value);
        }
        get {
            return _mana;
        }
    }
    public int max_mana;
    public int mana_reg;

    public List<Spell> spells = new List<Spell>();
    public Spell spell {
        get {
            return GetSelectedSpell();
        }
    }
    public int selectedSpellIndex = 0;
    public const int MAXSPELLS = 4;
    public int spellPower = 5;

    public EntityAttributePackage _attributePackage;
    private PlayerEventWrapper _eventWrapper;

    public event Action<int> manaChanged;
    public event Action<int> spellSelected;

    public void Equip (iNodeObject obj) {
        Debug.Log(obj);
    }

    public IEnumerator ManaRegeneration() {
        while (true) {
            Mana += mana_reg;
            yield return new WaitForSeconds(1);
        }
    }

    public SpellCaster(EntityAttributePackage attributePackage, Hittable.Team team) {
        this.team = team;
        _attributePackage = attributePackage;
        MapKeysToSpells();
        spells.Add(SpellBuilder.BuildArcaneBolt(this));
        _potentialSpellModsByType = new(SpellLoader.SpellModsByType);
    }

    public IEnumerator Cast(Vector3 where, Vector3 target) {        
        if (spells.Count == 0) yield break;

        if (_eventWrapper == null) {
            _eventWrapper = (PlayerEventWrapper)_attributePackage.AttributeDict["event_wrapper"].Get();
        }

        Spell selectedSpell = spells[selectedSpellIndex];

        if (Mana >= selectedSpell.GetManaCost() && selectedSpell.IsReady()) {

            selectedSpell.UpdateDicts(GameManager.Instance.currentWave);
            Mana -= selectedSpell.GetManaCost();
            selectedSpell.last_cast = Time.time;
            yield return selectedSpell.Cast(where, target, team);
            _eventWrapper.InvokeSpellCast();
        }
        yield break;
    }

    public iNodeObject GetNodeObjectByType(string affinity, string weakness) {
            Spell s = ObjectByTypeFetcher.FetchUnusedObject<Func<SpellCaster, Spell, Spell>>(_potentialSpellModsByType, affinity, weakness)(this, null);
            if (s == null) return null;
            _potentialSpellModsByType[s.type].Remove(s.name);
            return s;
    }

    public Spell GetSelectedSpell() {
        if (spells.Count == 0) return null;
        return spells[selectedSpellIndex];
    }

    public void SelectSpell(int index) {
        if (index < 0 || index >= spells.Count) return;
        spellSelected?.Invoke(index);
        selectedSpellIndex = index;
    }

    public bool AddSpell(Spell newSpell) {
        if (spells.Count >= MAXSPELLS) return false;
        spells.Add(newSpell);
        return true;
    }

    public void RemoveSpellAt(int index) {
        if (index < 0 || index >= spells.Count) return;
        spells.RemoveAt(index);

        if (selectedSpellIndex >= spells.Count)
        {
            selectedSpellIndex = Mathf.Max(0, spells.Count - 1);
        }
    }

    public void SetStats(int newMana, int newManaRegen, int newSpellPower) {
        this.Mana = newMana;
        this.max_mana = newMana; 
        this.mana_reg = newManaRegen;
        this.spellPower = newSpellPower;
    }

    /* Select actions are defined in the InputSystem. Their name must be in the
     * form "Spell[num]" */

    private List<(InputAction action, Action<InputAction.CallbackContext> handler)> _spellHandlers = new();
    private void MapKeysToSpells() {
        var actionMap = InputSystem.actions;
        for (int i = 1; i <= MAXSPELLS; i++) {
            string num = Convert.ToString(i, 10);
            string actionName = String.Concat("Spell", num);
            var selectSpellAction = actionMap.FindAction(actionName);

            int ind = i;
            
            Action<InputAction.CallbackContext> handler = (_) => SelectSpell(ind - 1);
            selectSpellAction.started += handler;
            _spellHandlers.Add((selectSpellAction, handler));
        }
    }

    public void Dispose() {
        foreach (var (action, handler) in _spellHandlers) {
            action.started -= handler;
        }
    }
}
