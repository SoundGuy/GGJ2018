using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour {

    public string TextToChangeTo;
    // Use this for initialization
    public void UpdateTheText()
    {
    
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.text = TextToChangeTo;
    }


    public void UpdateTheText(string inText)
    {
        TextToChangeTo = inText;
        UpdateTheText();
    }
}
