﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateHandling;
using UnityEngine.UI;
using Initialization;

public class RightMenu1 : MonoBehaviour
{

    public static RightMenu1 Instance { get { return instance; } }
    private static RightMenu1 instance;

    public GameObject Menu;
    public Animator anim;

	public GameObject Scrolllist;
    public Animator animScroll;

    public Animator animSelector;

	public ButtonListControl buttonListControl;
    public Text instructions;
    public bool isOn;
    public bool inFusion;
    public bool inGroup;
    public bool inReaction;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
        animScroll = Scrolllist.GetComponent<Animator>();
        isOn = false;
        inFusion = false;
        inGroup = false;
        inReaction = false;
    }

    public void Play()
    {
        if (!isOn)
        {
            if (ZoomInfo.Instance.infoOn) {
                ZoomInfo.Instance.CloseZoomin();
            }
            anim.Play("RightMenuSlideIn");
            isOn = true;
            Initialize.sh.setCurrentState("ActionBar", true, true);

        }
        else
        {
			//Debug.Log ("Before THAT IF THO");
			if (!inGroup && !inReaction) {
				anim.Play ("RightSideRetract");
				isOn = false;
				//Debug.Log("In if and state is: " + Initialize.sh.getCurrentState().name);
                Initialize.sh.setCurrentState ("MainGameScene", true, true);
				//Debug.Log("Main Game Scene? " + Initialize.sh.getCurrentState().name);
			}
        }
    }
    public void InteractFusion() 
    {
        if (isOn)
        {
			//instructions.text = "Pick two atoms to fuse!";
			anim.Play("RightSideRetract");
            animSelector.Play("SelectorAppear");
            isOn = false;
            inFusion = true;
            Initialize.ranch.inFusion = true;
            Initialize.sh.setCurrentState ("Fusion", true, true);
        }
    }
	public void InteractGroup()
	{
		if (isOn && !inGroup)
		{
			//instructions.text = "Pick atoms to group into a molecule!";
			inGroup = true;
            Initialize.ranch.inGrouping = true;
            buttonListControl.PopulateGroupList(true);
            animScroll.Play("ScrollListEnter");
            Debug.Log("GOT TO CHANGE PLACE");
            Initialize.sh.setCurrentState("Group", true, false);//visible, not active
		}
	}
	public void InteractReaction()
	{
		if (isOn && !inReaction)
		{
			//instructions.text = "Pick Triums to make a compound!";
			inReaction = true;
            Initialize.ranch.inReaction = true;
            buttonListControl.PopulateGroupList(false);
            animScroll.Play("ScrollListEnter");
            Initialize.sh.setCurrentState("Reaction", true, false);//visible not active
		}
	}
}
