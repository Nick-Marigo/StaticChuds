using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

[JsonObject(MemberSerialization.OptIn)]
public class Relic {
    [JsonProperty]
    public string name { get; protected set; }
    [JsonProperty]
    protected int sprite;
    [JsonProperty("trigger")]
    TriggerGenerator triggerGen;
    [JsonProperty("effect")]
    EffectGenerator effectGen;

    public Trigger trigger;
    public Effect effect;

    private GameObject owner;
    public GameObject Owner {
        get {
            return owner;
        }
        set {
            /* Updates the system reference in
             * effect and trigger. See more notes in Effect.cs*/
            trigger.ChangeOwner(value);
            effect.ChangeOwner(value);
        }
    }
 
    public Relic() {

    }

    /* After the Relic is deserialized, it generates the appropiate Effect and Trigger 
     * using the generator classes */
    [OnDeserialized]
    void OnDeserialization(StreamingContext context) {

        trigger = triggerGen.GenerateTrigger(this);
        if (trigger == null) {
            Debug.Log("failed to load trigger for " + name + " relic");
            return;
        }
        effect = effectGen.GenerateEffect(this);
        if (effect == null) {
            Debug.Log("failed to load effect for " + name + " relic");
            return;
        }
    }
}
