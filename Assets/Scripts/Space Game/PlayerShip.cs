using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IInteractable, IDamagable
{
    [SerializeField] private SplineFollower pathFollower;
    [SerializeField] private Action action;
    [SerializeField] private IntEvent scoreEvent;
    [SerializeField] private Inventory inventory;
    [SerializeField] private IntVariable score;
    [SerializeField] private FloatVariable health;

    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject destroyPrefab;

    [SerializeField] BoolVariable reticleEngaged;

    private void Start()
    {
        health.value = 100;
        scoreEvent.Subscribe(AddPoints);

        if (action != null)
        {
            action.onEnter += OnInteractStart;
            action.onStay += OnInteractActive;
            action.onExit += OnInteractEnd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && reticleEngaged.value)
        {
            inventory.Use();
        }
        if (Input.GetButtonUp("Fire1") || !reticleEngaged.value)
        {
            inventory.StopUse();
        }

        //pathFollower.speed = Input.GetKey(KeyCode.Space) ? 30 : 20;
    }

    public void OnInteractActive(GameObject gameObject)
    {
        // 
    }

    public void OnInteractEnd(GameObject gameObject)
    {
        // 
    }

    public void OnInteractStart(GameObject gameObject)
    {
        // 
    }

    public void AddPoints(int points)
    {
        score.value += points;
        Debug.Log(score.value);
    }

    public void ApplyDamage(float damage)
    {
        health.value -= damage;
        if (health.value <= 0)
        {
            if (destroyPrefab != null)
            {
                Instantiate(destroyPrefab, gameObject.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        else
        {
            if (hitPrefab != null)
            {
                Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
            }
        }
    }

    public void AppyHealth(float health)
    {
        this.health.value += health;
        this.health.value = Mathf.Min(this.health.value, 100);
    }
}
