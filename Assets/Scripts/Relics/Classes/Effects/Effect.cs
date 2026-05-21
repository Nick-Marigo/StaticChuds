using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[JsonObject(MemberSerialization.Fields)]
abstract public class Effect {
    protected string description;
    protected string type;
    protected string amount;
    protected string until;

    // Attribute Packages are used to access and change attributes
    // on the player
    public Dictionary<string, EntityAttributePackage.AttributeGate> attributePackage;
    public event Action attributePackageRequested;
    public void InvokeAttributePackageRequested() {
        attributePackageRequested?.Invoke();
    }
    
    abstract public void PerformEffect();
}
