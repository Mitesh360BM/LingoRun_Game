using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizClass
{
    [Serializable]
    public class QuestionsClass {

        public string QuestionId;
        public string Question;
        public string optionA;
        public string optionB;
        public string optionC;
        public string answer;
        public bool isCorrect;
        public bool isWrong;    
    }


}
