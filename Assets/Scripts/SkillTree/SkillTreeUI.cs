using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    private SkillTree skillTree;

    [SerializeField] RectTransform skillTreeContent;
    [SerializeField] RectTransform linesContainer;
    [SerializeField] RectTransform nodesContainer;
    [SerializeField] GameObject nodePrefab;
    [SerializeField] GameObject linePrefab;
    [SerializeField] GameObject owner;

    void Start()
    {
        skillTree = new SkillTree(owner.GetComponent<PlayerInstance>());

        // Create root node
        Node root = skillTree.CreateRoot();
        root.isPurchased = true;
        NodeUI rootUI = SpawnNode(root, new Vector2(0, 0));
        rootUI.SetRootNull();
        rootUI.SetPurchasedColor();

        // Create first branch nodes
        BuildBranches();
    }

    NodeUI SpawnNode(Node node, Vector2 position)
    {
        GameObject nodeObject = Instantiate(nodePrefab, nodesContainer);
        RectTransform nodeObjectRect = nodeObject.GetComponent<RectTransform>();
        nodeObjectRect.anchoredPosition = position;

        NodeUI nodeUI = nodeObject.GetComponent<NodeUI>();
        nodeUI.SetNode(node, this);

        return nodeUI;
    }

    NodeUI SpawnConntectNode(Node node, Vector2 parentPosition, Vector2 childPosition)
    {
        NodeUI nodeUI = SpawnNode(node, childPosition);
        LineUI lineUI = SpawnLine(parentPosition, childPosition);

        nodeUI.SetIncomingLine(lineUI);

        return nodeUI;
    }

    void BuildBranches()
    {
        Vector2 rootPosition = Vector2.zero;

        // Spell/Mod branch
        Vector2 spellPosition = new Vector2(0, -150);
        Node spell1 = skillTree.CreateNodeSpell();
        SpawnConntectNode(spell1, rootPosition, spellPosition);

        // relic branch
        Vector2 relicPosition = new Vector2(-150, 100);
        Node relic1 = skillTree.CreateNodeRelic();
        SpawnConntectNode(relic1, rootPosition, relicPosition);

        // Stats branch
        Vector2 statsPosition = new Vector2(150, 100);
        Node stats1 = skillTree.CreateNodeStat();
        SpawnConntectNode(stats1, rootPosition, statsPosition);
    }

    public void NodeClicked(Node node, Vector2 position)
    {
        if (skillTree.canPurchased()) SpawnTwoChildren(node, position);
    }

    void SpawnTwoChildren(Node node, Vector2 position)
    {
        Vector2 direction = position.normalized;

        Vector2 sideways = new Vector2(-direction.y, direction.x);

        float forwardDistance = 200f;
        float sideDistance = 120f;

        Vector2 leftChildPosition = position + direction * forwardDistance - sideways * sideDistance;
        Vector2 rightChildPosition = position + direction * forwardDistance + sideways * sideDistance;

        Node child1 = CreateNodeFromBranch(node.branch);
        if (child1 == null) return;
        SpawnConntectNode(child1, position, leftChildPosition);

        Node child2 = CreateNodeFromBranch(node.branch);
        if (child2 == null) return;
        SpawnConntectNode(child2, position, rightChildPosition);
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

    LineUI SpawnLine(Vector2 startPosition, Vector2 endPosition)
    {
        GameObject lineObject = Instantiate(linePrefab, linesContainer);

        LineUI lineUI = lineObject.GetComponent<LineUI>();
        lineUI.SetupLine(startPosition, endPosition);

        return lineUI;
    }

}
