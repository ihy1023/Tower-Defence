using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
        }
        instance = this;
    }
    public GameObject buildEffect;

    public GameObject sellEffect;

    public GameObject standardTurretPrefab;

    public GameObject lagerTurretPrefab;

    public GameObject IceTurretPrefab;

    private TurretBlueprint turretToBuild;

    public Nodeui nodeUI;

    private Node selectedNode;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return stats.money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (stats.money < turretToBuild.cost)
        {
            Debug.Log("돈이 부족합니다.");
            return;
        }

        stats.money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(),Quaternion.identity);
        node.turret=turret;

        Debug.Log("터렛을 지었습니다. 남은 돈 : " + stats.money);
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            nodeUI.Hide();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        nodeUI.Hide();
    }
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;

    }
}
