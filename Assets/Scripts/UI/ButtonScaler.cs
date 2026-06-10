using UnityEngine;
using static UnityEngine.RectTransform;

public class ButtonScaler : MonoBehaviour {

    [SerializeField] private Canvas canvas;
    [SerializeField] public float xOffset = 0.1f;
    [SerializeField] public float yOffset = 0.1f;
    [SerializeField] public float xScale = 0.5f;
    [SerializeField] public float yScale = 0.5f;

    private RectTransform _rt;
    private RectTransform _rtParent;

    private void Start() {
        _rt = GetComponent<RectTransform>();
        _rtParent = _rt.parent as RectTransform;
    }

    private void Update() {
        float parentWidth  = _rtParent.rect.width;
        float parentHeight = _rtParent.rect.height;

        _rt.SetInsetAndSizeFromParentEdge(Edge.Left, parentWidth * xOffset, parentWidth * xScale);
        _rt.SetInsetAndSizeFromParentEdge(Edge.Top, parentHeight * yOffset , parentHeight * yScale);
    }
}
