public class NewWaveTrigger : Trigger {

    public NewWaveTrigger(string description, string type) {
        this.description = description;
        this.type = type;
    }

    override public void Activate() {
        base.Activate();
        EventBus.Instance.OnWaveStart += OnNewWave;
    }

    private void OnNewWave(int waveNum) {
        InvokeEffect();
    }

}
