using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController Instance;

	public Transform Head;
	public Transform LeftHand;
	public Transform RightHand;

	void Awake()
	{
		Instance = this;
	}
}
