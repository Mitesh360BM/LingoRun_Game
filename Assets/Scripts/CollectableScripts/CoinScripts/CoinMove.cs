using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SUBSCRIBE TO VIN CODES FOR MORE FREE SCRIPTS IN FUTURE VIDEOS :)

public class CoinMove : MonoBehaviour
{

    CoinsCollectScript coinScript;
    bool isactive;
    // Start is called before the first frame update

    private void OnEnable()
    {
        coinScript = gameObject.GetComponent<CoinsCollectScript>();
        isactive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isactive)
        {
            transform.position = Vector3.MoveTowards(transform.position, coinScript.playerTransform.position,
                coinScript.moveSpeed * Time.deltaTime);
        }
    }


}
