using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumUpdate : MonoBehaviour {
    [SerializeField] Text Number;
    [SerializeField] Text Ka;
    [SerializeField] Text Name;
    [SerializeField] float seconds = 1f;
    public string[] Data;
    public static string RawData;

    int tmp_ka = 0;

    [SerializeField] List<string> str = new List<string>();


    //区切り文字
    public char delim = ',';
    public string ka = "";
    public List<string[]> csv = new List<string[]>();

    public List<string[]> ReadFile(string csv)
    {

        // Assets/Resources配下のファイルを読み込む

        // StringReaderで一行ずつ読み込んで、区切り文字で分割
        List<string[]> data = new List<string[]>();
        StringReader sr = new StringReader(csv);
        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            data.Add(line.Split(delim));
        }

        return data;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(NumUp());

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    int i = 0;
    IEnumerator NumUp()
    {

        while (true)
        {

            string url = "http://rigpp.sakura.ne.jp/Rank/NumUpdate.php";
            Dictionary<string, string> headers = new Dictionary<string, string>();


            headers.Add("Access-Control-Allow-Credentials", "true");
            headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
            headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            headers.Add("Access-Control-Allow-Origin", "*");

            WWW www = new WWW(url,null,headers);
            yield return www;
            RawData = www.text;
            Data= RawData.Split(',');

            Number.text ="現在の番号　：" + Data[0];
            string s;
            switch (int.Parse(Data[1]))
            {

                case 0: s = "コーディング"; break;
                case 1: s = "2Dグラ"; break;
                case 2: s = "3Dグラ"; break;
                case 3: s = "シナリオ"; break;
                case 4: s = "DTM"; break;
                default: s = "コーディング"; break;
            }


            Ka.text = "現在の課　：" + s;
            if(i == 0 || int.Parse(Data[1]) != tmp_ka)
            {
                StartCoroutine(CsvDl());
                i++;
            }

            tmp_ka = int.Parse(Data[1]);

            yield return new WaitForSeconds(seconds);

        }

    }

    IEnumerator CsvDl()
    {

        Dictionary<string, string> headers = new Dictionary<string, string>();


        headers.Add("Access-Control-Allow-Credentials", "true");
        headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        headers.Add("Access-Control-Allow-Origin", "*");
        switch (int.Parse(Data[1]))
        {

            case 0: ka = "coding"; break;
            case 1: ka = "2D"; break;
            case 2: ka = "3D"; break;
            case 3: ka = "sinario"; break;
            case 4: ka = "DTM"; break;
            default: ka = "coding"; break;
        }

        string url2 = "https://docs.google.com/spreadsheets/d/1CBN8LVY8tTEJMPJbFtHbdX99lvwui42qvDhK_Mr513c/gviz/tq?tqx=out:csv&sheet=" + ka;
        WWW www2 = new WWW(url2,null,headers);
        yield return www2;
        csv = ReadFile(www2.text);
        //ここまでcsv読み込み
        //csv[][2]に名前入ってる
        //例csv[1][2]で1番目の人持ってこれる
        str.Clear();
        for(int i = 1; i<csv.Count;i++)
        {
            if(csv[i][2] == "\"\"")
            {
                break;
            }
            str.Add(csv[i][2].Split('\"')[1]);
        }
        if (i == 1)
        {
            StartCoroutine(Draw());
            i++;
            Debug.Log("Draw");
        }
    }

    IEnumerator Draw()
    {
        while (true)
        {
            if (str.Count != 0 && str.Count > int.Parse(Data[0])-1)
                Name.text = str[int.Parse(Data[0]) - 1];
            else
                Name.text = "";
            yield return new WaitForSeconds(2f);
        }
    }
}
