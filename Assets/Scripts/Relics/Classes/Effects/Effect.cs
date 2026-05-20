using System;
using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
abstract public class Effect : IDisposable {
    protected string description;
    protected string type;
    protected string amount;
    protected string until;

    // The Relic this effect belongs to
    [JsonIgnore]
    protected Relic relic;

    abstract public void PerformEffect();
    /* Called whenever a relic's owner is changed to update the system ref in the effect.
     * For example, if a effect needs access to the owner's spellcaster, it will update
     * the ref whenever the relic changes owners through this function*/
    abstract public void ChangeOwner(GameObject owner);

    /* IMPORTANT
     * All effects that suscribe to an event MUST override this function 
     * to unsuscribe to events when the object is destroyed */
    virtual protected void Unsuscribe() {
        return;
    }

    virtual public void Dispose() {
        Unsuscribe();
    }

    ~Effect() {
        Dispose();
    }
}
