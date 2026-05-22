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
    public Dictionary<string, EntityAttributePackage.AttributeGate> attributePackage;
    public event Action attributePackageRequested;
    
    public void InvokeAttributePackageRequested() {
        Debug.Log("asked for package");
        attributePackageRequested?.Invoke();
    }

    virtual protected void InvokeEffect() {
        effect.PerformEffect();
    }
}
