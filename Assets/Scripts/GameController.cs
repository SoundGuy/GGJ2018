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

	public float LevelEndWaitTime = 5f;

	private List<Scene> loadedScenes = new List<Scene>();

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		foreach(var sceneName in StartScenes)
		{
			if (!loadedScenes.Contains(SceneManager.GetSceneByName(sceneName)))
			{
				SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			}
		}
		LoadFirstLevel();
	}

	void LoadFirstLevel()
	{
		int i = 0;
		foreach (var sceneName in ScenesOrder)
		{
			if (loadedScenes.Contains(SceneManager.GetSceneByName(sceneName)))
			{
				currentScene = i;
				return;
			}
			i++;
		}
		LoadCurrentScene();
	}

	void OnEnable()
	{
		LevelController.OnLevelEnd += HandleOnLevelEnd;
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	void OnDisable()
	{
		LevelController.OnLevelEnd -= HandleOnLevelEnd;
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		loadedScenes.Add(scene);
	}

	void OnSceneUnloaded(Scene scene)
	{
		if (loadedScenes.Contains(scene))
		{
			loadedScenes.Remove(scene);
		}
	}

	void HandleOnLevelEnd(LevelController levelController)
	{
		Instance.StartCoroutine(DelayLevelEnd());
	}

	IEnumerator DelayLevelEnd()
	{
		yield return new WaitForSeconds(LevelEndWaitTime);
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
