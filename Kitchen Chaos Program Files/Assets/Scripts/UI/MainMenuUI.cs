using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button controlsButton;


    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });

        controlsButton.onClick.AddListener(() => {
            SceneManager.LoadScene(2);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
