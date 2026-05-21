using UnityEngine;

public class RelicUIManager : MonoBehaviour
{
    public GameObject relicUIPrefab;
    public PlayerController player;

    List<GameObject> relicUIObjects = new List<GameObject>();

    void RefreshRelicUI()
    {
        foreach (GameObject obj in relicUIObjects)
        {
            Destroy(obj);
        }

        relicUIObjects.Clear();

        Dictionary<string, Relic> relics = player.relicInventory.GetEquippedRelics();

        int i = 0;
        foreach (Relic relic in relics.Values)
        {
            GameObject relicUIObj = Instantiate(relicUIPrefab, transform);

            relicUIObj.transform.localPosition = new Vector3(40 * index, 0, 0);
            relicUI relicUI = relicUIObj.GetComponent<relicUI>();

            relicUI.SetRelic(relic);
            relicUIObjects.Add(relicUIObj);
            i++;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //EventBus.Instance.OnRelicPickup += OnRelicPickup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void OnRelicPickup(Relic r)
    {
        // make a new Relic UI representation
        GameObject rui = Instantiate(relicUIPrefab, transform);
        rui.transform.localPosition = new Vector3(-450 + 40 * (player.relics.Count - 1), 0, 0);
        RelicUI ruic = rui.GetComponent<RelicUI>();
        ruic.player = player;
        ruic.index = player.relics.Count - 1;
        
    }*/
}
