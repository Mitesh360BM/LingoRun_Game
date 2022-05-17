using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ServerInterface : MonoBehaviour
{
     bool internetAvailable;
    public static ServerInterface instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CheckInternt();
    }

    public HttpWebResponse CallHttpRequest(string url, string data, string method="POST")
    {
        ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
      
        request.Method = method;
                request.ContentType = "application/x-www-form-urlencoded";

                if(!string.IsNullOrEmpty(data)){

                     var postData = Encoding.ASCII.GetBytes(data);
                    request.ContentLength = postData.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(postData, 0, postData.Length);
                    }


                }
                   
          HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //   StreamReader reader = new StreamReader(response.GetResponseStream());
        //   object jsonResponse = reader.ReadToEnd();

          return response;
     }

    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
        // all Certificates are accepted
        return true;
    }

    public bool CheckInternt()
    {

        StartCoroutine(CheckForInternet(resp =>
        {
            if (resp)
            {

                internetAvailable = true;
               
            }
            else
            {

                internetAvailable = false;
               
            }

           


        }));

        return internetAvailable;


    }



    public IEnumerator CheckForInternet(Action<bool> inCall)
    {
        WaitForSeconds wait = new WaitForSeconds(0.0f);

        bool internetPossiblyAvailable = false;

        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = true;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (internetPossiblyAvailable)
        {
            Ping pingServer = new Ping("8.8.8.8");
            //DebugLog.Log(pingServer.ip);
            float startTime = Time.time;

//            yield return new WaitForSeconds(1);
            while (!pingServer.isDone && Time.time < startTime + 5.0f)
            {
                yield return wait;
            }
            if (pingServer.isDone)
            {
                if (inCall != null)
                    inCall(true);
				
                pingServer.DestroyPing();
            }
            else
            {
                
                WWW checkInternet = new WWW("https://www.google.com");
                if (checkInternet.error == null)
                {

                    if (inCall != null)
                        inCall(true);


                }
                else
                {

                    if (inCall != null)
                        inCall(false);

                }
                checkInternet.Dispose();

                pingServer.DestroyPing();
            }
        }
        else
        {
            if (inCall != null)
                inCall(false);
        }

    }


     public async System.Threading.Tasks.Task<string> CallHttpFormRequestAsync(string url, MultipartFormDataContent data, string method="POST")
    {
        HttpClient httpClient = new HttpClient();
        

        HttpResponseMessage response = await httpClient.PostAsync(url, data);

        //response.EnsureSuccessStatusCode();
        //httpClient.Dispose();
        string sd = response.Content.ReadAsStringAsync().Result;
        return sd;
     }


}
