using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvReader : MonoBehaviour
{
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

    private void Start()
    {
        StartCoroutine(CsvDl());
    }
    IEnumerator CsvDl()
    {
        string[] data = NumUpdate.RawData.Split(',');
        switch (int.Parse(data[1]))
        {

            case 0: ka = "coding"; break;
            case 1: ka = "2D"; break;
            case 2: ka = "3D"; break;
            case 3: ka = "sinario"; break;
            case 4: ka = "DTM"; break;
            default: ka = "coding"; break;
        }

        string url = "https://docs.google.com/spreadsheets/d/1CBN8LVY8tTEJMPJbFtHbdX99lvwui42qvDhK_Mr513c/gviz/tq?tqx=out:csv&sheet=" + ka;
        WWW www = new WWW(url);
        yield return www;
        csv = ReadFile(www.text);
    }
}