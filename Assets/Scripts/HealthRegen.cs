using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    Health health;
    [SerializeField] private float regenRate = 3;

    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == null) return;
        float value = regenRate * Time.deltaTime;

        health.RestoreHealth(value);
    }
}
