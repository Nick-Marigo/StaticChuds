using System;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject(MemberSerialization.Fields)]
abstract public class Trigger : IDisposable {
    protected string description;
    protected string type;
    protected string amount;

    // The relic this trigger belongs to
    [JsonIgnore]
    protected Relic relic;

    virtual protected void InvokeEffect() {
        if (relic.effect == null) {
            Debug.Log("relic " + relic.name + " has a trigger but no effect");
            return;
        }
        relic.effect.PerformEffect();
    }

    virtual public void ChangeOwner(GameObject owner) {
        return;
    }

    /* IMPORTANT
     * All triggers that suscribe to an event MUST override this function 
     * to unsuscribe to events when the object is destroyed */
    virtual protected void Unsuscribe() {
    }

    public void Dispose() {
        Unsuscribe();
    }

    ~Trigger() {
        Dispose();
    }
}
