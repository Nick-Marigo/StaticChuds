using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreeDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform skillTreeContent;
    [SerializeField] private RectTransform viewport;

    public void OnDrag(PointerEventData eventData)
    {
        skillTreeContent.anchoredPosition += eventData.delta;
        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector2 contentSize = skillTreeContent.rect.size;
        Vector2 viewportSize = viewport.rect.size;

        float maxX = (contentSize.x - viewportSize.x) /2f;
        float maxY = (contentSize.y - viewportSize.y) / 2f;

        float clampedX = Mathf.Clamp(skillTreeContent.anchoredPosition.x, -maxX, maxX);
        float clampedY = Mathf.Clamp(skillTreeContent.anchoredPosition.y, -maxY, maxY);

        skillTreeContent.anchoredPosition = new Vector2(clampedX, clampedY);
    }
}
