using UnityEngine;

public class Character_SafeArea : MonoBehaviour
{
    Character character;
    Health health;
    string AreaTagName = "SafeArea";

    void Start()
    {
        health = GetComponent<Health>();
        character = GetComponent<Character>();
        if(health == null)
        {
            print("No health component found in " + gameObject.name);
            enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(AreaTagName))
        {
            health.ResetHealth();
            character.GetProtect();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}
