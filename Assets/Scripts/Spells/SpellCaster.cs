using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellCaster 
{
    public int mana = 0;
    public int max_mana;
    public int mana_reg;
    public Hittable.Team team;
    public List<Spell> spells = new List<Spell>();
    public Spell spell
    {
        get
        {
            return GetSelectedSpell();
        }
    }
    public int selectedSpellIndex = 0;
    public const int MAXSPELLS = 4;
    public int spellPower = 5;

    public IEnumerator ManaRegeneration()
    {
        while (true)
        {
            mana += mana_reg;
            mana = Mathf.Min(mana, max_mana);
            yield return new WaitForSeconds(1);
        }
    }

    public SpellCaster(Hittable.Team team)
    {
        this.team = team;
        spells.Add(SpellBuilder.BuildArcaneBolt(this));
    }

    public IEnumerator Cast(Vector3 where, Vector3 target)
    {        
        if (spells.Count == 0) yield break;

        Spell selectedSpell = spells[selectedSpellIndex];

        if (mana >= selectedSpell.GetManaCost() && selectedSpell.IsReady())
        {
            selectedSpell.UpdateDicts(GameManager.Instance.currentWave);
            mana -= selectedSpell.GetManaCost();
            selectedSpell.last_cast = Time.time;
            yield return selectedSpell.Cast(where, target, team);
        }
        yield break;
    }

    public Spell GetSelectedSpell()
    {
        if (spells.Count == 0) return null;
        return spells[selectedSpellIndex];
    }

    public void SelectSpell(int index)
    {
        if (index < 0 || index >= spells.Count) return;
        selectedSpellIndex = index;
    }

    public bool AddSpell(Spell newSpell)
    {
        if (spells.Count >= MAXSPELLS) return false;
        spells.Add(newSpell);
        return true;
    }

    public void RemoveSpellAt(int index)
    {
        if (index < 0 || index >= spells.Count) return;
        spells.RemoveAt(index);

        if (selectedSpellIndex >= spells.Count)
        {
            selectedSpellIndex = Mathf.Max(0, spells.Count - 1);
        }
    }

    public void SetStats(int newMana, int newManaRegen, int newSpellPower)
    {
        // For Debug
        //int oldMana = this.mana;
        //int oldMax = this.max_mana;

        this.mana = newMana;
        this.max_mana = newMana;
        //float perc = this.mana * 1.0f / this.max_mana;
        //this.mana = Mathf.RoundToInt(perc * newMana);

        this.mana_reg = newManaRegen;
        this.spellPower = newSpellPower;

        //Debug.Log("OldMana: " + oldMana + " NewMana: " + this.mana + " OldMax: " + oldMax + " NewMax: " + this.max_mana);
    }

}
