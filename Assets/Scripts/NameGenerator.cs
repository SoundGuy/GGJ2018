using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour {

    public List<string> prefixes;
    public List<string> syllables;
    public List<string> suffixes;
    public int percengateOfThreesyllableWord = 2;

    List<string> getSimilarWords(int num)
    {


        int pref = UnityEngine.Random.Range(0, prefixes.Count);
        int suff = UnityEngine.Random.Range(0, suffixes.Count);
        List<string> retVal = new List<string>();

        for (int i=0;i<num;i++) {
            int newPrefixNum = (i%2==0)? pref : ((pref + i) % prefixes.Count);
            int newSuufiexNum = (i % 2 != 0) ? suff : ((suff + i) % suffixes.Count);
            string prefix  = prefixes[newPrefixNum];
            string suffix = suffixes[newSuufiexNum];
            string name = string.Format("{0}{1}", prefix, suffix);
        retVal.Add(name);
        }

        return retVal;
    }
    string getWord(int midLength)
    {
        string midPart = "";
        for (int i=0;i<midLength;i++ )
        {
            midPart = string.Format("{0}{1}", midPart, GetRandomItem(syllables));
        }
        return string.Format("{0}{1}{2}", GetRandomItem(prefixes), midPart, GetRandomItem(suffixes));
    }

    private object GetRandomItem(List<string> inList)
    {
        int i = UnityEngine.Random.Range(0, inList.Count);
        return (inList[i]);
    }


    string getRandomWord() {
        if (UnityEngine.Random.Range(0,10) <2)
        {
            return getWord(1);
        }
        return getWord(0);      
}
    // Use this for initialization
    void Start () {
        /*
        for (int i = 0; i < 50; i++)
        {
            List<string> Namestr = getSimilarWords(4);
            Debug.Log(string.Format("{0} {1} {2} {3}", Namestr[0], Namestr[1], Namestr[2], Namestr[3]));
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
