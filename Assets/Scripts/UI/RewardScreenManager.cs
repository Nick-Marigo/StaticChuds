using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    [SerializeField] PlayerController player;
    [SerializeField] SpellUI rewardSpellUI;
    [SerializeField] TextMeshProUGUI rewardDescription;


    List<Relic> relicChoices = new List<Relic>();


    Spell rewardSpell;

    public void ShowReward() {
        rewardSpell = SpellBuilder.Build(player.spellcaster);
        rewardSpellUI.SetSpell(rewardSpell);
        rewardSpellUI.ShowDropButton(false);

        rewardDescription.text = rewardSpell.GetFullDescription();
    }

    public void AcceptReward() {
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

        GameManager.Instance.state = GameManager.GameState.WAVESTATS;
    }

    public void ClearReward()
    {
        rewardSpell = null;
        rewardSpellUI.ShowDropButton(false);
        player.spellUIContainer.ShowDropButtons(false);
    }

    public void ShowRelicReward()
    {
        GetRelicChoices();

    }

    void GetRelicChoices()
    {
        relicChoices.Clear();

        while (relicChoices.Count < 3)
        {
            Relic relicReward = player.relicInventory.FetchUnusedRelic();

            if (relicReward == null)
            {
                //Set some text that says no more relics
                break;
            }

            if (!relicChoices.Contains(relicReward))
            {
                relicChoices.Add(relicReward);
            }
        }
    }
}
