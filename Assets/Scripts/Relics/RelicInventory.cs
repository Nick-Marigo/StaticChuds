using System;
using System.Collections.Generic;
using UnityEngine;

public class RelicInventory {
    EntityAttributePackage _attributePackage;

    Dictionary<string, Func<Relic>> _potentialRelics;
    Dictionary<string, Relic> _equippedRelics;

    public RelicInventory (EntityAttributePackage attributePackage) {
        _potentialRelics = RelicLoader.Relics;
        _equippedRelics = new();

        _attributePackage = attributePackage;

        // REMOVE: debugging for JSON loading
        /*
        foreach (KeyValuePair<string, Func<Relic>> relic in _potentialRelics) {
            Relic r = relic.Value();
            Debug.Log($"{r.name}, trigger: {r.trigger.description}, effect: {r.effect.description}");
        }
        */
    }

    public Relic FetchUnusedRelic() {
        Relic relic = _potentialRelics["Green Gem"](); 
        _potentialRelics.Remove("Green Gem");
        return relic;
    }

    public void EquipRelic(Relic relic) {
        _equippedRelics.Add(relic.name, relic);
        relic.attributePackageRequested += () => relic.SetAttributePackage(_attributePackage.GetAttributes()); 
    }

    public Dictionary<string, Relic> GetEquippedRelics() {
        return _equippedRelics;
    }
}
