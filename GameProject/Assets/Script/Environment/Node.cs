using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if(CameraController.On_Off)
        {
            if (turret != null)
            {
                return;
            }

            GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        }
    }

    private void OnMouseEnter()
    {
        if (CameraController.On_Off)
        {
            rend.material.color = hoverColor;
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
