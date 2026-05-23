using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[JsonObject(MemberSerialization.OptIn)]
abstract public class Trigger {
    [JsonProperty]
    public string description { get; protected set; }
    [JsonProperty]
    protected string type;
    [JsonProperty]
    protected string amount;

    public Effect effect;

    // Attribute Packages are used to access and change attributes
    // on the player
    public EntityAttributePackage attributePackage;
    public event Action attributePackageRequested;
    
    public void InvokeAttributePackageRequested() {
        attributePackageRequested?.Invoke();
    }

    virtual protected void InvokeEffect() {
        effect.PerformEffect();
    }

    void ToRemove() {
    }
}
