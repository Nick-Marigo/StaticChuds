using UnityEngine;
using System;
using System.Collections.Generic;

public class EntityAttributePackage : MonoBehaviour {
    public class AttributeGate {
        public Func<object> Get;
        public Action<object> Set;
    }

    Dictionary<string, AttributeGate> _attributeDict;
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

    void _LoadAttributes() {
        if (_attributeDict != null) return;

        _SetPlayerSystems();

        _attributeDict = new();
        _attributeDict.Add("mana", new AttributeGate {
                Get = () => _spellCaster.Mana,
                Set = (value) => _spellCaster.Mana = (int)value
                });
        _attributeDict.Add("spellpower", new AttributeGate {
                Get = () => _spellCaster.spellPower,
                Set = (value) => _spellCaster.spellPower = (int)value
                });
        _attributeDict.Add("event_wrapper", new AttributeGate {
                Get = () => _eventWrapper
                });
        _attributeDict.Add("speed", new AttributeGate {
                Get = () => _playerController.speed,
                Set = (value) => _playerController.speed = (int)value
                });
        _attributeDict.Add("health", new AttributeGate {
                Get = () => _playerInstance.hp.hp,
                Set = (value) => _playerInstance.hp.hp = Mathf.Min((int)value, _playerInstance.hp.max_hp)
                });
        _attributeDict.Add("max_health", new AttributeGate {
                Get = () => _playerInstance.hp.max_hp,
                Set = (value) => _playerInstance.hp.max_hp = (int)value
                });
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


