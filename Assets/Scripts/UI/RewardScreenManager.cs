using UnityEngine;
using TMPro;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    [SerializeField] PlayerController player;
    [SerializeField] SpellUI rewardSpellUI;
    [SerializeField] TextMeshProUGUI rewardDescription;

    Spell rewardSpell;

    public void ShowReward()
    {
        rewardSpell = SpellBuilder.Build(player.spellcaster);
        rewardSpellUI.SetSpell(rewardSpell);
        rewardSpellUI.ShowDropButton(false);

        rewardDescription.text = rewardSpell.GetFullDescription();
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

        GameManager.Instance.state = GameManager.GameState.WAVESTATS;
    }

    public void ClearReward()
    {
        rewardSpell = null;
        rewardSpellUI.ShowDropButton(false);
        player.spellUIContainer.ShowDropButtons(false);
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
