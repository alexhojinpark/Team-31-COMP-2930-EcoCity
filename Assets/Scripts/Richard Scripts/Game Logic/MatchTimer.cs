﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchTimer : MonoBehaviour
{
    [Header("Gameplay Attributes")]
    public float levelTimeInSeconds;
    public int startYear;
    public int endYear;
    public bool matchStarted;


    [Header("Accessible Data")]
    public Month currentMonth;
    public int currentYear;

    private int levelTimeInMonths;
    private float timePerMonth;
    private float timePerYear;
    private float monthTimer;
    private float yearTimer;

    [Header("Player Resources")]
    private ResourceKeeper resourceKeeper;

    public enum Month {JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC};

    private void Awake()
    {
        resourceKeeper = GameObject.FindGameObjectWithTag("ResourceKeeper").GetComponent<ResourceKeeper>();
    }

    void Start()
    {
        timePerYear = levelTimeInSeconds / (endYear - startYear);
        timePerMonth = timePerYear / 12;
        currentYear = startYear;
        levelTimeInMonths = 0;

        matchStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        AdvanceTime();
    }

    private void AdvanceTime()
    {
        if (matchStarted)
        {
            levelTimeInSeconds -= Time.deltaTime;
            monthTimer += Time.deltaTime;
            yearTimer += Time.deltaTime;
            
            if (resourceKeeper.population >= 250)
            {
                SceneManager.LoadScene("win_screen");
            }
        }

        if (monthTimer > timePerMonth)
        {
            levelTimeInMonths++;
            // Increase money, wood, metal, electricity every month

            resourceKeeper.ApplyMonthlyNumbers();

            if (levelTimeInMonths > 11)
            {
                currentYear++;
                levelTimeInMonths = 0;
                currentMonth = (Month)levelTimeInMonths;
                monthTimer = 0;
            }
            else
            {
                currentMonth = (Month)levelTimeInMonths;
                monthTimer = 0;
            }


        }
    }

    public void StartMatch()
    {
        matchStarted = true;
    }

    public void StopTime()
    {
        matchStarted = false;
    }
}
