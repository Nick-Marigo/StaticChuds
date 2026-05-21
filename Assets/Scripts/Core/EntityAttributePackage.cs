using UnityEngine;
using System;
using System.Collections.Generic;

public class EntityAttributePackage : MonoBehaviour {
    public class AttributeGate {
        public Func<object> Get;
        public Action<object> Set;
    }

    Dictionary<string, AttributeGate> _attributeDict;
    SpellCaster _spellCaster;

    public Dictionary<string, AttributeGate> LoadAttributes() {
        if (_attributeDict != null) return null;

        _spellCaster = gameObject.GetComponent<PlayerController>().spellcaster;

        _attributeDict = new();
        _attributeDict.Add("mana", new AttributeGate {
                Get = () => _spellCaster.mana,
                Set = (value) => _spellCaster.mana = (int)value
                });

        Debug.Log("Package is " + _attributeDict);

        return _attributeDict;
    }
}


