using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public UnityEvent PlayerPause;
    public UnityEvent PlayerResume;

    public GameObject pauseIcon;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("ppp");
            if(GamePaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        pauseIcon.SetActive(true);
        Time.timeScale = 1f;
        GamePaused = false;
        PlayerResume.Invoke();
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        pauseIcon.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
        PlayerPause.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMenu() {
        DataPersistenceManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


}
