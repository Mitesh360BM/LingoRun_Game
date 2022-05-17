using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizAnswerObjectScript : MonoBehaviour
{

    public string option;

    public MeshRenderer OptionImage;

    public GameObject vfx_Correct, vfx_Wrong;

    public List<Material> sprites = new List<Material>();

    public void setText(string i)
    {
        option = i;
        if (option == "A")
        {
            OptionImage.material = sprites[0];

        }
        if (option == "B")
        {

            OptionImage.material = sprites[1];
        }
        if (option == "C")
        {
            OptionImage.material = sprites[2];

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (QuizController.instance.checkCorrectAnswer(option))
            {
                PlayerDataController.instance.CorrectAnswer += 1;
                EventController.instance.coinCollectEvent_fn();
                ScreenController.instance.confettiPanel.SetActive(true);
                // vfx_Correct.GetComponent<ParticleSystem>().Play();
                StartCoroutine(PlayerDataController.instance.GenTransactionID());
                PlayerDataController.instance.SendScoreToServer();
            }
            else if (!QuizController.instance.checkCorrectAnswer(option))
            {
                 EventController.instance.playerLifeEvent_fn();
               // PlayerLifeInstance.instance.Instance_playerDeadEvent();
                EventController.instance.explosionEvent_fn();
              //  vfx_Wrong.GetComponent<ParticleSystem>().Play();
            }

            Debug.Log(option);
            //API CALL TO SEND ANSWER
            StartCoroutine(QuizController.instance.SendAnswer(option));
            //


            QuizController.instance.isAnswerGiven = true;
            //Send Score to Server
           

        }
    }

   
    private void Update()
    {
        if (QuizController.instance.isAnswerGiven == true)
        {
            Destroy(gameObject,2f);

        }
    }
}
