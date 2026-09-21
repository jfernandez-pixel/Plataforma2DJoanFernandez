using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private int _maxHealth = 100;
    [SerializeField]private float _movementSpeed = 4.5f;
    [SerializeField]private float _JumpHeight = 2;
    
    private Rigidbody2D _rigidbody2D;

    private InputAction _attackAction;
    private InputAction _jumpAction;
    private InputAction _moveAction; 
    private Vector2 _moveInput;
    private Animator _animator;

    [SerializeField]private Transform _groundSensor;
    [SerializeField]private float _sensorSize = 1;
    [SerializeField]private LayerMask _groundLayer;
    [SerializeField]private int _attackDamage = 7;
    [SerializeField]private Transform _attackHitbox;
    [SerializeField]private float _hitBoxRadius = 1f;

    void Awake() //Hace lo mismo que el start pero se ejecuta antes
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>(); //Rotar el personaje cuando gira

        if(_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRunning", true);
        }
        else if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler (0, 0, 0);
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }


        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        if(_attackAction.WasPressedThisFrame() && IsGrounded())
        {
            Attack();
        }

        _animator.SetBool("IsJumping", !IsGrounded());
    }

    void FixedUpdate() //Se ejecuta el mismo numero de veces x segundo
    {
                _rigidbody2D.linearVelocity = new Vector2(_moveInput.x * _movementSpeed, _rigidbody2D.linearVelocity.y);
    }

    void Jump() //Funcion de salto donde se llama al rigidbody pq es qn tiene las fisicas y se le anyade una fuerza up hacia donde la envia
    {
        _rigidbody2D.AddForce(Vector2.up * Mathf.Sqrt(_JumpHeight * -2 * Physics2D.gravity.y), ForceMode2D.Impulse); //
    }

    void Attack()
    {
        _animator.SetTrigger("IsAttacking");
        
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_attackHitbox.position, _hitBoxRadius);

        foreach(Collider2D enemy in colliders2D)
        {
            if(enemy.gameObject.layer == 7)
            {
                mimik enemyScirpt = enemy.GetComponent<mimik>();
                enemyScirpt.TakeDamage(_attackDamage);
            }
        }

    }

    bool IsGrounded() // El bool es para que la funcion devuelva algo (verdadero o falso)
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_groundSensor.position, _sensorSize);

        foreach(Collider2D item in colliders2D)
        {
            if(item.gameObject.layer == 6)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundSensor.position, _sensorSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackHitbox.position, _hitBoxRadius);
    }
}
