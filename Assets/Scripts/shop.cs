using UnityEngine;

public class shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint laserTurret;
    public TurretBlueprint iceTurret;

    BuildManager buildManager;
    public static shop Instance;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret");
        //buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectlaserTurret()
    {
        Debug.Log("laser Turret");
        //buildManager.SelectTurretToBuild(laserTurret);


    }
    public void SelectIceTurret()
    {
        Debug.Log("Ice Turret");
        //buildManager.SelectTurretToBuild(iceTurret);
    }
}
