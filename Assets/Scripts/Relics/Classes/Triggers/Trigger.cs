using Newtonsoft.Json;
using System;

[JsonObject(MemberSerialization.OptIn)]
abstract public class Trigger : iRequestAttributePackage {
    [JsonProperty]
    public string description { get; protected set; }
    [JsonProperty]
    protected string type;
    [JsonProperty]
    protected string amount;

    public Effect effect;

    // Attribute Packages are used to access and change attributes
    // on the player
    public EntityAttributePackage attributePackage { set; get; }
    public event Action attributePackageRequested;
    
    // This function is called when the relic is claimed for setup
    virtual public void Activate() {
        attributePackageRequested?.Invoke();
    }

    public void InvokeAttributePackageRequested() {
        attributePackageRequested?.Invoke();
    }

    virtual protected void InvokeEffect() {
        effect.PerformEffect();
    }
}
