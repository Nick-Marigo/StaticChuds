using UnityEngine;

public class Node
{
    string name;
    string description;
    string branch;
    bool isUnlocked = false;
    Sprite icon;

    public Node(string name, string description, Sprite icon, string branch)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.branch = branch;
    }

    
}
