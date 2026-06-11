using UnityEngine;
using System.Collections.Generic;

public class SkillTree
{
    public int skillPoints = 0;

    private PlayerInstance _owner;

    int spellbranchCount = 0;
    int relicbranchCount = 0;
    int statsbranchCount = 0;

    public SkillTree(PlayerInstance owner) {
        this._owner = owner;
    }

    public Node CreateRoot()
    {
        Node newNode = new Node("Root", null);
        return newNode;
    }

    public Node CreateNodeSpell()
    {
        spellbranchCount++;
        Node newNode = new Node("Spells", _owner.spellCaster);
        return newNode.obj == null ? null : newNode;
    }

    public Node CreateNodeRelic()
    {
        relicbranchCount++;
        Node newNode = new Node("Relics", _owner.relicInventory);
        return newNode.obj == null ? null : newNode;
    }

    public Node CreateNodeStat()
    {
        statsbranchCount++;
        Node newNode = new Node("Stats", _owner.AttributePackage);
        return newNode.obj == null ? null : newNode;
    }
    
    public bool CanPurchase() {
        return skillPoints > 0;
    }
}
