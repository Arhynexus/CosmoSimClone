using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CosmoSimClone
{



    public class PauseMenuPanel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonShowPause()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void OnButtonContinue()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void OnButtonMainMenu()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;

            SceneManager.LoadScene(LevelSequenceController.MainMenuScene);
        }
    }
}
