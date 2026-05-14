using UnityEngine;
using System.Collections.Generic;

public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    public PlayerController player;

    public void RefreshSpells(List<Spell> spells)
    {
        for (int i = 0; i < spellUIs.Length; i++)
        {
            bool hasSpell = i < spells.Count;

            spellUIs[i].SetActive(hasSpell);

            if (hasSpell)
            {
                SpellUI spellUI = spellUIs[i].GetComponent<SpellUI>();
                spellUI.SetSpell(spells[i]);
                spellUI.ShowDropButton(false);
            }
        }
    }

    public void DropSpell(int index)
    {
        player.spellcaster.RemoveSpellAt(index);
        RefreshSpells(player.spellcaster.spells);
    }

    public void ShowDropButtons(bool show)
    {
        for (int i = 0; i < spellUIs.Length; i++)
        {
            if (!spellUIs[i].activeSelf) continue;

            SpellUI spellUI = spellUIs[i].GetComponent<SpellUI>();
            spellUI.ShowDropButton(show);
        }
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
