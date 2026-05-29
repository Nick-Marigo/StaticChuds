public class CastSpellTrigger : Trigger {

    public CastSpellTrigger(string description, string type) {
        this.description = description;
        this.type = type;
    }

    override public void Activate() {
        base.Activate();
        PlayerEventWrapper eventWrapper = (PlayerEventWrapper)attributePackage.AttributeDict["event_wrapper"].Get();
        eventWrapper.spellCast += InvokeEffect;
    }
}
