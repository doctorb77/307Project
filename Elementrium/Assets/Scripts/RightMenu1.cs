﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RightMenu1 : MonoBehaviour
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
			anim.Play("RightMenuSlideIn");
			isOn = true;

		}
		else
		{
			anim.Play("RightSideRetract");
			isOn = false;
		}

	}
}
