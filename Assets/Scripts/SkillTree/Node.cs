using UnityEngine;

public class Node
{
    public string name;
    public string description;
    public string branch;
    public bool isPurchased = false;
    public int icon;

    private iNodeObject _obj;

    public Node(string name, string description, int icon, string branch ) {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.branch = branch;
    }

    public void Purchase() {
        isPurchased = true;
    }


}
