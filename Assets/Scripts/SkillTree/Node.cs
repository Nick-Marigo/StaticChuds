using UnityEngine;

public class Node
{
    public string name;
    public string description;
    public string branch;
    public bool isPurchased = false;
    public int icon;

    private iNodeSystem _system;
    public iNodeObject obj { get; private set; }

    public Node(string branch, iNodeSystem system) {
        this.branch = branch;
        if (branch == "Root") {
            this.icon = 0; // Set root icon
            this.name = "Root";
            return;
        }

        _system = system;
        obj = system.GetNodeObjectByType("mana", "damage");
        if (obj == null) return;
        this.name = obj.name;
        this.description = obj.description;
        this.icon = obj.icon;
    }

    public void Purchase() {
        isPurchased = true;
        _system.Equip(obj);
    }
}
