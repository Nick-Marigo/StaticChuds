using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RelicUI : MonoBehaviour
{
    public PlayerInstance player;
    public int index;

    [SerializeField] Image icon;
    [SerializeField] GameObject highlight;
    [SerializeField] TextMeshProUGUI relicName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] GameObject takeButton;

    private Relic relic;
    private Action<float> _highlightOnAdapter;
    private Action _highlightOffAdapter;

    public void SetRelicDisplay(Relic relic)
    {
        this.relic = relic;
        GameManager.Instance.relicIconManager.PlaceSprite(relic.icon, icon);
        // Subscribe to highlight highlight events
        _highlightOnAdapter = (float delay) => StartCoroutine(EnableHighlight(delay));
        _highlightOffAdapter = () => DisableHighlight();
        relic.effect.highlightRequested += _highlightOnAdapter;
        relic.effect.highlightStopRequested += _highlightOffAdapter;
    }

    void OnDisable() {
        if (relic != null) {
            relic.effect.highlightRequested -= _highlightOnAdapter;
            relic.effect.highlightStopRequested -= _highlightOffAdapter;
        }
    }
    // TODO: Subcribe to an event
    public IEnumerator EnableHighlight(float delay)
    {
        Debug.Log("highlight called");
        if (highlight.gameObject == null) yield break;
        highlight.SetActive(true);
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
            if (highlight != null)
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

    public void SetRelicRewardDisplay(Relic relic)
    {
        relicName.text = relic.name;

        description.text = relic.trigger.description;
        description.text = description.text + " " + relic.effect.description;
        GameManager.Instance.relicIconManager.PlaceSprite(relic.icon, icon);
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
