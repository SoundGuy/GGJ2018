using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateRiddleText : MonoBehaviour {



	// Use this for initialization
	void OnEnable() {
        LevelController lvl = FindObjectOfType<LevelController>();
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.text = lvl.ButtonsTexts[lvl.EndButton];
	}
	
	
}
