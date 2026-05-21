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
    }

    public Relic FetchUnusedRelic() {
        Relic relic = _potentialRelics["green_gem"](); 
        _potentialRelics.Remove("green_gem");
        return relic;
    }

    public void EquipRelic(Relic relic) {
       relic.attributePackageRequested += () => relic.SetAttributePackage(_attributePackage.GetAttributes()); 
    }
}
