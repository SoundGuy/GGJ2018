using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlasma : MonoBehaviour {

    public string InputAxisName;

    public GameObject particles;

    // Use this for initialization
    void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
       if (Input.GetButton(InputAxisName))
        {
            particles.SetActive(true);
        }
        else
        {
            particles.SetActive(false);
        }

    }
}
