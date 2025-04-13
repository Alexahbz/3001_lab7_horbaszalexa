using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    public bool hasSniper = false;
    public bool hasShotgun = false;

    private float switchTimer = 0f;
    private float switchCoolDown = 5f;

    private float shootCooldown = 1.5f;
    private float shootTimer = 0f;
    private enum WeaponType { None, Shotgun, Sniper}
    private WeaponType currentWeapon = WeaponType.None;

    void Update()
    {
        if (!hasSniper || !hasShotgun)
        {
            MoveToRemainingWeapon();
        }
        if (hasSniper && hasShotgun)
        {
            switchTimer += Time.deltaTime;
            if (switchTimer >= switchCoolDown)
            {
                SwitchWeapon();
                switchTimer = 0f;
            }

            if (currentWeapon == WeaponType.Sniper)
            {
                Debug.Log("Enemy using sniper");
                UseSniper();
            }
            else 
            {
                Debug.Log("Enem using shotgun");
                UseShotgun();
            }
        }
    }

    void SwitchWeapon()
    {
        if (Random.value > 0.5f)
        {
            currentWeapon = WeaponType.Sniper;
        }
        else
        {
            currentWeapon = WeaponType.Shotgun;
        }

        Debug.Log("Switched weapon to: " + currentWeapon);
    }

    void UseSniper()
    {
        Debug.Log("Enemy is using the sniper!");

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < 4f)
        {
            Vector2 dir = (transform.position - player.position).normalized;
            Vector2 newPos = (Vector2)transform.position + dir * speed * Time.deltaTime;
            transform.position = ClampToScreen(newPos);
        }

        shootTimer += Time.deltaTime;
        if (distance >= 3.5f && shootTimer >= shootCooldown)
        {
            Debug.Log("Enemy sniped the player!");
            shootTimer = 0f;
        }
        
    }
    void UseShotgun()
    {
        Debug.Log("Enemy is using the shotgun!");

        
        Vector2 dir = (player.position - transform.position).normalized;
        Vector2 newPos = (Vector2)transform.position + dir * speed * Time.deltaTime;
        transform.position = ClampToScreen(newPos);

        float distance = Vector2.Distance(transform.position, player.position);

        shootTimer += Time.deltaTime;
        if (distance >= 1.5f && shootTimer >= shootCooldown)
        {
            Debug.Log("Enemy shot the player!");
            shootTimer = 0f;
        }
    }

    Vector2 ClampToScreen(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, -7.5f, 7.5f);
        pos.y = Mathf.Clamp(pos.y, -4f, 4f);
        return pos;
    }

    void MoveToRemainingWeapon()
    {
        GameObject sniper = GameObject.FindWithTag("Sniper");
        GameObject shotgun = GameObject.FindWithTag("Shotgun");

        GameObject target = null;

        if (!hasSniper && sniper != null)
        {
            target = sniper;
        }
        else if (!hasShotgun && shotgun != null)
        {
            target = shotgun;
        }

        if (target != null)
        { 
            Vector2 direction = (target.transform.position - transform.position).normalized;
            Vector2 newPos = (Vector2)transform.position + direction * speed * Time.deltaTime;

            transform.position = ClampToScreen(newPos);
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sniper"))
        {
            hasSniper = true;
            Destroy(other.gameObject);
            Debug.Log("Enemy picked up a sniper");
        }
        else if (other.CompareTag("Shotgun"))
        {
            hasShotgun = true;
            Destroy(other.gameObject);
            Debug.Log("Enemy picked up a shotgun");
        }
    }
}
