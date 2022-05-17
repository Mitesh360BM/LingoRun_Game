using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public DifficultyController playerDifficulty;
    public float StartDelay, repeatDelay;
    public List<GameObject> lanes;
    public List<GameObject> carObject;
    public GameObject playerObject;
    public float zOffset;

    public bool isReady;
    private void Start()
    {
        // InvokeRepeating("spawn_Cars", StartDelay,repeatDelay);

    }
    private void OnEnable()
    {
      //  StartCoroutine(spawn_Cars());
        
    }




    IEnumerator spawn_Cars()
    {
        yield return new WaitUntil(() => !QuizController.instance.isQuestionVisible);
        if (playerDifficulty.isEasy)
        {
            repeatDelay = 1.25f;
        
        }
        if (playerDifficulty.isMedium)
        {
            repeatDelay = 1f;
        }

        if (playerDifficulty.isHard)
        {


            repeatDelay = 0.75f;
        }
        yield return new WaitUntil(()=> (GameController.instance.isGameStart && !GameController.instance.isPlayerDead));
        if (GameController.instance.isGameStart && !GameController.instance.isPlayerDead)
        {
            if (isReady)
            {
                int k = Random.Range(0, lanes.Count);
                int i = Random.Range(0, carObject.Count);
                if (!carObject[i].activeInHierarchy)
                {
                    GameObject car = Instantiate(carObject[i], new Vector3(lanes[k].transform.position.x, carObject[i].transform.position.y, transform.position.z), transform.rotation);
                    Debug.Log("Car Spawn: " + carObject[i].name);
                    // carObject[i].SetActive(true);
                    // carObject[i].transform.position = new Vector3(lanes[k].transform.position.x, carObject[i].transform.position.y, transform.position.z);
                }
                else
                {
                    // spawn_Cars();

                }
            }
          
          
        }

        yield return new WaitForSeconds(repeatDelay);
       

        StartCoroutine(spawn_Cars());
    
    }





    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(0,0,playerObject.transform.position.z+ zOffset);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Traps")
        {
            isReady = false;
        
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Traps")
        {
            isReady = true;


        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Traps")
        {
            isReady = false;


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
}
