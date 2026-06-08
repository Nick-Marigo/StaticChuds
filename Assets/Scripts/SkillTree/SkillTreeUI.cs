using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    private SkillTree skillTree;

    [SerializeField] RectTransform skillTreeContent;
    [SerializeField] GameObject nodePrefab;

    void Start()
    {
        skillTree = new SkillTree();

        // Create root node
        Node root = skillTree.CreateRoot();
        SpawnNode(root, new Vector2(0, 0));

        // Create first branch nodes
        BuildBranches();
    }

    void SpawnNode(Node node, Vector2 position)
    {
        GameObject nodeObject = Instantiate(nodePrefab, skillTreeContent);
        RectTransform nodeObjectRect = nodeObject.GetComponent<RectTransform>();
        nodeObjectRect.anchoredPosition = position;
        NodeUI nodeUI = nodeObject.GetComponent<NodeUI>();
        nodeUI.SetNode(node, this);
    }

    void BuildBranches()
    {
        // Spell/Mod branch
        Node spell1 = skillTree.CreateNodeSpell();
        SpawnNode(spell1, new Vector2(0, -150));

        // relic branch
        Node relic1 = skillTree.CreateNodeRelic();
        SpawnNode(relic1, new Vector2(-150, 100));

        // Stats branch
        Node stats1 = skillTree.CreateNodeStat();
        SpawnNode(stats1, new Vector2(150, 100));
    }

    public void NodeClicked(Node node, Vector2 position)
    {
        SpawnTwoChildern(node, position);
    }

    void SpawnTwoChildern(Node node, Vector2 position)
    {
        
    }

    Node CreateNodeFromBranch(string branch)
    {
        switch (branch)
        {
            case "Spells":
                return skillTree.CreateNodeSpell();
            case "Relics":
                return skillTree.CreateNodeRelic();
            case "Stats":
                return skillTree.CreateNodeStat();
            default:
                Debug.Log("YA BOI FAILED");
                return null;
        }
    }

}
