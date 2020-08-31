using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIRestart))]
public class UIController : MonoBehaviour
{
    public Text coinText;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject winMenu;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();
        GameManager.Instance.AddRestartableElement(GetComponent<UIRestart>());
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = player.coins.ToString();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnGameOver() 
    { 
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnWin()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
