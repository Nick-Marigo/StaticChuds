using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[JsonObject(MemberSerialization.OptIn)]
abstract public class Effect {
    [JsonProperty]
    public string description { get; protected set; }
    [JsonProperty]
    protected string type;
    [JsonProperty]
    protected string amount;
    [JsonProperty]
    protected string until;

    // Attribute Packages are used to access and change attributes
    // on the player
    public EntityAttributePackage attributePackage;
    public event Action attributePackageRequested;
    public void InvokeAttributePackageRequested() {
        attributePackageRequested?.Invoke();
    }
    
    abstract public void PerformEffect();
}
