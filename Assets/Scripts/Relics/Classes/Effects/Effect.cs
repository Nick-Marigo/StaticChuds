using Newtonsoft.Json;
using System;

[JsonObject(MemberSerialization.OptIn)]
abstract public class Effect : iRequestAttributePackage {
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
    public EntityAttributePackage attributePackage { set; get; }
    public event Action attributePackageRequested;
    public void InvokeAttributePackageRequested() {
        attributePackageRequested?.Invoke();
    }
    
    abstract public void PerformEffect();
}
