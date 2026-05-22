using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RelicUI : MonoBehaviour
{
    public PlayerController player;
    public int index;

    public Image icon;
    public GameObject highlight;
    public TextMeshProUGUI label;

    public void SetRelicDisplay(Relic relic)
    {
        label.text = "";
        GameManager.Instance.relicIconManager.PlaceSprite(relic.sprite, icon);
    }

    // TODO: Subcribe to an event
    public IEnumerator EnableHighlight(float delay)
    {
        highlight.SetActive(true);
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
            highlight.SetActive(false);
        }
        else
        {
            yield break;
        }
    }

    // TODO: Subcribe to an event
    public void DisableHighlight()
    {
        highlight.SetActive(false);
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
