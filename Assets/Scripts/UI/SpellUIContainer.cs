using UnityEngine;
using System.Collections.Generic;

public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    private SpellCaster _spellCaster;
    public SpellCaster SpellCaster {
        set { 
            _spellCaster = value;
            _spellCaster.spellSelected += UpdateSelectedHighlight;
        }
    }

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

        UpdateSelectedHighlight(_spellCaster.selectedSpellIndex);
    }

    public void DropSpell(int index)
    {
        _spellCaster.RemoveSpellAt(index);
        RefreshSpells(_spellCaster.spells);
        ShowDropButtons(false);
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

    public void UpdateSelectedHighlight(int selectedIndex)
    {
        for (int i = 0; i < spellUIs.Length; i++)
        {
            if (!spellUIs[i].activeSelf) continue;

            SpellUI spellUI = spellUIs[i].GetComponent<SpellUI>();
            spellUI.ShowHighlight(i == selectedIndex);
        }
    }
}
