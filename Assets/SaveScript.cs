using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveScript : MonoBehaviour
{

    string str;
    public InputField inputField;
    public Text text;

    public bool isChatUp;

    public void SaveText()
    {
        str = inputField.text;
        text.text = str;
        
        StartCoroutine(Chatup());
        inputField.text = "";
    }


    IEnumerator Chatup()
    {

            //送信
            string url = "http://sada-913.xyz/Unity/test/ChatUp.php"; //送信するphp
            WWWForm wwwForm = new WWWForm();
            wwwForm.AddField("chat", str);//Tranceformを文字列にして鯖へ
            WWW www = new WWW(url, wwwForm);
            yield return www;
            Debug.Log("送信完了");
            
    }
}