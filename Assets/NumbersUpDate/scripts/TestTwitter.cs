using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twitter;
using UnityEngine.UI;

namespace Twitter
{
    public class TestTwitter : MonoBehaviour
    {
        Stream stream;
        int i = 0;
        public string search_srting;
        [SerializeField] Text text;
        [SerializeField] RawImage image;


        private void Update()
        {
            if (EventHandler.isOauth && i == 0)
            {
                i = 1;
                //Filter(search_srting);
                UserStream();
            }
        }

        public  void Tweet(string text)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["status"] = text;  // ツイートするテキスト
            StartCoroutine(Twitter.Client.Post("statuses/update", parameters, this.Callback));
        }
        
        public void Filter(string search)
        {
            stream = new Stream(StreamType.PublicFilter);
            Dictionary<string, string> streamParameters = new Dictionary<string, string>();

            List<string> tracks = new List<string>();
            tracks.Add(search);
            Twitter.FilterTrack filterTrack = new Twitter.FilterTrack(tracks);
            streamParameters.Add(filterTrack.GetKey(), filterTrack.GetValue());
            StartCoroutine(stream.On(streamParameters, OnStream_user));
        }

        public void UserStream()
        {
            stream = new Stream(StreamType.User);
            Dictionary<string, string> streamParameters = new Dictionary<string, string>();

            StartCoroutine(stream.On(streamParameters, OnStream_user));
        }
        
        void Callback(bool success, string response)
        {
            if (success)
            {
                Tweet tweet = JsonUtility.FromJson<Tweet>(response); // 投稿したツイートが返ってくる
            }
            else
            {
                Debug.Log(response);
            }
        }



        void OnStream_user(string response, StreamMessageType messageType)
        {
            try
            {
                if (messageType == StreamMessageType.Tweet)
                {
                    Tweet tweet = JsonUtility.FromJson<Tweet>(response);
                    text.text = (tweet.user.name + "\n" + tweet.text);
                    StartCoroutine(GetImage(tweet.user.profile_image_url));
                }
                else if (messageType == StreamMessageType.StreamEvent)
                {
                    StreamEvent streamEvent = JsonUtility.FromJson<StreamEvent>(response);
                    // Debug.Log(streamEvent.event_name); // Response Key 'event' is replaced 'event_name' in this library.
                }
                else if (messageType == StreamMessageType.FriendsList)
                {
                    FriendsList friendsList = JsonUtility.FromJson<FriendsList>(response);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }

        IEnumerator GetImage(string url)
        {
            WWW www = new WWW(url);

            // 画像ダウンロード完了を待機
            yield return www;

            // webサーバから取得した画像をRaw Imagで表示する
            image.texture = www.texture;
        }
    }

    
}