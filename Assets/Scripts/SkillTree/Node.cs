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

    public Node(string name, string description, int icon, string branch, iNodeSystem system) {
        _system = system;
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.branch = branch;

        if (system == null) return;
        _obj = system.GetNodeObjectByType("mana", "damage");
        if (_obj == null) return;
        this.name = _obj.name;
    }

    public void Purchase() {
        isPurchased = true;
        _system.Equip(_obj);
    }
}
