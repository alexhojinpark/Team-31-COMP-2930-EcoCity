﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public Button[] upgradeOptions;
    private Building selectedBuilding;
    public InspectMenu inspectMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            
        }
    }

    public void PopulateList(Upgrade[] upgrades)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
           upgradeOptions[i].GetComponentInChildren<Text>().text = upgrades[i].upgradeName + " " + upgrades[i].cost;
        }
    }

    public void SetSelectedBuilding(Building b)
    {
        selectedBuilding = b;
    }

    public void ClearSelected()
    {
        selectedBuilding = null;
    }

    public void PassUpgrade(int index)
    {
        selectedBuilding.ActivateUpgrade(index);
    }

    public void UpdateInspectMenu(int i)
    {
        inspectMenu.SetMoneyCost(selectedBuilding.upgrades[i].cost.ToString());
    }
}
