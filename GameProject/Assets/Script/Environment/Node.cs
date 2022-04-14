using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    public Color notEnoughMoneyColor;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
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
            if (!buildManager.CanBuild)
            {
                return;
            }
            if (turret != null)
            {
                return;
            }

            buildManager.BuildTurretOn(this);
        }
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
