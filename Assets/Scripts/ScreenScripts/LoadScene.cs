using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;
    public GameObject Loadingpanel;

    private void Awake()
    {
        instance = this;
        loading();
       
    }
  

    List<AsyncOperation> asynloading = new List<AsyncOperation>();
    public void loading()
    {
 Loadingpanel.SetActive(true);
     asynloading.Add(  SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));
        StartCoroutine(GetProgress());
    }

    public IEnumerator GetProgress()
    {

        for (int i = 0; i < asynloading.Count; i++)
        {
            while (!asynloading[i].isDone)
            {
                yield return null;
            }

        
        }

        Loadingpanel.SetActive(false);

    }
}
