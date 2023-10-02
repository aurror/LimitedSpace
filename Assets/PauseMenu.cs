using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu panel.
    public Button menuButton; // Reference to the "Menu" button.
    public Button continueButton; // Reference to the "Continue" button.
    public Button controlButtonButton; // Reference to the "Continue" button.
    public GameObject control;
    public GameObject menu;

    private bool isPaused = false;

    private void Start()
    {
        // Ensure the pause menu is initially hidden.
        pauseMenu.SetActive(false);

        // Add click event listeners to the buttons.
        menuButton.onClick.AddListener(OpenMainMenu);
        continueButton.onClick.AddListener(ResumeGame);
        controlButtonButton.onClick.AddListener(OpenControls);
    }

    private void Update()
    {
        // Toggle the pause menu when the "Escape" key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game by setting the time scale to zero.
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game by setting the time scale back to one.
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    void OpenMainMenu()
    {
        ResumeGame();
        // Implement code to open your main menu scene or perform other actions here.
        SceneManager.LoadScene("Menu");
    }

    void OpenControls()
    {
        menu.SetActive(false);
        control.SetActive(true);
    }

    public void CloseControls()
    {
        menu.SetActive(true);
        control.SetActive(false);
    }
}

