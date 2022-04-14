using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public TurretBlueprint FirstTurret;
    public TurretBlueprint SecondTurret;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectFirstTurret()
    {
        buildManager.SelectTurretToBuild(FirstTurret);
    }

    public void SelectSecondTurret()
    {
        buildManager.SelectTurretToBuild(SecondTurret);
    }
}
