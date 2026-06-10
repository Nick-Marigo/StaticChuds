using UnityEngine;
using System;
using System.Collections.Generic;

public class EntityAttributePackage : MonoBehaviour, iNodeSystem {

    private Dictionary <string, Dictionary<string, AttributeGate>> _attributesByType;
    public class AttributeGate : iNodeObject {

        public string type { get; private set; }
        public string name { get; private set; }
        public Func<object> Get;
        public Action<object> Set;

        public AttributeGate(string name, string type) { 
            this.type = type; 
            this.name = name;
        }
    }

    private Dictionary<string, AttributeGate> _attributeDict;
    public Dictionary<string, AttributeGate> AttributeDict {
        get {
            _LoadAttributes();
            return _attributeDict;
        }
    }

    PlayerInstance _playerInstance;
    PlayerController _playerController;
    SpellCaster _spellCaster;
    PlayerEventWrapper _eventWrapper;

    public void Equip(iNodeObject obj) {
        Debug.Log(obj);
    }

    void _LoadAttributes() {
        if (_attributeDict != null) return;

        _SetPlayerSystems();

        _attributesByType = new();
        foreach (string type in GameManager.Instance.types) {
            _attributesByType.Add(type, new());
        }

        _attributeDict = new();

        AddAttribute("mana", "mana",
                () => _spellCaster.Mana,
                (value) => _spellCaster.Mana = (int)value);
        AddAttribute("spellpower", "damage",
                () => _spellCaster.spellPower,
                (value) => _spellCaster.spellPower = (int)value);
        AddAttribute("event_wrapper", "none",
                () => _eventWrapper,
                null);
        AddAttribute("speed", "speed",
                () => _playerController.speed,
                (value) => _playerController.speed = (int)value);
        AddAttribute("health", "health",
                () => _playerInstance.hp.hp,
                (value) => _playerInstance.hp.hp = Mathf.Min((int)value, _playerInstance.hp.max_hp));
        AddAttribute("max_health", "health",
                () => _playerInstance.hp.max_hp,
                (value) => _playerInstance.hp.max_hp = (int)value);   
    }

    public iNodeObject GetNodeObjectByType(string affinity, string weakness) {
        _LoadAttributes();
        var attribute = ObjectByTypeFetcher.FetchUnusedObject<AttributeGate>(_attributesByType, affinity, weakness);
        if (attribute == null) return null;
        _attributesByType[attribute.type].Remove(attribute.name);
        return attribute;
    }

    private void AddAttribute(string name, string type, Func<object> getter, Action<object> setter) {
        var attributeGate = new AttributeGate(name, type) {
            Get = getter,
            Set = setter 
        };

        _attributeDict.Add(name, attributeGate);
        if (type == "none") return;

        _attributesByType[type].Add(name, attributeGate);
    }

    void _SetPlayerSystems() {
        _playerInstance = gameObject.GetComponent<PlayerInstance>();
        _playerController = gameObject.GetComponent<PlayerController>();
        _spellCaster = _playerInstance.spellCaster;
        if (_spellCaster == null) {
            Debug.Log($"ERROR-EntityAttributePackage: entity has no SpellCaster");
        }
        _eventWrapper = _playerInstance.eventWrapper;
    }
}


