using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameController : MonoBehaviour {

	public static GameController Instance;

	public string[] StartScenes;
	public string[] ScenesOrder;
	private int currentScene = 0;

	public float LevelEndWaitTime = 1f;

	private List<Scene> loadedScenes = new List<Scene>();

	private int correctButton = 0;

	void Awake()
	{
		Instance = this;
		XRSettings.showDeviceView = false;
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
		GUIController.OnButtonClick += OnGUIButtonClick;
	}

	void OnDisable()
	{
		LevelController.OnLevelEnd -= HandleOnLevelEnd;
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
		GUIController.OnButtonClick += OnGUIButtonClick;
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
		UpdateButtonsForLevel(levelController);
	}

	void UpdateButtonsForLevel(LevelController levelController)
	{
		correctButton = levelController.EndButton;
		for (int i=0; i<levelController.EnabledButtons.Length; i++)
		{
			GUIController.Instance.SetButtonEnable(i, levelController.EnabledButtons[i]);
			if (levelController.EnabledButtons[i])
			{
				if (levelController.ButtonsImages[i] == null)
				{
					GUIController.Instance.SetButtonText(i, levelController.ButtonsTexts[i]);
				}
				else
				{
					GUIController.Instance.SetButtonImage(i, levelController.ButtonsImages[i]);
				}
			}
		}
	}

	void OnGUIButtonClick(int id)
	{
		if (correctButton == id)
		{
			if (GUIController.Instance != null)
			{
				for (int i=0; i<LevelController.NumOfButtons; i++)
				{
					GUIController.Instance.SetButtonEnable(i, false);
				}
			}
			Teleporter.StartTeleporter();
			Instance.StartCoroutine(DelayLevelEnd());
		}
		else
		{
			LevelController lvl = FindObjectOfType<LevelController>();
			lvl.GenerateWordRiddles();
			UpdateButtonsForLevel(lvl);
		}
	}

	IEnumerator DelayLevelEnd()
	{
		yield return new WaitForSeconds(LevelEndWaitTime);
		NextLevel();
	}

	void NextLevel()
	{
		UnloadCurrentScene();
		currentScene++;
		LoadCurrentScene();
	}

	void LoadCurrentScene()
	{
		Debug.Log("LoadScene " + currentScene);
		SceneManager.LoadScene(ScenesOrder[currentScene], LoadSceneMode.Additive);
		if (GUIController.Instance != null)
		{
			for (int i=0; i<LevelController.NumOfButtons; i++)
			{
				GUIController.Instance.SetButtonEnable(i, false);
			}
		}
	}

	void UnloadCurrentScene()
	{
		Debug.Log("UnloadScene " + currentScene);
		SceneManager.UnloadSceneAsync(ScenesOrder[currentScene]);
	}
}
