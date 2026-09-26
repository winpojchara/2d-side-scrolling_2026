using UnityEngine;
using UnityEngine.UI;

public class HealthUIDisplay : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Slider healthSlider;

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = GetValue();
    }

     float GetValue()
    {
        return (float)health.CurrentHealth / (float)health.MaxHealth;
    }
}
