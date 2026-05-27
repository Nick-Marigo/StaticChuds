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

    PlayerController _playerController;
    SpellCaster _spellCaster;
    PlayerEventWrapper _eventWrapper;

    public Dictionary<string, AttributeGate> GetAttributes() {
        _LoadAttributes();
        return _attributeDict;
    }

    void _LoadAttributes() {
        if (_attributeDict != null) return;

        _SetPlayerSystems();

        _attributeDict = new();
        _attributeDict.Add("mana", new AttributeGate {
                Get = () => _spellCaster.mana,
                Set = (value) => _spellCaster.mana = (int)value
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
    }

    void _SetPlayerSystems() {
        /*PlayerController playerController = gameObject.GetComponent<PlayerController>();*/
        _playerController = gameObject.GetComponent<PlayerController>();
        //_spellCaster = playerController.spellcaster;
        _spellCaster = _playerController.spellcaster;
        if (_spellCaster == null) {
            Debug.Log($"ERROR-EntityAttributePackage: entity has no SpellCaster");
        }
        _eventWrapper = _playerController.eventWrapper;
    }
}


