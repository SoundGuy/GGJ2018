﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class LevelController : MonoBehaviour {
	public const int NumOfButtons = 4;

	public static Action<LevelController> OnLevelEnd;
	public static Action OnGeneratedRiddle;

	public bool IsRiddle = false;

	public Goal[] ActionsOrder;
	private int currentOrder = 0;

	public int EndButton = 0;
	public bool[] EnabledButtons = new bool[NumOfButtons];
	public string[] ButtonsTexts = new string[NumOfButtons];
	public Sprite[] ButtonsImages = new Sprite[NumOfButtons];


    public string LevelString;
  
	void OnEnable()
	{
		Goal.Complete += OnTouchedOcject;
		if (IsRiddle)
		{
			GenerateWordRiddles();
		}
		else
		{
			SetTeleportButton();
		}

        GUIController.Instance.SetTopText(LevelString);


    }

	void OnDisable()
	{
		Goal.Complete -= OnTouchedOcject;
	}

	void OnTouchedOcject(Goal touchObject)
	{
		if (currentOrder< ActionsOrder.Length && ActionsOrder[currentOrder] == touchObject)
		{
            currentOrder++;
			if (currentOrder == ActionsOrder.Length)
			{
				FinishLevel();
			}
		}
	}

	void FinishLevel()
	{
		if (OnLevelEnd != null)
		{
			OnLevelEnd(this);
		}
	}

	public void GenerateWordRiddles()
	{
		ButtonsTexts = NameGenerator.Instance.GetSimilarWords(NumOfButtons).ToArray();
		EndButton = UnityEngine.Random.Range(0, NumOfButtons);
		for (int i=0; i<NumOfButtons; i++)
		{
			EnabledButtons[i] = true;
			ButtonsImages[i] = null;
		}
		if (OnGeneratedRiddle != null)
		{
			OnGeneratedRiddle();
		}
	}

	public void SetTeleportButton()
	{
		EndButton = 0;
		ButtonsTexts[0] = "Teleport";
		EnabledButtons[0] = true;
		for (int i=1; i<NumOfButtons; i++)
		{
			EnabledButtons[i] = false;
			ButtonsTexts[i] = string.Empty;
			ButtonsImages[i] = null;
		} 
	}
}
