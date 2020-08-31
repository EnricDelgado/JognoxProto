using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRestart : RestartManager
{
    public override void Restart()
    {
        base.Restart();
        UIController UI = GameManager.Instance.GetUI();

        UI.gameOverMenu.SetActive(false);
        UI.pauseMenu.SetActive(false);
        Time.timeScale = 1;

        Debug.Log("UI restarted");
    }
}
