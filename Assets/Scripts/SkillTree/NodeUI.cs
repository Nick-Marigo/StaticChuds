using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image nodeImage;
    [SerializeField] Color purchaseColor = Color.red;
    LineUI incomingLine;

    private Node node;
    private SkillTreeUI skillTreeUI;

    public void SetNode(Node node, SkillTreeUI skillTreeUI)
    {
        this.node = node;
        this.skillTreeUI = skillTreeUI;

        nameText.text = node.name;
        descriptionText.text = node.description;
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

    public void SetIncomingLine(LineUI line)
    {
        incomingLine = line;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (node.isPurchased || node.name == "Root")
        {
            return;
        }

        if (incomingLine == null)
        {
            return;
        }

        incomingLine.StartFill(() =>
        {
            node.Purchase();
            SetPurchasedColor();

            RectTransform rect = GetComponent<RectTransform>();
            skillTreeUI.NodeClicked(node, rect.anchoredPosition);
        });
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (node.isPurchased)
        {
            return;
        }

        if (incomingLine != null)
        {
            incomingLine.CancelFill();
        }
    }

    public void SetPurchasedColor()
    {
        nodeImage.color = purchaseColor;
    }
}
