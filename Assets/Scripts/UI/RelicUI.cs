using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicUI : MonoBehaviour
{
    public PlayerController player;
    public int index;

    public Image icon;
    public GameObject highlight;
    public TextMeshProUGUI label;

    public void SetRelic(Relic relic)
    {
        label.text = relic.name;
        GameManager.Instance.relicIconManager.PlaceSprite(relic.GetSprite(), icon);
    }

    // TODO: Subcribe to event maybe?
    public void ShowHighLight(bool show)
    {
        highlight.SetActive(show);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if a player has relics, this is how you *could* show them
        /*
        Relic r = player.relics[index];
        GameManager.Instance.relicIconManager.PlaceSprite(r.sprite, icon);
        */
    }

    // Update is called once per frame
    void Update()
    {
        // Relics could have labels and/or an active-status
        /*
        Relic r = player.relics[index];
        label.text = r.GetLabel();
        highlight.SetActive(r.IsActive());
        */
    }
}
