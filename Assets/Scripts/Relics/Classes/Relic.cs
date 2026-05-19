using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

[JsonObject(MemberSerialization.Fields)]
public class Relic {
    // TODO give relics an owner
    string name;
    int sprite;
    [JsonProperty("trigger")]
    TriggerGenerator triggerGen;
    [JsonProperty("effect")]
    EffectGenerator effectGen;

    [JsonIgnore]
    public Trigger trigger;
    [JsonIgnore]
    public Effect effect;

    [JsonConstructor]
    public Relic() {

    }

    /* After the Relic is deserialized, it generates the appropiate Effect and Trigger 
     * using the generator classes */
    [OnDeserialized]
    void OnDeserialization(StreamingContext context) {
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
    }
}
