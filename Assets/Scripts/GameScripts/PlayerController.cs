using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static bool isGamePaused { get; set; } = false;

    private void Update()
    {
        if (!isGamePaused && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ClickDestroyEnemie();
        }
    }

    private void ClickDestroyEnemie()
    {
        if (isGamePaused)
        {
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            EnemiesControl enemy = hit.collider.GetComponent<EnemiesControl>();
            if (enemy != null)
            {
                enemy.OnMouseUp();
            }
        }
    }
}