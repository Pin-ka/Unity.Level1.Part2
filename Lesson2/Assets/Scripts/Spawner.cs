using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 1f;        // The amount of time between each spawn.
    public float spawnDelay = 0f;       // The amount of time before spawning starts.
    public GameObject[] enemies;        // Array of enemy prefabs.
    public GameObject Platform;
    public Collider2D[] PlatformsT;
    private Vector3 CurrentPos;


    void Start()
    {
        PlatformsT = Platform.transform.GetComponentsInChildren<Collider2D>();
        // Start calling the Spawn function repeatedly after a delay .
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }


    void Spawn()
    {
        CurrentPos = PlatformsT[Random.Range(0, PlatformsT.Length)].transform.position;
        Vector3 temp = new Vector3(CurrentPos.x-0.5f,CurrentPos.y+2,CurrentPos.z);
        // Instantiate a random enemy.
        int enemyIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyIndex], temp, transform.rotation);

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }
}
