using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DLChat : MonoBehaviour {
    public Text text;
    public Text mytext;
    public float time = 1.0f;
    string tmpstr = "";


	// Use this for initialization
	void Start () {
        StartCoroutine(WaitTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ChatDl()
    {
        string url2 = "http://sada-913.xyz/Unity/test/Chat_Dl.php";
        WWW www2 = new WWW(url2);
        yield return www2;//受信

        Debug.Log("文字列" + www2.text);
       // if(text.text != mytext.text)
        {
            text.text = www2.text;
        }

    }

    IEnumerator WaitTime()
    {
        while (true)
        {
            StartCoroutine(ChatDl());
            yield return new WaitForSeconds(time);
        }
    }
}
