using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    [SerializeField] private float lifeSpanTime = 1;
    // Update is called once per frame
    void Update()
    {
        lifeSpanTime = Mathf.MoveTowards(lifeSpanTime,0,Time.deltaTime);

        if(lifeSpanTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
