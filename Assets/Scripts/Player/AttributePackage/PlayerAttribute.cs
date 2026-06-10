public class PlayerAttribute<T> {

    T value;
    string type;
    
    public PlayerAttribute(T value, string type) {
        this.value = value;
        this.type = type;
    }
}
