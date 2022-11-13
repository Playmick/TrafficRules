using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
///  вестова€ система позаимствованна€ мной из одного моего прошлого проекта
/// который € переделывал после целой команды программистов
/// </summary>

public class Quest : MonoBehaviour
{
    public int CurrentNumberStep { get; private set; }
    public Step[] steps;


    [Tooltip("«адайте кнопку дл€ перехода к следующему шагу")]
    [SerializeField] InputActionReference controllerNextStep;

    //______________________system variables
    private AudioSource auDictor;
    IEnumerator waitSpeachDictor;
    Action playstep;
    public int testStep;
    //_______________________

    private void Start()
    {
        CurrentNumberStep = 0;
    }

    protected virtual void OnEnable()
    {
        controllerNextStep.action.performed += ClickNextStepButton;
    }

    protected virtual void OnDisable()
    {
        controllerNextStep.action.performed -= ClickNextStepButton;
    }

    void ClickNextStepButton(InputAction.CallbackContext obj)
    {
        if (steps[CurrentNumberStep].clickToNextStep)
            NextStep();
    }

    void NextStep()
    {
        //остановить звук
        steps[CurrentNumberStep].eventAfterStep?.Invoke();
        CurrentNumberStep++;
        steps[CurrentNumberStep].eventBeforeStartStep?.Invoke();
        //если подсказка не как предыдуща€
            //заменить подсказку
        //если подсказка об€зательна
            //показать подсказку
        //включить звук
        //если переходим к следующему шагу после диктора то запускаем корутину котора€ ждЄт его завершени€
        //по завершении снова запускаем NextStep
    }

}

/// <summary>
/// ƒанные об одном шаге
/// </summary>
[System.Serializable]
public class Step
{
    public string name;

    [Space]
    public UnityEvent eventBeforeStartStep = new UnityEvent();


    [Tooltip("ќзвучка диктором")]
    public AudioClip dictor;

    [Space]
    [Tooltip("ѕродолжить сразу после речи диктора?")]
    public bool nextAfterDictor = false;

    [Space]
    [Tooltip("¬ывести ли подсказку принудительно на этом шаге?")]
    public bool forcedShowToolTip = false;
    [Tooltip(" ак в предыдущем")]
    public bool likePrevious = true;
    [Tooltip("“екстова€ подсказка")]
    public string tipText;

    [Tooltip("ѕереход к следующему шагу будет по клику на кнопку")]
    public bool clickToNextStep;


    [Space]
    public UnityEvent eventAfterStep = new UnityEvent();
}
