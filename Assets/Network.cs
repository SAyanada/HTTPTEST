using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : MonoBehaviour {
    string str;
    public float SyncTime = 0.5f;
    [SerializeField]Transform t_2;
    public float Speed = 15;
    string ID;


	// Use this for initialization
	void Start () {
        StartCoroutine(Net_ID());
	}
	
	void Update () {
		
	}
    public IEnumerator NetUp(bool isNetUp,Transform t)
    {

        if (isNetUp)
        {
            //送信
            string url = "http://sada-913.xyz/Unity/test/Net.php"; //送信するphp
            WWWForm wwwForm = new WWWForm();
            wwwForm.AddField(ID, t.CreateSaveString(true, true, true, true));//Tranceformを文字列にして鯖へ
            WWW www = new WWW(url, wwwForm);
            yield return www;
        }

        if (!isNetUp)
        {
            string url2;
            if (ID == "0")
                url2 = "http://sada-913.xyz/Unity/test/Net_Dl_2.php";
            else
                url2 = "http://sada-913.xyz/Unity/test/NetDl.php";
            WWW www2 = new WWW(url2);
            yield return www2;//受信

            Debug.Log(www2.text);

            if (www2.text != null)
            {
                str = www2.text;
                t_2.SetupFromSaveString(str, false, true, true, true);
                float step = Speed * Time.deltaTime;
                t.position = Vector3.MoveTowards(t.position, t_2.position, step);
                //t.SetupFromSaveString(str, false, true, true, true);
            }
        }
    }

    IEnumerator Net_ID()
    {
        string url2 = "http://sada-913.xyz/Unity/test/Net_ID.php";
        WWW www2 = new WWW(url2);
        yield return www2;//受信

        Debug.Log(www2.text + "= ID");
        ID = www2.text;

    }


}
