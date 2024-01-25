using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Start UI")]
    [SerializeField] GameObject startUI;
    
    [Header("Game UI")]
    [SerializeField] GameObject gameUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] TMP_Text timerUI;
    [SerializeField] Slider healthUI;

    [Header("End UI")]
    [SerializeField] GameObject endUI;
    [SerializeField] TMP_Text scoreUI;

    [Header("Player Properties")]
    [SerializeField] FloatVariable health;
    [SerializeField] GameObject respawn;

    [Header("Events")]
    [SerializeField] VoidEvent gameStartEvent;
    [SerializeField] VoidEvent gameEndEvent;
    [SerializeField] GameObjectEvent respawnEvent;

    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        RESPAWN,
        GAME_OVER,
        WIN
    }

    public State state = State.TITLE;
    private float timer = 0;
    private int lives = 0;
    private float[] prevHealth = new float[5];

    public int Lives 
    { 
        get { return lives; } 
        set { 
            lives = value; 
            livesUI.text = "LIVES: " + lives.ToString(); 
        } 
    }

    public float Timer
    {
        get { return timer; }
        set {
            timer = value;
            if (timer < 10) timerUI.text = "0" + string.Format("{0:F1}", timer); else timerUI.text = string.Format("{0:F1}", timer);
        }
    }

    private void OnEnable()
    {
        // scoreEvent.Subscribe(OnAddPoints);
    }

    private void OnDisable()
    {
        // scoreEvent.Unsubscribe(OnAddPoints);
    }

    // Start is called before the first frame update
    void Start()
    {
        // scoreEvent.Subscribe(OnAddPoints);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.TITLE:
                // UI
                startUI.SetActive(true);
                gameUI.SetActive(false);
                endUI.SetActive(false);

                // Cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                break;

            case State.START_GAME:
                // UI
                startUI.SetActive(false);
                gameUI.SetActive(true);
                endUI.SetActive(false);

                // Cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // Properties
                Timer = 60;
                Lives = 3;

                // Events
                gameStartEvent.RaiseEvent();
                respawnEvent.RaiseEvent(respawn);

                // State
                state = State.PLAY_GAME;

                break;

            case State.PLAY_GAME:
                // Properties
                Timer = Timer - Time.deltaTime;
                if (Timer <= 0) { state = State.GAME_OVER; }

                break;

            case State.RESPAWN:
                // Properties
                Timer += 15;
                Lives--;

                // Events/State
                if (Lives == 0) { gameEndEvent.RaiseEvent(); }
                else { respawnEvent.RaiseEvent(respawn); state = State.PLAY_GAME; }

                break;

            case State.GAME_OVER:
                // UI
                startUI.SetActive(false);
                gameUI.SetActive(false);
                endUI.SetActive(true);

                // Cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                break;

            case State.WIN:
                break;

            default:
                break;
        }

        healthUI.value = health.value / 100.0f;
    }

    public void OnStartGame()
    {
        state = State.START_GAME;
    }

    public void OnEndGame()
    {
        state = State.GAME_OVER;
    }

    public void OnPlayerDead()
    {
        state = State.RESPAWN;
    }

    public void OnAddPoints(IntVariable score)
    {
        scoreUI.text = "Score: " + score.value;
        print(score.value);
    }
}
