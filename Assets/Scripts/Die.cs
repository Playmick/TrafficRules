using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : ShowMessageInTrigger
{
    protected DI di;

    public string DieText { get => dieText; set => dieText = value; }
    [SerializeField]private string dieText = "Вы проиграли";

    public Sprite DieImage { get => dieImage; set => dieImage = value; }
    [SerializeField] private Sprite dieImage = null;

    void Start()
    {
        di = DI.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //"вспышка"
            di.blackColor.SetActive(true);
            di.flashBlindness.SetActive(true);

            di.tooltip.ChangeTooltipText(DieText);
            //di.tooltip.CloseImage();
            di.tooltip.ChangeImage(DieImage);

            di.tooltip.UpdateTimerText();
            //включить табличку
            di.tooltip.ShowTipWithTimer();

            di.tooltip.EndOfButtonHold += LoadMainMenu;

            //отключить телепорт
            di.rightTeleportController.SetActive(false);
            di.leftTeleportController.SetActive(false);

            //включаем звук
            di.dieSound?.Play();
        }
    }

    private void LoadMainMenu()
    {
        di.tooltip.EndOfButtonHold -= LoadMainMenu;
        SceneManager.LoadScene("MainMenu");
    }
}
