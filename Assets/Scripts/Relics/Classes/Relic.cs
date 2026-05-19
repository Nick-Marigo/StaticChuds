using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

[JsonObject(MemberSerialization.Fields)]
public class Relic {
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

    /* After the Relic is deserialized, it generates
     * the appropiate Effect and Trigger using the
     * generator classes */
    [OnDeserialized]
    void OnDeserialization(StreamingContext context) {
        triggerGen.GenerateTrigger();
        effectGen.GenerateEffect();
    }
}
