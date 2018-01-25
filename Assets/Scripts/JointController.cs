using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class JointController : MonoBehaviour {

	public XRNode Joint;
		
	// Update is called once per frame
	void Update () {
		transform.localPosition = InputTracking.GetLocalPosition(Joint);
		transform.localRotation = InputTracking.GetLocalRotation(Joint);
	}
}
