using System.Collections.Generic;

public class RelicInventory {
    EntityAttributePackage _attributePackage;

    List<string> _potentialRelics;
    Dictionary<string, Relic> _equippedRelics;

    public RelicInventory (EntityAttributePackage attributePackage) {
        _potentialRelics = new(RelicLoader.RelicNames);
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
        if(_potentialRelics.Count == 0) { 
            return null;
        }

        // TO REMOVE
        return RelicLoader.Relics["Golden Mask"]();

        int randIdx = UnityEngine.Random.Range(0, _potentialRelics.Count);
        string chosenRelic = _potentialRelics[randIdx];
        Relic relic = RelicLoader.Relics[chosenRelic](); 
        _potentialRelics.RemoveAt(randIdx);
        return relic;
    }

    public void EquipRelic(Relic relic) {
        _equippedRelics.Add(relic.name, relic);
        relic.attributePackageRequested += () => relic.attributePackage = _attributePackage;
    }

    public Dictionary<string, Relic> GetEquippedRelics() {
        return _equippedRelics;
    }
}
