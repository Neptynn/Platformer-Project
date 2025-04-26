using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;
        public GameObject fallMenuUI;
        public GameObject finishMenuUI;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Continue();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void FallMenu()
        {
            Debug.Log("Fall Menu");
            fallMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        public void FinishMenu()
        {
            Debug.Log("Finish Menu");
            finishMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        
        public void Continue()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        private void OnEnable()
        {
            EnterFinish.OnFinish += FinishMenu;
        }

        private void OnDisable()
        {
            EnterFinish.OnFinish -= FinishMenu;
        }
    }
}
