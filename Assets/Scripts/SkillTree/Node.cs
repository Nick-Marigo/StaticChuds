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
        if (system != null) { 
            _obj = system.GetNodeObjectByType("mana", "damage");
            Debug.Log($"got {_obj}");
        }
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.branch = branch;
    }

    public void Purchase() {
        isPurchased = true;
        _system.Equip(_obj);
    }
}
