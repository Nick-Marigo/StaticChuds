using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

[JsonObject(MemberSerialization.Fields)]
public class Relic {
    string name;
    int sprite;
    TriggerGenerator trigger;
    EffectGenerator effect;

    public Trigger Trigger;
    public Effect Effect;

    [JsonConstructor]
    public Relic() {

    }

    [OnDeserialized]
    void OnDeserialization(StreamingContext context) {
    }
}
