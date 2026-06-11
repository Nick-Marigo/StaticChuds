using System;
using UnityEngine;
using Debug = UnityEngine.Debug;
using TMPro;

public class SkillTreeUI : MonoBehaviour
{
    public SkillTree skillTree { get; private set; }

    [SerializeField] RectTransform skillTreeContent;
    [SerializeField] RectTransform linesContainer;
    [SerializeField] RectTransform nodesContainer;
    [SerializeField] GameObject nodePrefab;
    [SerializeField] GameObject linePrefab;
    [SerializeField] public GameObject owner;
    [SerializeField] TextMeshProUGUI skillText;

    void Awake()
    {
        skillTree = new SkillTree(owner.GetComponent<PlayerInstance>());

        // Create root node
        Node root = skillTree.CreateRoot();
        root.isPurchased = true;
        NodeUI rootUI = SpawnNode(root, new Vector2(0, 0));
        rootUI.SetRootNull();
        rootUI.SetPurchasedColor();
        UpdateSkillPoints(0);

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
        spell1.depth = 1;

        // relic branch
        Vector2 relicPosition = new Vector2(-150, 100);
        Node relic1 = skillTree.CreateNodeRelic();
        SpawnConntectNode(relic1, rootPosition, relicPosition);
        relic1.depth = 1;

        // Stats branch
        Vector2 statsPosition = new Vector2(150, 100);
        Node stats1 = skillTree.CreateNodeStat();
        SpawnConntectNode(stats1, rootPosition, statsPosition);
        stats1.depth = 1;
    }

    public void UpdateSkillPoints(int n) {
        skillTree.skillPoints += n;
        skillText.text = $"Skill Points: {skillTree.skillPoints}";
    }

    public void NodeClicked(Node node, Vector2 position)
    {
        if (!skillTree.CanPurchase()) return;
        SpawnTwoChildren(node, position);
    }

    void SpawnTwoChildren(Node node, Vector2 position)
    {
        Vector2 direction = position.normalized;

        Vector2 sideways = new Vector2(-direction.y, direction.x);

        float spread = 1 - ((node.depth - 1) / 6f);
        float angle = (float)(Math.PI * 0.25) * spread * spread;

        float forwardDistance = 250f * Mathf.Cos(angle);
        float sideDistance = 250f * Mathf.Sin(angle);
        // Debug.Log($"spread: {spread}, angle: {angle}, sideDistance: {sideDistance}");

        Vector2 leftChildPosition = position + direction * forwardDistance - sideways * sideDistance;
        Vector2 rightChildPosition = position + direction * forwardDistance + sideways * sideDistance;

        if (ShouldCutBranchDepth(node.branch, node.depth)) return;

        Node child1 = CreateNodeFromBranch(node.branch);
        if (child1 == null) return;
        SpawnConntectNode(child1, position, leftChildPosition);
        child1.depth = node.depth + 1;

        Node child2 = CreateNodeFromBranch(node.branch);
        if (child2 == null) return;
        SpawnConntectNode(child2, position, rightChildPosition);
        child2.depth = node.depth + 1;
    }

    private bool ShouldCutBranchDepth(string branch, int depth) {
        switch (branch) {
            case "Spells":
                return depth > 3;
            case "Stats":
                return depth > 2;
            case "Relics":
                return depth > 3;
            default:
                return true;
        };
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
