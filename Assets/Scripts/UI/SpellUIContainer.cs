using UnityEngine;

public class SpellUIContainer : MonoBehaviour
{
    [SerializeField] private GameObject spellContainer; 
    [SerializeField] private GameObject skillTreeOverlay; // activate to stop selection in skilltree while upgading

    private readonly int upgradeTranslateMag = 462; 
    private bool _spellContaineroffset = false;

    public GameObject[] spellUIs;
    private SpellCaster _spellCaster;
    public SpellCaster SpellCaster {
        set { 
            _spellCaster = value;
            _spellCaster.spellSelected += UpdateSelectedHighlight;
            _spellCaster.spellUpgradeInitiated += ShowUpgradeOverlays;
            _spellCaster.spellUpgraded += RefreshSpells;
        }
    }

    public void RefreshSpells() {
        var spells = _spellCaster.spells;
        for (int i = 0; i < spellUIs.Length; i++)
        {
            bool hasSpell = i < spells.Count;

            spellUIs[i].SetActive(hasSpell);

            if (hasSpell) {
                SpellUI spellUI = spellUIs[i].GetComponent<SpellUI>();
                spellUI.SetSpell(spells[i]);
                spellUI.ShowDropButton(false);
                spellUI.ShowUpgradeOverlay(false);
            }
            if (_spellContaineroffset) {
                spellContainer.transform.Translate(new Vector3 (0, -upgradeTranslateMag, 0));
                skillTreeOverlay.SetActive(false);
                _spellContaineroffset = false;
            }
        }

        UpdateSelectedHighlight(_spellCaster.selectedSpellIndex);
    }

    public void DropSpell(int index)
    {
        _spellCaster.RemoveSpellAt(index);
        RefreshSpells();
        ShowDropButtons(false);
    }

    public void ModSpell(int index) {
        _spellCaster.ModSpellAt(index);
    }

    public void ShowUpgradeOverlays(bool show) {
        //spellContainer.GetComponent<RectTransform>();
        for (int i = 0; i < spellUIs.Length; i++) {
            if (!spellUIs[i].activeSelf) continue;

            SpellUI spellUI = spellUIs[i].GetComponent<SpellUI>();
            spellUI.ShowUpgradeOverlay(show);
        }

        spellContainer.transform.Translate(new Vector3 (0, upgradeTranslateMag, 0));
        skillTreeOverlay.SetActive(true);
        _spellContaineroffset = true;
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

    void OnDestroy() {
        if (_spellCaster == null) return;
        _spellCaster.spellSelected -= UpdateSelectedHighlight;
        _spellCaster.spellUpgradeInitiated -= ShowUpgradeOverlays;
        _spellCaster.spellUpgraded -= RefreshSpells;
    }
}
