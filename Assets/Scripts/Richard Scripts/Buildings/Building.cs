﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour
{

    public Material debugMaterial;
    public static Material defaultMaterial;
    public Renderer rend;
    public Upgrade[] upgrades;

    [Header("Building Attributes")]
    // How far should this building be moved up to land on the ground.
    public float verticalOffset;

    public string buildingType;
    public int cost;
    public int woodCost;

    private Animator animator;
    public int populationRequired;
    public int emission;
    public int size;
    public ParticleSystem landingParticleSystem;
    public ParticleSystem popParticleSystem;
    public ParticleSystem moneyParticleSystem;
    public ParticleSystem woodParticleSystem;
    public GameObject upgradeSystems;

    private AudioSource audioSource;

    public AudioClip landingThud;
    public AudioClip coinSpluge;

    //Category Specific Increases
    public int populationIncrease;
    public int woodIncomeIncrease;
    public int incomeIncrease;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        upgrades = GetComponentsInChildren<Upgrade>();
    }

    public void Start()
    {
        defaultMaterial = rend.material;
    }

    public virtual void Emit()
    {

    }

    public void PlayLandingSounds()
    {
        audioSource.PlayOneShot(coinSpluge);
        audioSource.PlayOneShot(landingThud);
    }

    /// <summary>
    /// Debug function that sets the mesh color to magenta.
    /// </summary>
    public void FocusOnBuilding()
    {
        //animator.SetBool("Inspecting", true);
    }

    public static void UnfocusAllBuildings()
    {
        Building[] b = GameObject.FindObjectsOfType<Building>();
        foreach (Building obj in b)
        {
            //obj.animator.SetBool("Inspecting", false);
        }
    }

    public void ActivateUpgrade(int index)
    {
        if (ResourceKeeper.money < upgrades[index].cost)
        {
            GameObject.FindGameObjectWithTag("CreditNotif").GetComponent<Animator>().SetTrigger("Notify");
            GameObject.FindGameObjectWithTag("CreditPanel").GetComponentInChildren<Image>().color = Color.red;
            GameObject.FindGameObjectWithTag("CreditNotifTitle").GetComponent<TextMeshProUGUI>().text = "NOT ENOUGH MONEY";
        }
        if (ResourceKeeper.wood < upgrades[index].woodCost)
        {
            GameObject.FindGameObjectWithTag("WoodNotif").GetComponent<Animator>().SetTrigger("Notify");
            GameObject.FindGameObjectWithTag("WoodPanel").GetComponentInChildren<Image>().color = Color.red;
            GameObject.FindGameObjectWithTag("WoodNotifTitle").GetComponent<TextMeshProUGUI>().text = "NOT ENOUGH WOOD";
        }
        if (ResourceKeeper.upgradeMaterial < upgrades[index].upgradeMaterialCost)
        {
            GameObject.FindGameObjectWithTag("SteelNotif").GetComponent<Animator>().SetTrigger("Notify");
            GameObject.FindGameObjectWithTag("SteelPanel").GetComponentInChildren<Image>().color = Color.red;
            GameObject.FindGameObjectWithTag("SteelNotifTitle").GetComponent<TextMeshProUGUI>().text = "NOT ENOUGH STEEL";
        }
        else if (ResourceKeeper.money >= upgrades[index].cost && !upgrades[index].upgradeActive && ResourceKeeper.upgradeMaterial >= upgrades[index].upgradeMaterialCost && ResourceKeeper.wood >= upgrades[index].woodCost)
        {
            upgrades[index].Activate();
            ResourceKeeper.money -= upgrades[index].cost;
            ResourceKeeper.wood -= upgrades[index].woodCost;
            ResourceKeeper.upgradeMaterial -= upgrades[index].upgradeMaterialCost;
            ResourceKeeper.emission -= upgrades[index].emissionReduction;
            ResourceKeeper.income += upgrades[index].incomeIncrease;
            ResourceKeeper.woodIncome += upgrades[index].woodIncomeIncrease;
            ResourceKeeper.population += upgrades[index].populationIncrease;

            EmitResources(upgrades[index].populationIncrease, upgrades[index].incomeIncrease, upgrades[index].woodIncomeIncrease);
            ParticleSystem[] systems = upgradeSystems.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem p in systems)
            {
                p.Play();
            }
        }  
    }

    public void ShakeOnLand()
    {
        CameraShaker.Instance.ShakeOnce(4f, 2f, 0.0f, 0.5f);
        landingParticleSystem.Play();
        EmitResources(populationIncrease, incomeIncrease, woodIncomeIncrease);
        PlayLandingSounds();
    }

    public void EmitResources(int pop, int credits, int wood)
    {
        popParticleSystem.Emit(pop);
        woodParticleSystem.Emit(wood);
        moneyParticleSystem.Emit(credits);
    }
}
