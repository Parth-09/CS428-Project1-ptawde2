using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;

public class Time24API : MonoBehaviour
{
    // datetime	"2022-09-15T22:29:45.812539-05:00"
    public GameObject timeTextObject;
    string url = "http://worldtimeapi.org/api/timezone/Asia/Kolkata";
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTime", 0f, 10f);
    }
    void UpdateTime()
    {
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {

                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                int startTime = webRequest.downloadHandler.text.IndexOf("datetime", 0);
                int endTime = webRequest.downloadHandler.text.IndexOf(",", startTime);
                string hour = webRequest.downloadHandler.text.Substring(startTime + 22, (endTime - startTime - 42));
                string min = webRequest.downloadHandler.text.Substring(startTime + 25, (endTime - startTime - 42));
                timeTextObject.GetComponent<TextMeshPro>().text = "" + hour + ":" + min;
            }
        }
    }

}