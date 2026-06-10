using UnityEngine;
using System.Collections.Generic;

public class SkillTree
{
    public int skillPoints = 100;

    private PlayerInstance _owner;

    // TOREMOVE: For testing nodes when building out the branch
    int spellbranchCount = 0;
    int relicbranchCount = 0;
    int statsbranchCount = 0;


    public SkillTree(PlayerInstance owner) {
        this._owner = owner;
    }

    public Node CreateRoot()
    {
        Node newNode = new Node("Root", "Root", 0, "Root", null);
        return newNode;
    }

    //TODO: determine what spell or mod should spawn based on player class
    public Node CreateNodeSpell()
    {
        spellbranchCount++;
        Node newNode = new Node("Spell " + spellbranchCount, "Spell " + spellbranchCount, 0, "Spells", _owner.spellCaster);
        return newNode;
    }

    //TODO: determine what relic should spawn based on player class
    public Node CreateNodeRelic()
    {
        relicbranchCount++;
        Node newNode = new Node("Relic " + relicbranchCount, "Relic " + relicbranchCount, 0, "Relics", _owner.relicInventory);
        return newNode;
    }

    //TODO: determine what Stat should spawn based on player class
    public Node CreateNodeStat()
    {
        statsbranchCount++;
        Node newNode = new Node("Stats " + statsbranchCount, "Stats " + statsbranchCount, 0, "Stats", _owner.AttributePackage);
        return newNode;
    }
    
    public bool canPurchased()
    {
        if (skillPoints > 0)
        {
            skillPoints--;
            return true;
        }

        return false;

    }
}
