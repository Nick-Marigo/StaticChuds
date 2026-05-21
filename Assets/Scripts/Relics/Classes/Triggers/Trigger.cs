using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[JsonObject(MemberSerialization.Fields)]
abstract public class Trigger {
    protected string description;
    protected string type;
    [JsonIgnore]
    public Effect effect;

    // Attribute Packages are used to access and change attributes
    // on the player
    public Dictionary<string, EntityAttributePackage.AttributeGate> attributePackage;
    public event Action attributePackageRequested;
    
    abstract protected void InvokeEffect();
}
