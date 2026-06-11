using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class EntityAttributePackage : MonoBehaviour, iNodeSystem {

    private Dictionary <string, Dictionary<string, AttributeGate>> _attributesByType;

    [JsonObject(MemberSerialization.OptIn)]
    public class AttributeGate : iNodeObject {
        [JsonProperty]
        public string type { get; private set; }
        [JsonProperty]
        public string description { get; private set; }
        [JsonProperty("sprite")]
        public int icon { get; private set; }
        [JsonProperty]
        public string amount { get; private set; }

        public string name { get; private set; }
        public Func<object> Get;
        public Action<object> Set;
        public Action Upgrade;

        public AttributeGate(string name, string type) { 
            //this.type = type; 
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
        ((AttributeGate)obj).Upgrade();
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
        AddAttribute("health", "none",
                () => _playerInstance.hp.hp,
                (value) => _playerInstance.hp.hp = Mathf.Min((int)value, _playerInstance.hp.max_hp));
        AddAttribute("max_health", "health",
                () => _playerInstance.hp.max_hp,
                (value) => _playerInstance.hp.max_hp = (int)value);   


        _attributeDict["mana"].Upgrade = () => {Debug.Log("hello");};
        PopulateAttributeGates();
    }

    public iNodeObject GetNodeObjectByType(string affinity, string weakness) {
        _LoadAttributes();
        var attribute = ObjectByTypeFetcher.FetchUnusedObject<AttributeGate>(_attributesByType, affinity, weakness);
        if (attribute == null) return null;
        Debug.Log($"before removal: {attribute.name} + {attribute.type}");
        _attributesByType[attribute.type].Remove(attribute.name);
        return attribute;
    }

    /* Loads all the attributes of each existing Gate from JSON */
    private void PopulateAttributeGates() {
        TextAsset statsJson = Resources.Load<TextAsset>("stats");
        if (statsJson == null) {
            Debug.Log("Failed to get stats json from Resources");
            return;
        }

        JObject stats = JObject.Parse(statsJson.text);
        foreach (var entry in stats) {
            string key = entry.Key;
            if (_attributeDict.TryGetValue(key, out AttributeGate existing)) {
                JsonConvert.PopulateObject(entry.Value.ToString(), existing);
            }
        }

        foreach (var entry in _attributeDict) {
            Debug.Log($"name: {entry.Value.name} || type: {entry.Value.type} || description: {entry.Value.description}");
        }
        
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


