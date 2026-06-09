using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    private SkillTree skillTree;

    [SerializeField] RectTransform skillTreeContent;
    [SerializeField] RectTransform linesContainer;
    [SerializeField] RectTransform nodesContainer;
    [SerializeField] GameObject nodePrefab;
    [SerializeField] GameObject linePrefab;

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
        GameObject nodeObject = Instantiate(nodePrefab, nodesContainer);
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
        SpawnLine(Vector2.zero, new Vector2(0, -150));

        // relic branch
        Node relic1 = skillTree.CreateNodeRelic();
        SpawnNode(relic1, new Vector2(-150, 100));
        SpawnLine(Vector2.zero, new Vector2(-150, 100));

        // Stats branch
        Node stats1 = skillTree.CreateNodeStat();
        SpawnNode(stats1, new Vector2(150, 100));
        SpawnLine(Vector2.zero, new Vector2(150, 100));
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
        Node child2 = CreateNodeFromBranch(node.branch);

        SpawnNode(child1, leftChildPosition);
        SpawnLine(position, leftChildPosition);

        SpawnNode(child2, rightChildPosition);
        SpawnLine(position, rightChildPosition);
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

    void SpawnLine(Vector2 startPosition, Vector2 endPosition)
    {
        GameObject lineObject = Instantiate(linePrefab, linesContainer);

        RectTransform lineRect = lineObject.GetComponent<RectTransform>();

        Vector2 direction = endPosition - startPosition;
        float distance = direction.magnitude;

        lineRect.anchoredPosition = startPosition + direction / 2f;

        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.rotation = Quaternion.Euler(0, 0, angle);
    }

}
