using UnityEngine;

public class Node
{
    public string name;
    public string description;
    public string branch;
    public bool isPurchased = false;
    public int icon;

    private iNodeObject _system;

    public Node(string name, string description, int icon, string branch, iNodeObject system) {
        _system = system;
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.branch = branch;
    }

    public void Purchase() {
        isPurchased = true;
        _system.Equip();
    }
}
