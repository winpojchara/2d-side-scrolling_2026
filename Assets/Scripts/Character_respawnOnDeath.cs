using UnityEngine;

public class Character_respawnOnDeath : MonoBehaviour
{
    Character character;
    Health health;
    private Vector3 spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = GetComponent<Character>();
        health = character.GetComponent<Health>();

        spawnPoint = transform.position; //save this position at start of the game

        character.OnDeath += () => SendToRespawnLocation();
    }

    void SendToRespawnLocation()
    {
        character.transform.position = spawnPoint;
        health.ResetHealth();
        character.GetProtect();
    }
}
