using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : ShowMessageInTrigger
{
    private DI di;

    
    public GameObject arrow;

    //текст победы мен€етс€ через квест
    public string WinText { get => winText; set => winText = value; }
    [SerializeField]private string winText = "¬ыполнено";

    public Sprite WinImage { get => winImage; set => winImage = value; }
    [SerializeField] private Sprite winImage = null;

    //bool tablNotSubscribe;
    void Start()
    {
        di = DI.instance;
        //tablNotSubscribe = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinMethod();
        }
    }

    public void WinMethod()
    {
        //сменить текст на табличке
        di.tooltip.ChangeTooltipText(WinText);
        di.tooltip.UpdateTimerText();

        di.tooltip.ChangeImage(WinImage);

        //включить табличку
        di.tooltip.ShowTipWithTimer();

        di.tooltip.EndOfButtonHold += LoadMainMenu;
        
        //отключить телепорт
        di.rightTeleportController.SetActive(false);
        di.leftTeleportController.SetActive(false);

        arrow?.SetActive(false);
    }

    private void LoadMainMenu()
    {
        di.tooltip.EndOfButtonHold -= LoadMainMenu;
        SceneManager.LoadScene("MainMenu");
    }
}
