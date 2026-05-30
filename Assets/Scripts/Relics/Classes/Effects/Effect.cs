using UnityEngine;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

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

    private float _highlightDur = -1; 

    // Attribute Packages are used to access and change attributes
    // on the player
    public EntityAttributePackage attributePackage { set; get; }
    public event Action attributePackageRequested;
    public void InvokeAttributePackageRequested() {
        attributePackageRequested?.Invoke();
    }

    // Highlight event caught by the ui
    public event Action<float> highlightRequested;
    public event Action highlightStopRequested;

    /* This function suscribes the StopEffect function to the correct event based on
     * what was passed into the "until" attribute */
    void _SubscribeToStopCondition() {
        PlayerEventWrapper eventWrapper = (PlayerEventWrapper)attributePackage.AttributeDict["event_wrapper"].Get();
        switch(until){
            case(null):
                _highlightDur = 0.2f;
                break;
            case("move"):
                eventWrapper.playerMoved += _StopEffect;
                break;
            case("cast-spell"):
                eventWrapper.spellCast += _StopEffect;
                break;
            case var s when Regex.IsMatch(s, @"^\d+ seconds$"):
                Match match = Regex.Match(s, @"^(\d+) seconds$");
                var seconds = float.Parse(match.Groups[1].Value);
                //Debug.Log($"Seconds is {seconds}");
                IEnumerator timer(float time) {
                    yield return new WaitForSeconds(time);
                    _StopEffect();
                }

                // coroutine will start when PerformEffect is called
                highlightRequested += (_) => CoroutineManager.Instance.Run(timer(seconds));
                break;
        }
    }

    /* This function is called when the relic is claimed for setup */
    virtual public void Activate() {
        _SubscribeToStopCondition();
        // Get an attribute package from the owner
        attributePackageRequested?.Invoke();
    }

    virtual public void PerformEffect() {
        highlightRequested?.Invoke(_highlightDur);
    }

    public event Action effectStopped;

    virtual protected void _StopEffect() { 
        highlightStopRequested?.Invoke();
        // This event is emitted for any modules that might need to know when
        // their associated event has stopped
        effectStopped?.Invoke();
    }
}
