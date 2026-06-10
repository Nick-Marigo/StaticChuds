using UnityEngine;
using System.Collections.Generic;

public class RelicUIManager : MonoBehaviour
{
    public GameObject relicUIPrefab;
    public PlayerInstance player;

    List<GameObject> relicUIObjects = new List<GameObject>();

    private void OnDestroy() {
        player.relicInventory.relicEquipped -= RefreshRelicUI;
    }

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

            relicUIObj.transform.localPosition = new Vector3(GetRelicPlacementOffset(i), 0, 0);
            RelicUI relicUI = relicUIObj.GetComponent<RelicUI>();

            relicUI.SetRelicDisplay(relic);
            relicUIObjects.Add(relicUIObj);
            i++;
        }
    }
    
    private int GetRelicPlacementOffset(int relicIndex) {
        // Places first in middle then alternates placement on right then left
        // Relic index == # of relics owned before obtaining
        if (relicIndex == 0) return 0;
			
        int sign = (relicIndex % 2) * 2 - 1; // right is signed positive, left is signed negative
        int offsetShift = (relicIndex + 1) / 2; // Actual offset from index=0
			
        return (sign * offsetShift) * 40; // 40 pixels of spacing between object origins
    }
}
