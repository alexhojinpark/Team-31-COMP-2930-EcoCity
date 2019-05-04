﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectMenu : MonoBehaviour
{
    public Text nameText;
    public Text description;
    public Text moneyCost;
    public Text woodCost;
    public Text popCost;
    public Text stat1;
    public Text stat2;
    public Text stat3;

    public Image varImg;

    private Sprite gold;
    private Sprite wood;
    private Sprite pop;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        gold = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Art/2D/UI/Resource Vector Graphics/UI_Graphic_Resource_Coins.png", typeof(Sprite));
        wood = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Art/2D/UI/Resource Vector Graphics/UI_Graphic_Resource_Wood.png", typeof(Sprite));
        pop = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Art/2D/UI/Resource Vector Graphics/UI_Graphic_Resource_Food.png", typeof(Sprite));


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNameText(string s)
    {
        nameText.text = s;
    }

    public void SetDescriptionText(string s)
    {
        description.text = s;
    }

    public void SetInspecting(bool b)
    {
        animator.SetBool("Inspecting", b);
    }

    public void SetStat1(string s)
    {
        stat1.text = s;
    }

    public void SetStat2(string s)
    {
        stat2.text = s;
    }

    public void SetStat3(string s)
    {
        stat3.text = s;
    }

    public void SetMoneyCost(string s)
    {
        moneyCost.text = s;
    }

    public void SetWoodCost(string s)
    {
        woodCost.text = s;
    }

    public void SetPopCost(string s)
    {
        popCost.text = s;
    }

    public void ReceiveBuilding(GameObject g)
    {
        Building b = g.GetComponent<Building>();

        SetMoneyCost(b.cost.ToString());
        SetPopCost(b.populationRequired.ToString());
        SetWoodCost(b.woodCost.ToString());

        SetStat1("+" + b.emission.ToString() + " EMISSION");

        if (b.woodIncomeIncrease != 0)
        {
            SetStat2("+" + b.woodIncomeIncrease.ToString() + " WOOD INCOME");
            varImg.GetComponent<Image>().sprite = wood;

        }
        if (b.incomeIncrease != 0)
        {
            SetStat2("+" + b.incomeIncrease.ToString() + " INCOME");
            varImg.GetComponent<Image>().sprite = gold;
        }
        if (b.populationIncrease != 0)
        {
            SetStat2("+" + b.populationIncrease.ToString() + " POPULATION");
            varImg.GetComponent<Image>().sprite = pop;
        }
        SetStat3("");
    }
}