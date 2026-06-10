using UnityEngine;
using static UnityEngine.RectTransform;

public class ButtonScaler : MonoBehaviour {

    [SerializeField] private Canvas canvas;
    [SerializeField] private float offset = 40;

    private RectTransform _rt;
    private RectTransform _rtParent;

    private void Start() {
        _rt = GetComponent<RectTransform>();
        _rtParent = _rt.parent as RectTransform;
    }

    private void Update() {
        float parentWidth  = _rtParent.rect.width;
        float parentHeight = _rtParent.rect.height;

        _rt.SetInsetAndSizeFromParentEdge(Edge.Left, offset, parentWidth * 0.4f);
        _rt.SetInsetAndSizeFromParentEdge(Edge.Top, offset , parentHeight * 0.4f);
    }
}
