using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NolifePanel : MonoBehaviour
{
    public void closePanel()
    {
        if (GameController.instance.isPlayerDead)
        {
            GameController.instance.OnClick_QuitViaNolife();

        }
        else
        {
            this.gameObject.SetActive(false);
        }


    }
}
