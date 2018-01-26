using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	public static Action<int> OnButtonClick;

	public static GUIController Instance;

	public Button[] Buttons;
	public Text[] ButtonsTexts;
	public Image[] ButtonsImages;
    public Text topText;

	void Awake()
	{
		Instance = this;
	}

    public void SetTopText(string inText)
    {
        topText.text = inText;
    }
	public void SetButtonEnable(int id, bool enabled)
	{
		Buttons[id].gameObject.SetActive(enabled);
		if (!enabled)
		{
			ButtonsTexts[id].gameObject.SetActive(false);
			ButtonsImages[id].gameObject.SetActive(false);
		}
		if (id == LevelController.NumOfButtons-1)
		{
			var btn0 = Buttons[0].GetComponent<RectTransform>();
			var btn3 = Buttons[3].GetComponent<RectTransform>();
			if (enabled)
			{
				btn0.anchoredPosition = new Vector2(-btn3.anchoredPosition.x, btn3.anchoredPosition.y);
			}
			else
			{
				btn0.anchoredPosition = new Vector2(0, btn3.anchoredPosition.y);
			}
		}
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