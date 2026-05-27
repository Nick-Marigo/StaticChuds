using UnityEngine;
using System.Collections.Generic;

public class RelicUIManager : MonoBehaviour
{
    public GameObject relicUIPrefab;
    public PlayerController player;

    List<GameObject> relicUIObjects = new List<GameObject>();

    public void RefreshRelicUI()
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

            relicUIObj.transform.localPosition = new Vector3(40 * i, 0, 0);
            RelicUI relicUI = relicUIObj.GetComponent<RelicUI>();

            relicUI.SetRelicDisplay(relic);
            relicUIObjects.Add(relicUIObj);
            i++;
        }
    }
}
