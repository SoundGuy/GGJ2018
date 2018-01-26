using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public string[] ScenesOrder;
	private int currentScene;

	void OnEnable()
	{
		LevelController.OnLevelEnd += HandleOnLevelEnd;
	}

	void OnDisable()
	{
		LevelController.OnLevelEnd -= HandleOnLevelEnd;
	}

	void HandleOnLevelEnd(LevelController levelController)
	{
		SceneManager.LoadScene(ScenesOrder[currentScene], LoadSceneMode.Additive);
		currentScene++;
	}
}
