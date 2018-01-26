﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	public static Action<int> OnButtonClick;

	public static GUIController Instance;

	public Text[] ButtonsTexts;
	public Image[] ButtonsImages;

	void Awake()
	{
		Instance = this;
	}

	public void SetButtonText(int id, string text)
	{
		ButtonsTexts[id].gameObject.SetActive(true);
		ButtonsTexts[id].text = text;
		ButtonsImages[id].gameObject.SetActive(false);
	}

	public void SetButtonImage(int id, Sprite sprite)
	{
		ButtonsImages[id].gameObject.SetActive(true);
		ButtonsImages[id].sprite = sprite;
		ButtonsTexts[id].gameObject.SetActive(false);
	}

	public void ButtonClick(int id)
	{
		if (OnButtonClick != null)
		{
			OnButtonClick(id);
		}
	}
}