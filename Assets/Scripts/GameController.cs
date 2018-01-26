using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController Instance;

	public string[] StartScenes;
	public string[] ScenesOrder;
	private int currentScene = 0;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		foreach(var sceneName in StartScenes)
		{
			//if (SceneManager.GetSceneByName(sceneName) == null)
			{
				SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			}
		}
		LoadFirstLevel();
	}

	void LoadFirstLevel()
	{
		/*int i = 0;
		foreach (var sceneName in ScenesOrder)
		{
			if (SceneManager.GetSceneByName(sceneName) != null)
			{
				currentScene = i;
				return;
			}
			i++;
		}*/
		LoadCurrentScene();
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
		Debug.Log("LoadScene " + currentScene);
		SceneManager.LoadScene(ScenesOrder[currentScene], LoadSceneMode.Additive);
	}

	void UnloadCurrentScene()
	{
		Debug.Log("UnloadScene " + currentScene);
		SceneManager.UnloadSceneAsync(ScenesOrder[currentScene]);
	}
}
