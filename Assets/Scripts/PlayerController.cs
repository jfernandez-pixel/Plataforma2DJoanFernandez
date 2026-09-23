using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private int _maxHealth = 100;
    [SerializeField]private int _actualHealth;
    [SerializeField]private float _movementSpeed = 4.5f;
    [SerializeField]private float _JumpHeight = 2;
    
    private Rigidbody2D _rigidbody2D;

    //Creamos variable para las acciones del input system
    private InputAction _pauseAction; 
    private InputAction _attackAction;
    private InputAction _jumpAction;
    private InputAction _moveAction; 
    private Vector2 _moveInput;
    private Animator _animator;
    private AudioSource _playerAudioSource;


    [SerializeField]private Transform _groundSensor;
    [SerializeField]private float _sensorSize = 1;
    [SerializeField]private LayerMask _groundLayer;
    [SerializeField]private int _attackDamage = 7;
    [SerializeField]private Transform _attackHitbox;
    [SerializeField]private float _hitBoxRadius = 1f;
    [SerializeField]private AudioClip _jumpSound;
    [SerializeField]private AudioClip _attackSound;

    void Awake() //Hace lo mismo que el start pero se ejecuta antes
    {
        //Detecta el RigidBody y el animator 
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>(); 


        //Detectar botones del InputSystem
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];
        _pauseAction = InputSystem.actions["Pause"];

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //_acutalHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(_pauseAction.WasPressedThisFrame()) //para el tiempo se pone arriba para que no ejecute lo de abajo 
        {
            GameManager.Instance.Pause(); 
        }

        if(GameManager.Instance.IsPaused())
        {
            return; // Si se cumple la condicion de arriba para el codigo y no ejecuta lo de abajo 
        }
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


        if(_jumpAction.WasPressedThisFrame() && IsGrounded()) //Sirve para detectar el boton de saltar y saltar mientras no toque el suelo 
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
        PlaySFX(_jumpSound); // Reproduce una sola vez en sonido y con el play one shot lo dispara 50 veces sin cortar otros audios 
    }

    void Attack() // Funcion de ataque
    {
        _animator.SetTrigger("IsAttacking");
        PlaySFX(_attackSound); // Reproduce una sola vez en sonido y con el play one shot lo dispara 50 veces sin cortar otros audios 

        
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_attackHitbox.position, _hitBoxRadius);// Añade el radio de la hitbox

        foreach(Collider2D enemy in colliders2D) // Dice lo que pasara si tocan los colliders del personaje y enemigo
        {
            if(enemy.gameObject.layer == 7)
            {
                mimik enemyScirpt = enemy.GetComponent<mimik>();
                enemyScirpt.TakeDamage(_attackDamage); // Resta el daño al enemigo
            }
        }

    }

    public void AddHealth()
    {
        if(_actualHealth <= _maxHealth)
        {
            _actualHealth += 10;
        }
        else if(_actualHealth == _maxHealth)
        {
            _actualHealth += 0;
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

    void PlaySFX(AudioClip clip) // Funcion para llamar a los sonidos  PlayerSFX(sonido que queremos que suene)
    {
        _playerAudioSource.PlayOneShot(clip);//Lanza el sonido (clip)= es para que pongamos el audio ^^^^^^^^^^
        
    }

    void OnDrawGizmos() // Sirve para dibujar los gizmos y asi poder visualizarlos en pantalla
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundSensor.position, _sensorSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackHitbox.position, _hitBoxRadius);
    }
}
