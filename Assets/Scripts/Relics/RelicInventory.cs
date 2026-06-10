using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class RelicInventory : iNodeSystem {

    private EntityAttributePackage _attributePackage;

    private Dictionary<string, Dictionary<string, Func<Relic>>> _potentialRelicsByType;
    private List<string> _potentialRelics;
    private Dictionary<string, Relic> _equippedRelics;

    public event Action relicEquipped;

    public RelicInventory (EntityAttributePackage attributePackage) {
        _potentialRelics = new(RelicLoader.RelicNames);
        _potentialRelicsByType = new(RelicLoader.RelicsByType);
        _equippedRelics = new();

        _attributePackage = attributePackage;

        // Quick equip for testing
        //Relic rel = RelicLoader.Relics["Cursed Scroll"]();

        // Equip all relics FIXME does not update GUI
        // foreach (var rel in RelicLoader.RelicNames) {
        //     //Relic r = FetchUnusedRelicByType("mana", "speed");
        //     Relic r = GetRelicByType("health", "damage");
        //     Debug.Log($"fetched {r.name} || {r.type}");
        //     //var r = rel.Value();
        //     EquipRelic(r);
        // }
    }

    public void Equip(iNodeObject obj) {
       // Debug.Log(obj);
        EquipRelic((Relic)obj);
        relicEquipped?.Invoke();
    }

    public iNodeObject GetNodeObjectByType(string affinity, string weakness) {
            var rLambda = ObjectByTypeFetcher.FetchUnusedObject<Func<Relic>>(_potentialRelicsByType, affinity, weakness);
            if (rLambda == null) return null;
            var r = rLambda();
            _potentialRelicsByType[r.type].Remove(r.name);
            return r;
    }

    /* Returns an undiscovered relic by type: relic with the given affinity will
     * have priority while weakness-related relics are saved for last */
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

