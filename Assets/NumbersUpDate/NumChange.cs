using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumChange : MonoBehaviour {
    public string[] data = { "0","0"};
    public int num = 0;
    public int ka = 0;

    [SerializeField] NumUpdate numu;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        num = int.Parse(numu.Data[0]);
        ka = int.Parse(numu.Data[1]);
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButtonDown(1))
        {

        }
        else if (Input.GetMouseButtonDown(2))
        {

        }
	}

    

    public void pulus()
    {
        num++;
        StartCoroutine(Change(num.ToString(), ka.ToString()));
    }
    public void minus()
    {
        num--;
        if (num < 1)
            num = 1;
        StartCoroutine(Change(num.ToString(), ka.ToString()));
    }
    public void Change()
    {
        ka++;
        num = 1;
        if (ka > 4)
        {
            ka = 0;
        }
        StartCoroutine(Change(num.ToString(), ka.ToString()));
    }
    public void Init()
    {
        ka = 0;
        num = 1;
        StartCoroutine(Change(num.ToString(), ka.ToString()));
    }

    IEnumerator Change(string num,string ka)
    {

        string url = "http://rigpp.sakura.ne.jp/Rank/NumChange.php";
        WWWForm wwwform = new WWWForm();
        string s;
        s = num + "," + ka;

        wwwform.AddField("a", s);
        //wwwform.AddField("a", "11,2");
        WWW www = new WWW(url, wwwform);
        yield return www;




    }
}
