using UnityEngine;
using Newtonsoft.Json;
using System;

[JsonObject(MemberSerialization.OptIn)]
abstract public class Effect : iRequestAttributePackage {
    [JsonProperty]
    public string description { get; protected set; }
    [JsonProperty]
    protected string type;
    [JsonProperty]
    protected string amount;
    [JsonProperty]
    protected string until;

    // Attribute Packages are used to access and change attributes
    // on the player
    public EntityAttributePackage attributePackage { set; get; }
    public event Action attributePackageRequested;
    public void InvokeAttributePackageRequested() {
        attributePackageRequested?.Invoke();
    }
    
    /* This function suscribes the StopEffect function to the correct event based on
     * what was passed into the "until" attribute */
    void _SubscribeToStopCondition() {
        Debug.Log(attributePackage.AttributeDict);
        PlayerEventWrapper eventWrapper = (PlayerEventWrapper)attributePackage.AttributeDict["event_wrapper"].Get();
        switch(until){
            case("move"):
                eventWrapper.playerMoved += _StopEffect;
                break;
            case("cast-spell"):
                eventWrapper.spellCast += _StopEffect;
                break;
            default:
                return;
        }
    }

    /* This function is called when the relic is claimed for setup */
    virtual public void Activate() {
        // Subscribe to stop condition at start of first wave after creation
        Action<int> subscriber = null;
        subscriber = (_) => {
            _SubscribeToStopCondition();
            EventBus.Instance.OnWaveStart -= subscriber;
        };

        EventBus.Instance.OnWaveStart += subscriber;
        attributePackageRequested?.Invoke();
    }

    abstract public void PerformEffect();

    public event Action effectStopped;
    public void InvokeEffectStopped() {
        effectStopped?.Invoke();
    }
    virtual protected void _StopEffect() { effectStopped?.Invoke(); }
}
