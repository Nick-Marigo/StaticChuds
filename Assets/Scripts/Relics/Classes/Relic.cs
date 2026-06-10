using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System;

[JsonObject(MemberSerialization.OptIn)]
public class Relic : iNodeObject, iRequestAttributePackage {
    [JsonProperty]
    public string name { get; protected set; }
    [JsonProperty]
    public int sprite { get; protected set; }
    [JsonProperty]
    public string type { get; protected set; }
    [JsonProperty("trigger")]
    TriggerGenerator triggerGen;
    [JsonProperty("effect")]
    EffectGenerator effectGen;

    public Trigger trigger;
    public Effect effect;

    // This event is invoked to get a Dictionary of
    // attributes from the relic holder
    EntityAttributePackage _attributePackage;
    public EntityAttributePackage attributePackage {
        get { return _attributePackage; }
        set { _SetAttributePackages(value); }
    }
    public event Action attributePackageRequested;
    public void InvokeAttributePackageRequested() {
        attributePackageRequested?.Invoke();
    }

    public void Activate() {
        //Debug.Log("activating " + name);
        trigger.Activate();
        effect.Activate();
    }

    public void Equip<RelicInventory> (RelicInventory inventory) {
    }

    void _SetAttributePackages (EntityAttributePackage package) {
        trigger.attributePackage = package;
        effect.attributePackage = package;
    }
 
    /* After the Relic is deserialized, it generates the appropiate Effect and Trigger 
     * using the generator classes */
    [OnDeserialized]
    void OnDeserialization(StreamingContext context) {
        //Debug.Log($"creating relic: {name}");

        trigger = triggerGen.GenerateTrigger();
        if (trigger == null) {
            Debug.Log("failed to load trigger for " + name + " relic");
            return;
        }
        effect = effectGen.GenerateEffect();
        if (effect == null) {
            Debug.Log("failed to load effect for " + name + " relic");
            return;
        }

        trigger.effect = effect;

        // Request a package from the player whenever a trigger or
        // effect requests one
        // FIX unsuscribe
        trigger.attributePackageRequested += InvokeAttributePackageRequested;
        effect.attributePackageRequested += InvokeAttributePackageRequested;
    }
}
