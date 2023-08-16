using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : Singleton<PanelManager>
{
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject MainGamePanel;

    // Start is called before the first frame update
    void Start()
    {
        ActivatePanel(MainGamePanel.name);
    }

    public void PlayerWins()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        ActivatePanel(WinPanel.name);
    }

    public void PlayerLoses()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        ActivatePanel(GameOverPanel.name);
    }

    private void ActivatePanel(string panelNameToBeActivated)
    {
        GameOverPanel.SetActive(GameOverPanel.name.Equals(panelNameToBeActivated));
        WinPanel.SetActive(WinPanel.name.Equals(panelNameToBeActivated));
        MainGamePanel.SetActive(MainGamePanel.name.Equals(panelNameToBeActivated));
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
