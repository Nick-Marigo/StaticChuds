using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class EffectGenerator {
    protected string description;
    protected string type;
    protected string amount;
    protected string until;
}
