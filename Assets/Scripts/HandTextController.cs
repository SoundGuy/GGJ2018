using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               
public class HandTextController : MonoBehaviour {
    public Image img;
    
	// Use this for initialization
	void Update () {
        if (img == null)
        {
            var obj = GameObject.Find("Hand Image");
            if (obj != null)
            {
                Debug.Log("Found Hand Text");
                img = obj.GetComponent<Image>();
            }
        }
    }

    private void OnEnable()
    {
        LevelController.OnCurrentActionChanged += ActionChanged;
    }

    private void OnDisable()
    {
        LevelController.OnCurrentActionChanged -= ActionChanged;

    }


    private void ActionChanged(int actionID,bool levelComplete)
    {
        if (img != null)
            img.fillAmount = actionID / 4.0f;
    }
}
