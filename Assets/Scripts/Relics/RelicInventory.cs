using System.Collections.Generic;

public class RelicInventory {
    EntityAttributePackage _attributePackage;

    Dictionary<string, Relic> _potentialRelics;
    Dictionary<string, Relic> _equippedRelics;

    public RelicInventory (EntityAttributePackage attributePackage) {
        _potentialRelics = new();
        _equippedRelics = new();
        _attributePackage = attributePackage;
    }

    


}
