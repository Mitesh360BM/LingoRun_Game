using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WinAnimFix : MonoBehaviour
{
    public Text txt;
    public string str;
    private void OnEnable()
    {
        txt.text = str;
        doTween();
    }


    private void doTween()
    {
        Vector3 targetPos = GameObject.Find("levlVal").transform.position;
        Quaternion targetRot = GameObject.Find("levlVal").transform.rotation;

        transform.DOMove(targetPos, 1f);
        transform.DORotateQuaternion(targetRot, 2f).OnComplete(()=> { 
            
            
        
        
        
        });
    
    }
}
