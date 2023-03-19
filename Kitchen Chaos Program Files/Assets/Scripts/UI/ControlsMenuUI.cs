using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsMenuUI : MonoBehaviour
{
    [SerializeField] private Button controlsButton;

    private void Awake()
    {
        controlsButton.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });
    }

}
