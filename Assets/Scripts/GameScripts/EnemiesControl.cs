using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesControl : MonoBehaviour
{
    public SpawnerEnemies spawner;
    [SerializeField] private float moveSpeed;
    private Vector3 size;
    private SpriteRenderer spriteRenderer;
    public GameObject explotionPrefab;


    private int moveDirection; 
    private float boundary = 8.2f; 
    private bool movingUp = true;
    private float upperLimit = 3f; 
    private float lowerLimit = 0f; 
    private float verticalSpeed = 1.5f;

    public int clicksToKill = 1; 
    private int currentClicks = 0;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        moveDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        SetGhostProperties(); 
    }

    private void Update()
    {
        MoveEnemy();
    }

    public void SetGhostProperties()
    {
        if (tag == "Grande")
        {
            //moveSpeed = 5.0f;
            size = new Vector3(0.6f, 0.5f, 1); 
        }
        else if (tag == "Pequeño")
        {
            //moveSpeed = 9.0f;
            size = new Vector3(0.4f, 0.4f, 1); 
        }
        transform.localScale = size;
    }

    private void MoveEnemy()
    {
        Vector2 position = transform.position;
        position.x += moveDirection * moveSpeed * Time.deltaTime;

        if (moveDirection < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDirection > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (position.x >= boundary || position.x <= -boundary)
        {
            moveDirection *= -1;
            position.x = Mathf.Clamp(position.x, -boundary, boundary);

            spriteRenderer.flipX = moveDirection < 0; 
        }

        if (movingUp)
        {
            position.y += verticalSpeed * Time.deltaTime;
            if (position.y >= upperLimit) movingUp = false;
        }
        else
        {
            position.y -= verticalSpeed * Time.deltaTime;
            if (position.y <= lowerLimit) movingUp = true;
        }

        transform.position = position;
    }
    public void OnMouseUp()
    {
        if (PanelOptionsController.isGamePaused)
            return;
        currentClicks++;

        if (currentClicks >= clicksToKill)
        {
            if (spawner != null)
            {
                spawner.IncreaseKillCount();
            }
            SfxManager.Instance.audioSource.PlayOneShot(SfxManager.Instance.poof);
            Destroy(gameObject);
            GameObject explotion = Instantiate(explotionPrefab, transform.position, transform.rotation);
            Destroy(explotion, 0.3f);
        }
    }
}