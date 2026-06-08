using UnityEngine;
using System.Collections.Generic;

public class SkillTree
{
    public int skillPoints = 5;

    public SkillTree()
    {
        
    }
    
    public void NodePurchased(Node node)
    {
        if (skillPoints > 0)
        {
            skillPoints--;
            node.isUnlocked = true;
        }
    }
}
