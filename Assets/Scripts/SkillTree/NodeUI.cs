using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NodeUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nameText;

    private Node node;

    public void SetNode(Node node)
    {
        this.node = node;

        nameText.text = node.name;
    }

}
