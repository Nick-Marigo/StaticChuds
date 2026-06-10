using UnityEngine;

public class Node
{
    public string name;
    public string description;
    public string branch;
    public bool isPurchased = false;
    public int icon;

    private iNodeSystem _system;
    private iNodeObject _obj;

    public Node(string branch, iNodeSystem system) {
        this.branch = branch;
        if (system == null) {
            this.icon = 0; // Set root icon
            return;
        }

        _system = system;
        _obj = system.GetNodeObjectByType("mana", "damage");
        if (_obj == null) return;
        this.name = _obj.name;
        this.description = _obj.description;
        this.icon = _obj.icon;
    }

    public void Purchase() {
        isPurchased = true;
        _system.Equip(_obj);
    }
}
