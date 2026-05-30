using System;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    public GameObject slider;

    public SpellCaster spellCaster;
    float old_perc;

    private void UpdateManaBar(int mana) {
        //Debug.Log($"max_mana: {spellCaster.max_mana}, Mana: {spellCaster.Mana}");
        if (spellCaster == null) throw new ArgumentNullException("No spellcaster attached");
        if (spellCaster.max_mana == 0) return;
        float perc = spellCaster.Mana * 1.0f / spellCaster.max_mana;
        if (Mathf.Abs(old_perc - perc) > 0.01f) {
            slider.transform.localScale = new Vector3(perc, 1, 1); // scale
            slider.transform.localPosition = new Vector3(-(1 - perc) / 2, 0, 0); // reposition
            old_perc = perc;
        }
    }

    public void SetSpellCaster(SpellCaster sc) {
        this.spellCaster = sc;
        old_perc = 0;
        spellCaster.manaChanged += UpdateManaBar;
    }
}
