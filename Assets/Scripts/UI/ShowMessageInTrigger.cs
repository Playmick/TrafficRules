using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowMessageInTrigger : MonoBehaviour
{
    protected DI di;

    public string Text { get => text; set => text = value; }
    [SerializeField] protected string text = "Конец";

    public Sprite Image { get => image; set => image = value; }
    [SerializeField] protected Sprite image = null;

    protected void Start()
    {
        di = DI.instance;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainMethod();
        }
    }
    protected virtual void MainMethod()
    {
        di.tooltip.timerTooltipText = Text;
        di.tooltip.ChangeImage(Image);
        //включить табличку
        di.tooltip.ShowTipWithTimer();

        di.tooltip.EndOfButtonHold += LoadMainMenu;

        //отключить телепорт
        di.rightTeleportController.SetActive(false);
        di.leftTeleportController.SetActive(false);
    }

    protected void LoadMainMenu()
    {
        di.tooltip.EndOfButtonHold -= LoadMainMenu;
        SceneManager.LoadScene("MainMenu");
    }
}
