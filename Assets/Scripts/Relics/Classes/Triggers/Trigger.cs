using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[JsonObject(MemberSerialization.OptIn)]
abstract public class Trigger {
    [JsonProperty]
    public string description { get; protected set; }
    [JsonProperty]
    protected string type;

    public Effect effect;

    // Attribute Packages are used to access and change attributes
    // on the player
    public Dictionary<string, EntityAttributePackage.AttributeGate> attributePackage;
    public event Action attributePackageRequested;
    
    abstract protected void InvokeEffect();
}
