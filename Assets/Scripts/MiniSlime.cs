using UnityEngine;

public class MiniSlime : Entities
{
    [SerializeField] float alertRadius;
    [SerializeField] float attentionSpan = 5f;
    [SerializeField] float sightRadius = 5f;
    [SerializeField] float idleTime = 2f;
    [SerializeField] Transform spawnCorePos;
    [SerializeField] float walkRadius = 5f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 3f;
    //[SerializeField] float ChargeSpeed = 6f;
    [SerializeField] Rigidbody2D rb;
    Transform playerPos;

    private Vector3 targetWayPoint;
    private Vector3 startPoint;
    private float idleTimer;
    private float attentionTimer;
    private bool isWalking = false;
    private bool isFacingRight = true;

    Character player;

    public override void TakeDamage(float damage, bool fatal = true)
    {
        base.TakeDamage(damage);
        if (currentState == State.Idle)
        {
            currentState = State.Attack;
        }
    }

    public override void Death()
    {
        Destroy(gameObject);
    }


    void Start()
    {
        startPoint = spawnCorePos != null ? spawnCorePos.position : transform.position;
        player = GameManager.Instance.character;
        playerPos = player.transform;
    }

    public void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                {
                    DetectPlayer();
                    IdleMode();
                }
                break;
            case State.Attack:
                {
                    AttackMode();
                }
                break;
        }

    }

    public void IdleMode()
    {
        if (isWalking)
        {
            Vector3 direction = targetWayPoint - transform.position;
            if (direction.x > 0)
            {
                MoveRight(walkSpeed);
            }
            else if (direction.x < 0)
            {
                MoveLeft(walkSpeed);
            }


            if (GetWayDistance(targetWayPoint) < .1f)
            {
                isWalking = false;
                idleTimer = idleTime;
            }
        }
        else //is not walking
        {
            StopMoving();
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                GetNewRandomTarget();
            }
        }
    }

    void DetectPlayer()
    {
        if (playerPos == null) return;
        if(player.isProtect()) return;

        float distanceToPlayer = GetWayDistance(playerPos.position);
        if (distanceToPlayer < alertRadius)
        {
            currentState = State.Attack;
            attentionTimer = attentionSpan;
        }
    }

    public void AttackMode()
    {

        if (playerPos == null) return;

        float distanceToPlayer = GetWayDistance(playerPos.position);

        Vector3 direction = playerPos.position - transform.position;
        if (distanceToPlayer > 0.3f)
        {
            if (direction.x > 0)
            {
                MoveRight(runSpeed);
            }
            else if (direction.x < 0)
            {
                MoveLeft(runSpeed);
            }
        }


        //Lost sight player
        if (distanceToPlayer >= sightRadius)
        {
            attentionTimer -= Time.deltaTime;

            if (attentionTimer <= 0)
            {
                currentState = State.Idle;
                GetNewRandomTarget();
            }
        }
        else
        {
            attentionTimer = attentionSpan;
        }

        //Player death
        bool stopChase = player.isProtect();
        if (stopChase)
        {
            currentState = State.Idle;
            GetNewRandomTarget();
        }
    }

    private void GetNewRandomTarget()
    {
        Vector3 center = startPoint;

        // สุ่มพิกัด X และ Z (ถ้าเป็นเกม 2D ให้เปลี่ยนจาก Z เป็น Y แทน)
        float randomX = Random.Range(-walkRadius, walkRadius);

        targetWayPoint = new Vector3(center.x + randomX, 0, 0);
        isWalking = true;
        idleTimer = idleTime;
    }

    float GetWayDistance(Vector3 target)
    {
        Vector3 a = new Vector3(gameObject.transform.position.x, 0, 0);
        Vector3 b = new Vector3(target.x, 0, 0);

        return Vector3.Distance(a, b);
    }

    void MoveLeft(float speed)
    {
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        isFacingRight = false;
        FlipCharacter();
    }

    void MoveRight(float speed)
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        isFacingRight = true;
        FlipCharacter();
    }
    void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void FlipCharacter()
    {
        if (isFacingRight && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (!isFacingRight && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        //contact Damage
        if (other.CompareTag(PlayerTag))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackPower);
            }
        }
    }

}
