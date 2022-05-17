using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class QuizUIScript : MonoBehaviour
{
    public GameObject ballonObject;
    public float ballonDefaultY, balloonTargetY, balloonAnimateTime;

  

    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public TextMeshProUGUI option3Text;




    public void setQuizQuestion(string question, string option1, string option2, string option3)
    {
        QuestionText.text = question;
        option1Text.text = "A: "+option1;
        option2Text.text = "B: " + option2;
        option3Text.text = "C: " + option3;
        StartCoroutine(AnimateBalloon());

    }

    private IEnumerator AnimateBalloon()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        QuizController.instance.isQuestionVisible = true;
        ballonObject.transform.DOLocalMoveY(balloonTargetY, balloonAnimateTime);
        yield return new WaitUntil(()=>QuizController.instance.isAnswerGiven);
        ballonObject.transform.DOLocalMoveY(ballonDefaultY, balloonAnimateTime);
      
    }

}
