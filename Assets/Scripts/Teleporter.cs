using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	public static Teleporter Instance;

	public GameObject TeleportObject;

	void Awake ()
	{
		Instance = this;
	}

	public static void StartTeleporter()
	{
		if (Instance == null)
		{
			return;
		}
		var headPosition = PlayerController.Instance.Head.position;
		Instance.TeleportObject.transform.localPosition = new Vector3(headPosition.x, 0, headPosition.z);
		Instance.TeleportObject.SetActive(true);
	}

	public static void StopTeleporter()
	{
		if (Instance == null)
		{
			return;
		}
		Instance.TeleportObject.SetActive(false);
	}
}
