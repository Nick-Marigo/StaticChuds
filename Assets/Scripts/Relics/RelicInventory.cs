using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class RelicInventory {

    private EntityAttributePackage _attributePackage;

    private Dictionary<string, Dictionary<string, Func<Relic>>> _potentialRelicsByType;
    private List<string> _potentialRelics;
    private Dictionary<string, Relic> _equippedRelics;

    public RelicInventory (EntityAttributePackage attributePackage) {
        _potentialRelics = new(RelicLoader.RelicNames);
        _potentialRelicsByType = new(RelicLoader.RelicsByType);
        _equippedRelics = new();

        _attributePackage = attributePackage;

        // Quick equip for testing
        //Relic rel = RelicLoader.Relics["Cursed Scroll"]();

        // Equip all relics
        foreach (var rel in RelicLoader.RelicNames) {
            Relic r = FetchUnusedRelicByType("mana", "speed");
            Debug.Log($"fetched {r.name} || {r.type}");
            //var r = rel.Value();
            EquipRelic(r);
        }
    }

    /* Returns an undiscovered relic by type: relic with the given affinity will
     * have priority while weakness-related relics are saved for last */
    public Relic FetchUnusedRelicByType(string affinity, string weakness) {
        var types = GameManager.Instance.types;
        Debug.Log(types);
        if ( !types.Contains(affinity) || !types.Contains(weakness) ) {
            throw new ArgumentException($"affinity:{affinity} or weakness:{weakness}  are not of a valid type");
        }

        Relic relic;
        if (GetRelicOfType(affinity, out relic)) return relic;
        foreach (string type in types) {
            if (type == weakness) continue;
            if (GetRelicOfType(type, out relic)) return relic;
        }
        if (GetRelicOfType(weakness, out relic)) return relic;
        return null;
    }

    // Returns true if there is a relic of specified type, and stores in in
    // relic variable
    private bool GetRelicOfType(string type, out Relic relic) {
        var relicDict = _potentialRelicsByType[type];
        relic = null;
        if (relicDict.Count == 0) return false;
        int relicIndex = UnityEngine.Random.Range(0, relicDict.Count);
        relic = relicDict.ElementAt(relicIndex).Value();
        return true;
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
        _potentialRelicsByType[relic.type].Remove(relic.name);
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

