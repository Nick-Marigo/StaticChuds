using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] GameObject nameObject;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject descriptionObject;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image nodeFrame;
    [SerializeField] Color purchaseColor = Color.red;
    [SerializeField] Image nodeImage;
    LineUI incomingLine;

    private Node node;
    private SkillTreeUI skillTreeUI;

    public void SetNode(Node node, SkillTreeUI skillTreeUI)
    {
        this.node = node;
        this.skillTreeUI = skillTreeUI;

        nameText.text = node.name;
        descriptionText.text = node.description;
        SetSprite();
    }

    void SetSprite()
    {
        switch (node.branch)
        {
            case "Spells":
                GameManager.Instance.spellIconManager.PlaceSprite(node.icon, nodeImage);
                break;
            case "Relics":
                GameManager.Instance.relicIconManager.PlaceSprite(node.icon, nodeImage);
                break;
            // TOADD
            /*case "Stats":
                GameManager.Instance.statsIconManager.PlaceSprite(node.icon, nodeImage);
                break;*/ 
            default:
                Debug.Log("Branch not found for image");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (nameObject != null)
        {
            nameObject.SetActive(true);
        }

        if (descriptionObject != null)
        {
            descriptionObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (nameObject != null)
        {
            nameObject.SetActive(false);
        }

        if (descriptionObject != null)
        {
            descriptionObject.SetActive(false);
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
            node.isPurchased = true;
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
        nodeFrame.color = purchaseColor;
    }

    public void SetRootNull()
    {
        nameObject = null;
        descriptionObject = null;
    }
}
