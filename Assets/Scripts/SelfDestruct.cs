using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float SelfDestctuctTimer =0;
    public float SelfDestctuctFuseTime;
    public bool destroyParent = false;
    public bool autoSelfDestruct = false;

    void StartTimer()
    {
        SelfDestctuctTimer = Time.time;
    }
    void SelfDestructMe()
    {
        Destroy(gameObject);
    }


    void SelfDestructParent()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    void OnEnable()
    {
        if (autoSelfDestruct)
        {
            StartTimer();
        }
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SelfDestctuctTimer != 0 &&
            Time.time > SelfDestctuctTimer + SelfDestctuctFuseTime)
        {
            if (destroyParent)
            {
                SelfDestructParent();
            } else
            {
                SelfDestructMe();
            }
        }
	}
}
