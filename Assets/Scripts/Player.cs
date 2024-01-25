using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] FloatVariable health;
    [SerializeField] PhysicsCharacterController characterController;
    [SerializeField] OrbitCamera cameraController;

    [Header("Events")]
    [SerializeField] IntEvent scoreEvent = default;
    [SerializeField] VoidEvent playerDeadEvent = default;
    [SerializeField] VoidEvent gameStartEvent = default;

    private int score = 0;

    public int Score { 
        get { return score; }
        set { 
            score = value; 
            scoreText.text = score.ToString();
            scoreEvent.RaiseEvent(score);
        } 
    }

    private void OnEnable()
    {
        gameStartEvent.Subscribe(OnStartGame);
    }

    private void Start()
    {
        //
    }

    public void AddPoints(int points)
    {
        Score += points;
    }

    private void OnStartGame()
    {
        characterController.enabled = true;
        cameraController.enabled = true;
        health.value = 100.0f;
    }

    public void OnEndGame()
    {
        characterController.enabled = false;
        cameraController.enabled = false;
    }

    public void Damage(float damage)
    {
        health.value -= damage;

        if (health.value <= 0)
        {
            playerDeadEvent.RaiseEvent();
        }
    }

    public void OnRespawn(GameObject respawn)
    {
        transform.position = respawn.transform.position;
        transform.rotation = respawn.transform.rotation;
        characterController.Reset();

        health.value = 50.0f;
    }
}
