using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    public bool hasSniper = false;
    public bool hasShotgun = false;

    void Update()
    {
        if (!hasSniper && !hasShotgun)
        {
            FleeFromPlayer();
        }
    }

    void FleeFromPlayer()
    {
        Vector2 direction = (transform.position - player.position).normalized;
        Vector2 newPos = (Vector2)transform.position + direction * speed * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, -7.5f, 7.5f);
        newPos.y = Mathf.Clamp(newPos.y, -4f, 4f);

        transform.position = newPos;
    }
}
