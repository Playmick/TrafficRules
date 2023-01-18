using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victorine : MonoBehaviour
{
    private DI di;

    //массив вопросов
    public Question[] Questions;

    public int CurrentQuestion = 0;

    [SerializeField]private Color greenColor;
    [SerializeField]private Color redColor;

    private int CountAlreadyReplyAnswers = 0;

    private void Start()
    {
        //выключить все канвасы
        //выключить все надписи закрытия

        //тут скорее всего нужно проверять не на нул, а на кол-во элементов
        if (Questions.Length > 0)
        {
            foreach (Question q in Questions)
            {
                q.CloseText.text = "";
                q.Canvas.enabled = false;
            }
        }
        else
        {
            Debug.Log("Заполни массив Questions");
        }
    }

    public void Activate()
    {
        Questions[CurrentQuestion].Canvas.enabled = true;
    }

    //забиндить все кнопки
    private void OnEnable()
    {
        
        foreach (Question q in Questions)
        {
            if (q.Answers!=null)
            {
                foreach (Answer a in q.Answers)
                    a.Button.onClick.AddListener(() => OnButtonClick(a));
            }
            else
            {
                Debug.Log("Заполни массив Answers");
            }
        }
        
    }

    //отбиндить все кнопки
    private void OnDisable()
    {
        //тут скорее всего нужно проверять не на нул, а на кол-во элементов
        if (Questions.Length > 0)
        {
            foreach (Question q in Questions)
            {
                if (q.Answers.Length > 0)
                {
                    foreach (Answer a in q.Answers)
                        a.Button.onClick.RemoveAllListeners();
                }
                else
                {
                    Debug.Log("Заполни массив Answers");
                }
            }
        }
        else
        {
            Debug.Log("Заполни массив Questions");
        }
    }

    void OnButtonClick(Answer _answer)
    {
        if(CountAlreadyReplyAnswers < Questions[CurrentQuestion].CountRightAnswers)
        {
            //подсветить выбранную кнопку нужным цветом
            if (_answer.IsRight)
                _answer.Button.image.color = greenColor;
            else
                _answer.Button.image.color = redColor;

            CountAlreadyReplyAnswers++;
        }
        
        if(CountAlreadyReplyAnswers >= Questions[CurrentQuestion].CountRightAnswers)
        {
            //включить надпись зажмите курок на 3 сек
            Questions[CurrentQuestion].UpdateCloseText();

            //уменьшение времени
            di.holdThreeSeconds.ReduceTime += Questions[CurrentQuestion].ReduceTime;
            //сброс времени
            di.holdThreeSeconds.ResetTime += Questions[CurrentQuestion].ResetTime;

            Questions[CurrentQuestion].NextQuestion += NextQuestion;
        }
        
    }

    private void NextQuestion()
    {
        //уменьшение времени
        di.holdThreeSeconds.ReduceTime -= Questions[CurrentQuestion].ReduceTime;
        //сброс времени
        di.holdThreeSeconds.ResetTime -= Questions[CurrentQuestion].ResetTime;

        //закрыли текущий канвас
        Questions[CurrentQuestion].Canvas.enabled = false;

        //если текущий канвас последний
        //то ничё не делаем

        //если текущий канвас не последний то включаем следующий

        CurrentQuestion++;
        if (CurrentQuestion < Questions.Length)
            Questions[CurrentQuestion].Canvas.enabled = true;
    }

    

    
}

[System.Serializable]
public class Question
{
    //экран с вопросом
    public Canvas Canvas;

    public Text CloseText;

    //текст для вопроса
    public Text TextQuestion;

    //массив ответов(это должны быть кнопки)
    public Answer[] Answers;

    public Action NextQuestion;

    //сколько должно быть верных ответов?
    public int CountRightAnswers
    {
        get
        {
            if (countRightAnswers <= 0)
            {
                if (Answers.Length > 0)
                {
                    foreach (Answer a in Answers)
                    {
                        if (a.IsRight)
                            countRightAnswers++;
                    }
                }
                else
                {
                    Debug.Log("Добавьте ответы в массив");
                }
            }
            return countRightAnswers;
        }
        set
        {
            countRightAnswers = value;
        }
    }
    public void UpdateCloseText()
    {
        CloseText.text = $"Удерживайте курок контроллера {time} секунды для выхода из сценария";
    }

    public void ReduceTime()
    {
        time = time - 1;
        UpdateCloseText();
        if (time <= 0)
            NextQuestion?.Invoke();
    }

    public void ResetTime()
    {
        time = 3;
        UpdateCloseText();
    }

    private int countRightAnswers = 0;
    private int time;

}

[System.Serializable]
public class Answer
{
    //кнопка
    public Button Button;

    //верный?
    public bool IsRight;
}

