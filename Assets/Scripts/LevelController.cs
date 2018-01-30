using System;
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

    public bool ResetCountOnWrongActionOrder;
    public bool ResetOnFailedGoal;

    public static Action<int, bool> OnCurrentActionChanged;

	void OnEnable()
	{
		Goal.Complete += OnGoalComplete;
        Goal.Failed += OnGoalFailed;
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
		Goal.Complete -= OnGoalComplete;
        Goal.Failed -= OnGoalFailed;
    }

    void OnGoalFailed(Goal FailedGoal)
    {
        if(ResetOnFailedGoal)
        {
            currentOrder = 0;
            Debug.Log(string.Format("New Action {0},{1}", currentOrder, currentOrder == ActionsOrder.Length));
            if (OnCurrentActionChanged != null)
                OnCurrentActionChanged(currentOrder, false);
        }
    }

	void OnGoalComplete(Goal completedGoal)
	{
		if (currentOrder >= ActionsOrder.Length)
		{
			FinishLevel();
		}
        else if(ActionsOrder[currentOrder] != completedGoal && ResetCountOnWrongActionOrder)
        {
            currentOrder = 0;
            Debug.Log(string.Format("New Action {0},{1}", currentOrder, currentOrder == ActionsOrder.Length));
            if(OnCurrentActionChanged!= null)
                OnCurrentActionChanged(currentOrder, false);
        }

		else if (currentOrder< ActionsOrder.Length && ActionsOrder[currentOrder] == completedGoal)
		{
            currentOrder++;
            if (OnCurrentActionChanged != null)
                OnCurrentActionChanged(currentOrder, currentOrder == ActionsOrder.Length);
            Debug.Log(string.Format("New Action {0},{1}", currentOrder, currentOrder == ActionsOrder.Length));
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
