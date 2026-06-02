using UnityEngine;
using System.Collections.Generic;

public class RelicInventory {
    EntityAttributePackage _attributePackage;

    List<string> _potentialRelics;
    Dictionary<string, Relic> _equippedRelics;

    public RelicInventory (EntityAttributePackage attributePackage) {
        _potentialRelics = new(RelicLoader.RelicNames);
        _equippedRelics = new();

        _attributePackage = attributePackage;

        // Quick equip for testing
        //Relic rel = RelicLoader.Relics["Cursed Scroll"]();

        // Equip all relics
        /*
        foreach (var rel in RelicLoader.Relics) {
            var r = rel.Value();
            EquipRelic(r);
        }
        */
    }

    public List<Relic> FetchUnusedRelics(int n) {
        List<Relic> potentialRelics = new();

        List<string> potentialDisplayedRelics = new(_potentialRelics);

        for (int i = 0; i < n; i++) {
            if(potentialDisplayedRelics.Count == 0) break;

            int randIdx = UnityEngine.Random.Range(0, potentialDisplayedRelics.Count);
            string chosenRelic = potentialDisplayedRelics[randIdx];

            Relic relic = RelicLoader.Relics[chosenRelic](); 
            potentialDisplayedRelics.RemoveAt(randIdx);

            potentialRelics.Add(relic);
        }

        return potentialRelics;
    }

    public void EquipRelic(Relic relic) {
        _equippedRelics.Add(relic.name, relic);
        _potentialRelics.Remove(relic.name);
        // Give the relic an attribute package
        relic.attributePackageRequested += () => relic.attributePackage = _attributePackage;
        // Relics can only be activated after their attribute package request event has
        // been subscribed to (they do not work without an owner)
        relic.Activate();

    }

    public Dictionary<string, Relic> GetEquippedRelics() {
        return _equippedRelics;
    }
}
