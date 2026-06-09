using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class NodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    private Node node;
    private SkillTreeUI skillTreeUI;

    public void SetNode(Node node, SkillTreeUI skillTreeUI)
    {
        this.node = node;
        this.skillTreeUI = skillTreeUI;

        nameText.text = node.name;
        descriptionText.text = node.description;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionText.gameObject != null)
        {
            descriptionText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionText.gameObject != null)
        {
            descriptionText.gameObject.SetActive(false);
        }
    }

}
