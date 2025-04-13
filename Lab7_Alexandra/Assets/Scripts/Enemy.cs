using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    public bool hasSniper = false;
    public bool hasShotgun = false;

    private float switchTimer = 0f;
    private float switchCoolDown = 5f;
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
            }
            else 
            {
                Debug.Log("Enem using shotgun");
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
    }
    void UseShotgun()
    {
        Debug.Log("Enemy is using the shotgun!");
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

            newPos.x = Mathf.Clamp(newPos.x, -7.5f, 7.5f);
            newPos.y = Mathf.Clamp(newPos.y, -4f, 4f);
            transform.position = newPos;
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
