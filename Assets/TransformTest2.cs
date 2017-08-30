using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest2 : MonoBehaviour {

    [SerializeField] Network n;
    [SerializeField] bool isNetUp;
    [SerializeField] string ID;

    void Start()
    {
        //StartCoroutine(Net_ID());
        StartCoroutine(Five_seconds());

    }
    IEnumerator Five_seconds()
    {
        while (true)
        {
            StartCoroutine(n.NetUp(isNetUp, transform));
            yield return new WaitForSeconds(n.SyncTime);
        }
    }

    IEnumerator Net_ID()
    {/*
        string url2 = "http://sada-913.xyz/Unity/test/Net_ID.php";
        WWW www2 = new WWW(url2);
        yield return www2;//受信

        Debug.Log(www2.text + "= ID");
        ID = www2.text;
        */

        yield return null;
    }
}
