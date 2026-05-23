using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class RewardScreenManager : MonoBehaviour
{
    const int _numRewardRelics = 3;

    public GameObject rewardUI;
    [SerializeField] PlayerController player;
    [SerializeField] SpellUI rewardSpellUI;
    [SerializeField] TextMeshProUGUI rewardDescription;

    [SerializeField] RelicUI[] relicChoiceUI;
    [SerializeField] RelicUIManager relicUIManager;
    [SerializeField] TextMeshProUGUI relicRewardMessage;
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

        // TO FIX
        //if (GameManager.Instance.currentWave % 3 == 0)
        if (true)
        {
            GameManager.Instance.state = GameManager.GameState.RELICREWARD;
        } 
        else
        {
            GameManager.Instance.state = GameManager.GameState.WAVESTATS;
        }

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

        if (relicChoices.Count == 0)
        {
            relicRewardMessage.text = "No more Relics to choose from!";

            for (int i = 0; i < relicChoiceUI.Length; i++)
            {
                relicChoiceUI[i].gameObject.SetActive(false);
            }

            return;
        }

        relicRewardMessage.text = "Choose a Relic!";

        for (int i = 0; i < relicChoiceUI.Length; i++)
        {
            if (i < relicChoices.Count)
            {
                relicChoiceUI[i].gameObject.SetActive(true);
                relicChoiceUI[i].SetRelicRewardDisplay(relicChoices[i]);
            }
            else
            {
                relicChoiceUI[i].gameObject.SetActive(false);
            }
        }

    }

    void GetRelicChoices()
    {
        relicChoices.Clear();
        relicChoices = player.relicInventory.FetchUnusedRelics(_numRewardRelics);
    }

    public void TakeRelic1() => TakeRelic(0);
    public void TakeRelic2() => TakeRelic(1);
    public void TakeRelic3() => TakeRelic(2);

    void TakeRelic(int takenRelic)
    {
        if (takenRelic < 0 || takenRelic >= relicChoices.Count)
        {
            return;
        }

        Relic relic = relicChoices[takenRelic];
        player.relicInventory.EquipRelic(relic);

        relicUIManager.RefreshRelicUI();

        GameManager.Instance.state = GameManager.GameState.WAVESTATS;

    }
}
