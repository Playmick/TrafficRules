using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victorine : MonoBehaviour
{
    [SerializeField] bool TurnOnInStart;
    //������ ��������
    public Question[] Questions;

    public int CurrentQuestion = 0;

    [SerializeField]private Color greenColor;
    [SerializeField]private Color redColor;

    private DI di;
    private int CountAlreadyReplyAnswers = 0;

    private void Start()
    {
        di = DI.instance;
        //��������� ��� �������
        //��������� ��� ������� ��������

        //��� ������ ����� ����� ��������� �� �� ���, � �� ���-�� ���������
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
            Debug.Log("������� ������ Questions");
        }

        if (TurnOnInStart)
        {
            Activate();
        }
    }

    public void Activate()
    {
        Questions[CurrentQuestion].Canvas.enabled = true;
        Questions[CurrentQuestion].Time = 3;

        //��� ����� ������� ���� ������� ���� ���� �� ���� �� ������, � ���� �� ���� ��
        //����� ��������� ��������� ����� "��������� ����������� ��������������/�������"
        //�� ������� ���, ������� ������ ������ �����
        //���� ���������� ��������� ����� � ���� ������� ������������ �������� ����������� �����
        di.RightRay.SetActive(true);
        di.LeftRay.SetActive(true);
    }

    public void Deactivate()
    {
        Questions[CurrentQuestion].Canvas.enabled = false;

        //��� ����� ������� ���� ������� ���� ���� �� ���� �� ������, � ���� �� ���� ��
        //����� ��������� ��������� ����� "��������� ����������� ��������������/�������"
        //�� ������� ���, ������� ������ ������ �����
        //���� ���������� ��������� ����� � ���� ������� ������������ �������� ����������� �����
        di.RightRay.SetActive(false);
        di.LeftRay.SetActive(false);
    }

    //��������� ��� ������
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
                Debug.Log("������� ������ Answers");
            }
        }
        
    }

    //��������� ��� ������
    private void OnDisable()
    {
        //��� ������ ����� ����� ��������� �� �� ���, � �� ���-�� ���������
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
                    Debug.Log("������� ������ Answers");
                }
            }
        }
        else
        {
            Debug.Log("������� ������ Questions");
        }
    }

    void OnButtonClick(Answer _answer)
    {
        if(CountAlreadyReplyAnswers < Questions[CurrentQuestion].CountRightAnswers)
        {
            //���������� ��������� ������ ������ ������
            if (_answer.IsRight)
            {
                _answer.Button.image.color = greenColor;

                //��� ����� �� ������ �������� ��� ������������, �� ����� �����
                di.tooltip.ChangeImage(di.Svet1);
                di.tooltip.ChangeTooltipText("���! �� ��������� ��������������� ������!");
                di.tooltip.UpdateCloseText();
                //�������� ��������
                di.tooltip.ShowTip();
            }
            else
                _answer.Button.image.color = redColor;

            CountAlreadyReplyAnswers++;
        }
        
        if(CountAlreadyReplyAnswers >= Questions[CurrentQuestion].CountRightAnswers)
        {
            

            //�������� ������� ������� ����� �� 3 ���
            Questions[CurrentQuestion].UpdateCloseText();
            Questions[CurrentQuestion].ResetTime();

            //���������� �������
            di.holdThreeSeconds.ReduceTime += Questions[CurrentQuestion].ReduceTime;
            //����� �������
            di.holdThreeSeconds.ResetTime += Questions[CurrentQuestion].ResetTime;

            Questions[CurrentQuestion].NextQuestion += NextQuestion;
        }
        
    }

    private void NextQuestion()
    {
        //���������� �������
        di.holdThreeSeconds.ReduceTime -= Questions[CurrentQuestion].ReduceTime;
        //����� �������
        di.holdThreeSeconds.ResetTime -= Questions[CurrentQuestion].ResetTime;

        //������� ������� ������
        Deactivate();

        //���� ������� ������ ���������
        //�� ���� �� ������

        //���� ������� ������ �� ��������� �� �������� ���������
        CurrentQuestion++;
        if (CurrentQuestion < Questions.Length)
            Activate();
    }

    

    
}

[System.Serializable]
public class Question
{
    //����� � ��������
    public Canvas Canvas;

    public Text CloseText;

    //����� ��� �������
    public Text TextQuestion;

    //������ �������(��� ������ ���� ������)
    public Answer[] Answers;

    public Action NextQuestion;

    public int Time;

    private int countRightAnswers = 0;

    //������� ������ ���� ������ �������?
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
                    Debug.Log("�������� ������ � ������");
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
        CloseText.text = $"����������� ����� ����������� {Time} ������� ��� �������� �����.";
    }

    public void ReduceTime()
    {
        Time = Time - 1;
        UpdateCloseText();
        if (Time <= 0)
            NextQuestion?.Invoke();
    }

    public void ResetTime()
    {
        Time = 3;
        UpdateCloseText();
    }

}

[System.Serializable]
public class Answer
{
    //������
    public Button Button;

    //������?
    public bool IsRight;
}

