public class AudioEmitter {

	public void EmitSound(AudioIdentifier sound) {
		EventBus.Instance.InvokePlaySound(sound);
	}
    
}
