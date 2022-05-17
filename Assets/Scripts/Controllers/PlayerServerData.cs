using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServerData : MonoBehaviour
{
    public static PlayerServerData instance;

    [Header("UserData")]
    public string userName;
    public string email;
    public string isVerified;
    public string grade;
    public string walletID;
    public string ImageUrl;



    private void Awake()
    {
        instance = this;

    }



    private void GetUserDetails()

    {

        StartCoroutine(APIController.instance.GetUserDataById((status)=> {

            if (status != null && !string.IsNullOrEmpty(status.ToString()))
            {
                var data = JObject.Parse(status);

                userName = data["userName"].ToString();
                email= data["userName"].ToString();
                isVerified= data["userName"].ToString(); 
                grade= data["userName"].ToString();
                walletID= data["userName"].ToString();
                ImageUrl= data["userName"].ToString();
            }
        
        
        }));
    }


}
