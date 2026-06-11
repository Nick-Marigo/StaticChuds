using UnityEngine;

public class Node
{
    public string name;
    public string description;
    public string branch;
    public bool isPurchased = false;
    public int icon;
    public int depth;

    private iNodeSystem _system;
    public iNodeObject obj { get; private set; }

    public Node(string branch, iNodeSystem system, (string affinity, string weakness) types) {
        this.branch = branch;
        if (branch == "Root" || system == null) {
            this.icon = 0; // Set root icon
            this.name = "Root";
            return;
        }

        _system = system;
        obj = system.GetNodeObjectByType(types.affinity, types.weakness);
        if (obj == null) return;
        this.name = obj.name;
        this.description = obj.description;
        this.icon = obj.icon;

        if (obj is SpellModifier modifier)
        {
            this.icon = modifier.GetNodeIcon();
        }
    }

    public void Purchase() {
        isPurchased = true;
        _system.Equip(obj);
    }
}
