using UnityEngine;

public class Character_TriggerTimeHop : MonoBehaviour
{
    GameManager gameManager;
    string timeHopTagName = "TimeHop";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(timeHopTagName))
        {
            TimeHopArea timeHopArea = other.GetComponent<TimeHopArea>();
            if(timeHopArea == null)
            {
                print("timeHopArea compoment is missing in " + gameObject.name);
                return;
            }
            int value = timeHopArea.GetValue;
            gameManager.timeHopSystem.SetProgressTime(value);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(timeHopTagName))
        {
            gameManager.timeHopSystem.SetProgressTime(0);
        }
    }
}
