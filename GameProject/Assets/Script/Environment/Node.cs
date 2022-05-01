using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    public Color notEnoughMoneyColor;

    public int count = 0;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        if(!CameraController.On_Off)
        {
            if(rend.material.color == hoverColor)
            {
                rend.material.color = startColor;
            }
            else if(rend.material.color == notEnoughMoneyColor)
            {
                rend.material.color = startColor;
            }        
        }
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if(CameraController.On_Off)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (turret != null)
            {
                buildManager.SelectNode(this);
                return;
            }

            if (!buildManager.CanBuild)
            {
                return;
            }

            BuildTurret(buildManager.GetTurretToBuild());
        }
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            return;
        }

        turretBlueprint = blueprint;

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //get rid of the old turret
        if(turret.transform.GetChild(0).gameObject.GetComponent<Turret>())
        {
            turret.transform.GetChild(0).gameObject.GetComponent<Turret>().Sell_Upgrade();
        }
        else if(turret.transform.GetChild(0).gameObject.GetComponent<DolDolTurret>())
        {
            turret.transform.GetChild(0).gameObject.GetComponent<DolDolTurret>().Sell_Upgrade();
        }
        Destroy(turret,2);

        //build a new one
        if(count == 0)
        {
            count++;
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;
        }
        else if(count == 1)
        {
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.upupgradedPrefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;
            isUpgraded = true;
        }

    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        count = 0;
        //spawn a cool effect
        if (turret.transform.GetChild(0).gameObject.GetComponent<Turret>())
        {
            turret.transform.GetChild(0).gameObject.GetComponent<Turret>().Sell_Upgrade();
        }
        else if (turret.transform.GetChild(0).gameObject.GetComponent<DolDolTurret>())
        {
            turret.transform.GetChild(0).gameObject.GetComponent<DolDolTurret>().Sell_Upgrade();
        }
        Destroy(turret,2);
        isUpgraded = false;
        turretBlueprint = null;
    }

    private void OnMouseEnter()
    {
        if (CameraController.On_Off)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (!buildManager.CanBuild)
            {
                return;
            }
            if(buildManager.HasMoney)
            {
                rend.material.color = hoverColor;
            }
            else
            {
                rend.material.color = notEnoughMoneyColor;
            }
        }
    }

    private void OnMouseExit()
    {
        if (CameraController.On_Off)
        {
            rend.material.color = startColor;
        }
    }
}
