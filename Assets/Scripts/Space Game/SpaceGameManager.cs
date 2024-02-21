using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceGameManager : Singleton<SpaceGameManager>
{
    [Header("Game Elements")]
    [SerializeField] Slider healthbar = default;
    [SerializeField] TMP_Text scoreDisplay = default;
    [SerializeField] AudioSource bgm = default;

    [Header("Variables")]
    [SerializeField, Range(1, 5)] float gameOverDelay = 3;
    [SerializeField] BoolVariable playerDead;
    [SerializeField] FloatVariable tHealth;
    [SerializeField] IntVariable score;

    private float timer;

    private void Start()
    {
        // Variables
        tHealth.value = 100;
        score.value = 0;
        playerDead.value = false;
        timer = gameOverDelay;

        // Game Elements
        if (!bgm.isPlaying) bgm.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        scoreDisplay.text = score.value.ToString();

        healthbar.value = tHealth.value;
        if (healthbar.value <= 0f) { PlayerDead(); }

        if (playerDead.value == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f) PlayerDead();
        }
    }

    public void AddPoints(int points)
    {
        score.value += points;
    }

    public void PlayerDead()
    {
        SceneManager.LoadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync("Rail");
    }

    public void Title()
    {
        SceneManager.LoadSceneAsync("Title");
        SceneManager.UnloadSceneAsync("GameOver");
    }

    public void Begin()
    {
        SceneManager.LoadSceneAsync("Rail");
        SceneManager.UnloadSceneAsync("Title");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
