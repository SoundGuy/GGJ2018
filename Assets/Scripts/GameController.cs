using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController Instance;

	public string[] StartScenes;
	public string[] ScenesOrder;
	private int currentScene = -1;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		foreach(var sceneName in StartScenes)
		{
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		}
	}

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
		UnloadCurrentScene();
		currentScene++;
		LoadCurrentScene();
	}

	void LoadCurrentScene()
	{
		SceneManager.LoadScene(ScenesOrder[currentScene], LoadSceneMode.Additive);
	}

	void UnloadCurrentScene()
	{
		SceneManager.UnloadSceneAsync(ScenesOrder[currentScene]);
	}
}
