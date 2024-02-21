using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallsGameManager : Singleton<BallsGameManager>
{
    [Header("GUIs")]
    [SerializeField] GameObject startMenu = default;
    [SerializeField] GameObject gameUI = default;
    [SerializeField] GameObject winScreen = default;
    [SerializeField] GameObject loseScreen = default;

    [Header("Game Elements")]
    [SerializeField] Slider healthbar = default;
    [SerializeField] TMP_Text livesDisplay = default;
    [SerializeField] TMP_Text scoreDisplay = default;
    [SerializeField] TMP_Text timerDisplay = default;
    [SerializeField] TMP_Text descriptionDisplay = default;
    [SerializeField] Transform killZone = default;
    [SerializeField] AudioSource bgm = default;
    [SerializeField] AudioSource damageTaken = default;

    [Header("Variables")]
    [SerializeField] IntVariable lives = default;
    [SerializeField] IntVariable score = default;
    [SerializeField] FloatVariable health = default;
    [SerializeField] FloatVariable timer = default;
    [SerializeField] StringVariable description = default;
    [SerializeField, Range(1, 5)] float respawnDelay = 3;

    [Header("Events")]
    [SerializeField] VoidEvent startGameEvent = default;
    [SerializeField] VoidEvent winGameEvent = default;
    [SerializeField] VoidEvent loseGameEvent = default;
    [SerializeField] VoidEvent respawnEvent = default;
    [SerializeField] VoidEvent playerDeadEvent = default;
    [SerializeField] VoidEvent descendEvent = default;

    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        WIN,
        LOSE,
        RESPAWN
    }

    private float respawnTimer;
    private bool fire1 = false, fire2 = false;
    private State state = State.TITLE;

    public int Lives 
    { 
        get { return lives.value; } 
        set { 
            lives.value = value; 
            livesDisplay.text = "LIVES: " + lives.value;
        } 
    }

    public int Score
    {
        get { return score.value; }
        set
        {
            score.value = value;
            scoreDisplay.text = "POWER: " + score.value.ToString();
        }
    }

    public float Health
    {
        get { return health.value; }
        set
        {
            health.value = value;
            healthbar.value = health.value / 100.0f;
        }
    }

    public float Timer
    {
        get { return timer.value; }
        set {
            timer.value = value;
            if (timer.value < 10) timerDisplay.text = "0" + string.Format("{0:F1}", timer.value); else timerDisplay.text = string.Format("{0:F1}", timer.value);
        }
    }

    public string Description
    {
        get { return description.value; }
        set
        {
            description.value = value;
            descriptionDisplay.text = description.value;
        }
    }

    private void Start() { respawnTimer = respawnDelay; }

    // Update is called once per frame
    private void Update()
    {
        switch (state)
        {
            case State.TITLE:
                // GUI
                startMenu.SetActive(true);
                gameUI.SetActive(false);
                winScreen.SetActive(false);
                loseScreen.SetActive(false);

                // Cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Game Elements
                bgm.Stop();

                break;

            case State.START_GAME:
                // GUI
                startMenu.SetActive(false);
                gameUI.SetActive(true);
                winScreen.SetActive(false);
                loseScreen.SetActive(false);

                // Cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // Properties
                Lives = 5;
                Score = 0;
                Health = 100;
                Timer = 120;
                Description = "Hold down the left mouse button while falling to float.";

                // Game Elements
                killZone.position = new Vector3(125, -10, 125);
                if (!bgm.isPlaying) bgm.Play();

                // Events
                startGameEvent.RaiseEvent();

                // State
                state = State.PLAY_GAME;

                break;

            case State.PLAY_GAME:
                // Properties
                Timer = timer.value;
                Timer -= Time.deltaTime;
                Description = description.value;
                Health = health.value;

                // Game Elements
                if (Score >= 50 && Score < 400)
                {
                    killZone.position = new Vector3(125, -60, 125);

                    if (!fire1) { fire1 = true; descendEvent.RaiseEvent(); }
                }
                else if (Score >= 400)
                {
                    killZone.position = new Vector3(125, -160, 125);

                    if (!fire2) { fire2 = true; descendEvent.RaiseEvent(); }
                }

                // State
                if (Timer <= 0) state = State.LOSE;
                if (Score >= 1000) state = State.WIN;

                break;

            case State.WIN:
                // GUI
                gameUI.SetActive(false);
                winScreen.SetActive(true);

                // Cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Game Elements
                killZone.position = new Vector3(125, -360, 125);

                // Events
                winGameEvent.RaiseEvent();

                break;

            case State.LOSE:
                // GUI
                gameUI.SetActive(false);
                loseScreen.SetActive(true);

                // Cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Events
                loseGameEvent.RaiseEvent();

                break;

            case State.RESPAWN:
                // Logic
                respawnTimer -= Time.deltaTime;

                if (respawnTimer <= 0)
                {
                    // Properties
                    Lives -= 1;
                    Timer += 3;
                    Health = 50;

                    // Events
                    respawnEvent.RaiseEvent();

                    // State
                    if (Lives == 0) state = State.LOSE; else state = State.PLAY_GAME;

                    respawnTimer = respawnDelay;
                }

                break;

            default:
                break;
        }
    }

    // Event Listener Functions
    public void OnTitle() { state = State.TITLE; }

    public void OnStartGame() { state = State.START_GAME; }

    public void OnScore(int points) { Score += points; }

    public void OnDamage(float damage) 
    {
        Health -= damage;
        damageTaken.Play();

        if (Health <= 0)
        {
            playerDeadEvent.RaiseEvent();
            state = State.RESPAWN;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void Awake()
    {
        startMenu.SetActive(true);
    }
}
