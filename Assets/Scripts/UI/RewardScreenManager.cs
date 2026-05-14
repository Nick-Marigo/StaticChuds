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
        rewardSpell = new SpellBuilder().Build(player.spellcaster);
        rewardSpellUI.SetSpell(rewardSpell);
        rewardSpellUI.ShowDropButton(false);

        rewardDescription.text = rewardSpell.GetFullDescription();
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
            rewardSpellUI.ShowDropButton(true);
            return;
        }

        player.spellUIContainer.RefreshSpells(player.spellcaster.spells);
        player.spellUIContainer.ShowDropButtons(false);
        rewardSpell = null;

        GameManager.Instance.state = GameManager.GameState.WAVESTATS;
    }

    public void DropReward()
    {
        rewardSpell = null;
        rewardSpellUI.ShowDropButton(false);
        player.spellUIContainer.ShowDropButtons(false);
        GameManager.Instance.state = GameManager.GameState.WAVESTATS;
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
