using UnityEngine;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;

    Spell rewardSpell;

    public void GenerateReward(PlayerController player)
    {
        rewardSpell = new SpellBuilder().Build(player.spellcaster);
    }

    public void AcceptReward(PlayerController player)
    {
        bool added = player.spellcaster.AddSpell(rewardSpell);

        if (!added)
        {
            Debug.Log("Spell inventory full. Drop a spell first. ");
            return;
        }

        rewardSpell = null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
