using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public List<IRestartable> restartables;
    public List<RestartManager> restartables = new List<RestartManager>();
    static GameManager GMInstance;

    public static GameManager Instance
    {
        get
        {
            if (GMInstance == null)
            {
                GameObject manager = new GameObject("_MiGameManager");

                manager.AddComponent<GameManager>();
                GMInstance = manager.GetComponent<GameManager>();

                DontDestroyOnLoad(manager);
            }

            return GMInstance;
        }

    }
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddRestartableElement(RestartManager restartableElement)
    {
        restartables.Add(restartableElement);
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting game");
        PlayerRestart();
        UIRestart();
        CollectablesRestart();
    }

    public UIController GetUI()
    {
        return GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
    }

    public Player GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public Collectables[] GetCollectables()
    {
        return FindObjectsOfType(typeof(Collectables)) as Collectables[];
    }

    void PlayerRestart()
    {
        GetPlayer().gameObject.GetComponent<PlayerRestart>().Restart();   
    }

    void UIRestart()
    {
        GetUI().gameObject.GetComponent<UIRestart>().Restart();
    }

    void CollectablesRestart()
    {
        foreach(Collectables cl in GetCollectables())
        {
            cl.gameObject.GetComponent<CollectableRestart>().Restart();
        }
    }
}
