using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour, IDamageable
{
    private PlayerControl inputActions;
    private Health health;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private SO_Myattack myAttack;
    [SerializeField] private float coolDownNextAttack = 0f;
    [SerializeField] private Inventory myInventory;
    [SerializeField] private Transform attackPoint;
    private bool isFacingRight = true;

    [SerializeField] private Transform groundCheckPos;
    private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float baseGravityScale = 1f;
    [SerializeField] private float jumpGravity = 2f;
    [SerializeField] private float fallGravity = 5f;
    [SerializeField] private float hangThreShold = .5f;

    private float IFrameTime = 1.5f;
    private float IFrameTimer;

    private float protectTime = 1f;
    private float protectTimer;



    public Transform AttackPoint => attackPoint;

    public event Action OnDeath;


    void OnEnable()
    {
        inputActions.Enable();
    }
    void OnDisable()
    {
        inputActions.Disable();
    }

    void Awake()
    {
        inputActions = new PlayerControl();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        inputActions.Enable();
    }


    void Update()
    {
        // print("Is Grounded: " + isGround());
        coolDownNextAttack = Mathf.MoveTowards(coolDownNextAttack, 0f, Time.deltaTime);
        IFrameTimer = Mathf.MoveTowards(IFrameTimer, 0f, Time.deltaTime);
        protectTimer = Mathf.MoveTowards(protectTimer, 0f, Time.deltaTime);

        Move();
        Jump();
        if (inputActions.Player.Attack.WasPressedThisFrame())
        {
            PerformAttack();
        }
        GravityControl();
    }

    bool isGround()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);
    }

    bool isIFrame()
    {
        return IFrameTimer > 0;
    }

    public bool isProtect()
    {
        return protectTimer > 0;
    }

    public void Move()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        if (moveInput.x < 0) // Move left
        {
            rb.linearVelocity = new Vector2(-movementSpeed, rb.linearVelocity.y);
            isFacingRight = false;
            FlipCharacter();
        }
        else if (moveInput.x > 0) // Move right
        {
            rb.linearVelocity = new Vector2(movementSpeed, rb.linearVelocity.y);
            isFacingRight = true;
            FlipCharacter();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

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

    public void Jump()
    {
        if (inputActions.Player.Jump.WasPressedThisFrame() && isGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // print("Jump");
        }
    }
    public void TakeDamage(float damage, bool fatal = true)
    {
        if (health == null)
        {
            print("Health component is missing.");
            return;
        }

        //case if self-damaage
        if (!fatal)
        {
            if (health.CurrentHealth - damage <= 1)
            {
                float newDamage = health.CurrentHealth - 1;
                if (newDamage < 0)
                {
                    newDamage = 0;
                }
                health?.ReduceHealth(newDamage);
            }
            else
            {
                health?.ReduceHealth(damage);
            }
            return;
        }

        //default case
        else if (fatal)
        {
            if (isIFrame()) return;

            health?.ReduceHealth(damage);
            GetIFrame();

            if (health.IsDead())
            {
                Death();
            }
        }
    }

    void GetIFrame()
    {
        IFrameTimer = IFrameTime;
    }

    public void GetProtect()
    {
        protectTimer = protectTime;
    }

    public void PerformAttack()
    {
        if (coolDownNextAttack > 0f) return;
        myAttack?.OnAttack(this);
    }

    public void SetAttackCooldown(float amount)
    {
        coolDownNextAttack = amount;
    }

    void Death()
    {
        OnDeath?.Invoke();

    }


    public void GravityControl()
    {
        if (isGround())
        {
            rb.gravityScale = baseGravityScale;
            return;
        }
        //Midair
        if (Mathf.Abs(rb.linearVelocity.y) < hangThreShold)
        {
            rb.gravityScale = jumpGravity;
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallGravity;
        }
        else
        {
            rb.gravityScale = baseGravityScale;
        }
    }
}
