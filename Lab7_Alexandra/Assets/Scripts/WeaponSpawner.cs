using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject shotgunPrefab;
    public GameObject sniperPrefab;

    public int totalWeapons = 4;

    private void Start()
    {
        for (int i = 0; i < totalWeapons; i++)
        {
            SpawnWeapon();
        }
    }

    void SpawnWeapon()
    {
        float x = Random.Range(-7.5f, 7.5f);
        float y = Random.Range(-4f, 4f);

        Vector2 spawnPos = new Vector2(x, y);

        if (Random.value > 0.5f)
        {
            Instantiate(shotgunPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Instantiate(sniperPrefab, spawnPos, Quaternion.identity);
        }
    }
}
