using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelScript : MonoBehaviour
{
    public Transform player;
    public Transform cameratransform;
    
  
    private void OnEnable()
    {
        player.position = new Vector3(0, 0, -112.23f);
       // cameratransform.position = new Vector3(0,35.3f,-127.87f);
       //StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(0f);

        this.gameObject.SetActive(false);

        
    }
    public void GameReset()
    {
        GameController.instance.GameReset();
       
    }
}
