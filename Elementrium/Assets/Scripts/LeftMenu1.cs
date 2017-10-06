﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StateHandling;

public class LeftMenu1 : MonoBehaviour
{

    public GameObject Menu;
    public Animator anim;
    public bool isOn;

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
        isOn = false;
    }

    public void Play()
    {
        if (!isOn)
        {
            anim.Play("LeftMenuDropDown");
            isOn = true;

        }
        else
        {
            anim.Play("LeftMenuRetract");
            isOn = false;
        }

    }
    public void ToGlossary()
    {
        SceneManager.LoadScene("Glossary");
        StateHandler.setCurrentState("Glossary", true, true);
    }
    public void ToAchievements()
    {
        SceneManager.LoadScene("Achievements");
        StateHandler.setCurrentState("Achievements", true, true);
    }
}