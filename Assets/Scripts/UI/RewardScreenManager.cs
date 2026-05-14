using UnityEngine;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    [SerializeField] PlayerController player;
    [SerializeField] SpellUI rewardSpellUI;

    Spell rewardSpell;

    public void ShowReward()
    {
        rewardSpell = new SpellBuilder().Build(player.spellcaster);
        rewardSpellUI.SetSpell(rewardSpell);
        rewardSpellUI.ShowDropButton(false);
    }

    public void GenerateReward(PlayerController player)
    {
        rewardSpell = new SpellBuilder().Build(player.spellcaster);
    }

    public void AcceptReward()
    {
        bool added = player.spellcaster.AddSpell(rewardSpell);

        if (!added)
        {
            Debug.Log("Spell inventory full. Drop a spell first. ");
            player.spellUIContainer.ShowDropButtons(true);
            return;
        }

        player.spellUIContainer.RefreshSpells(player.spellcaster.spells);
        player.spellUIContainer.ShowDropButtons(false);
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
