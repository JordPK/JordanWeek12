using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject startObject;
    [SerializeField] GameObject pauseObject;

    [SerializeField] TMP_Text health;

    playerMovement pm;

    public bool isPaused;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<playerMovement>();
        isPaused = true;
        Time.timeScale = 0;
        startObject.SetActive(true);
        pauseObject.SetActive(false);
        GameOverUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health" + pm.health;
    }


    public void GameOver()
    {
        pm.health = 0;
        Time.timeScale = 0f;
        GameOverUI.SetActive(true);
        isPaused = true;
    }

    public void RestartGame(int scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;

    }

    public void StartGame()
    {
        startObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
