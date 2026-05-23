using System;
using System.Collections.Generic;

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
        // MAX: I added this because my UI needs a null return if there are no more relics left to choose from
        // Change !_potentialRelics.ContainsKey("Jade Elephant") to whatever relic you are testing with, or once finished with all relics change to check if dictionary is empty
        if(!_potentialRelics.ContainsKey("Jade Elephant")) { 
            return null;
        }

        Relic relic = _potentialRelics["Jade Elephant"](); 
        _potentialRelics.Remove("Jade Elephant");
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
