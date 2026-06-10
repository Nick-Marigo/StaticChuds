using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreeDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform skillTreeContent;

    public void OnDrag(PointerEventData eventData)
    {
        skillTreeContent.anchoredPosition += eventData.delta;
    }
}
