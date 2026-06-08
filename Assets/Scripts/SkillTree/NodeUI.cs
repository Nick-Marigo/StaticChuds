using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NodeUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nameText;

    private Node node;
    private SkillTreeUI skillTreeUI;

    public void SetNode(Node node, SkillTreeUI skillTreeUI)
    {
        this.node = node;
        this.skillTreeUI = skillTreeUI;

        nameText.text = node.name;
    }

    public void NodeClicked()
    {
        if (!node.isPurchased && node.name != "Root")
        {
            node.isPurchased = true;
            RectTransform rect = GetComponent<RectTransform>();
            skillTreeUI.NodeClicked(node, rect.anchoredPosition);
        }
    }

}
