using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class Relic {
    string name;
    int sprite;
    TriggerGenerator trigger;
    EffectGenerator effect;
}
