using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    private SkillTree skillTree;

    [SerializeField] RectTransform skillTreeContent;
    [SerializeField] GameObject nodePrefab;



    void Start()
    {
        skillTree = new SkillTree();
        Node root = new Node("root", "root", 0, "root");

        GameObject rootObject = Instantiate(nodePrefab, skillTreeContent);

        NodeUI rootUI = rootObject.GetComponent<NodeUI>();
        rootUI.SetNode(root);
    }

}
