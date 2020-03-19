using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nodeui : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellCost;
    public Button sellButton;

    private Node target;

    public void SetTarget(Node _target)
    {

        target = _target;

        transform.position = target.GetBuildPosition();


        if (!target.isUpgraded)
        {
            upgradeCost.text = "Upgrade\n" + "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
            sellCost.text = "Sell\n" + "$" + target.turretBlueprint.GetSellAmount();
            sellButton.interactable = true;

        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();

    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}