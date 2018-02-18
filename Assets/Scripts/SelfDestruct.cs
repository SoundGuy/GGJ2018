using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float SelfDestctuctTimer =0;
    public float SelfDestctuctFuseTime;
    public bool destroyParent = false;
    public bool autoSelfDestruct = false;
    public bool disableInsteadOfDistruct = false;
    public TouchObject touchObejct;

    void StartTimer()
    {
        SelfDestctuctTimer = Time.time;
    }
    void SelfDestructMe()
    {
        Destruct(gameObject);
    }


    void SelfDestructParent()
    {
        Destruct(gameObject.transform.parent.gameObject);
    }

    void Destruct(GameObject go)
    {
        if (disableInsteadOfDistruct)
        {
            go.SetActive(false);
            NotifyTouchObject();
        } else
        {
            Destroy(go);
        }
    }

    private void NotifyTouchObject()
    {
        touchObejct.NotifyAboutSelfDestruct();
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
		if (touchObejct == null)
        {
            TouchObject parent = gameObject.GetComponentInParent<TouchObject>();
            if (parent != null)
            {
                touchObejct = parent;
            }
        }
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
