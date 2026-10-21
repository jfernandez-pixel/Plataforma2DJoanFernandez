using TMPro;
using UnityEngine;

public class mimik : MonoBehaviour
{
    
    [SerializeField]private int _maxHealth = 20;
    [SerializeField]private int _actualHealth;
    [SerializeField]private float _detectionRange = 2.5f;// Rango de deteccion para la animacion
    [SerializeField]private int _attackDamage = 10; //Daño de ataque
    private Animator _animator;
    private Transform _playerTransform; //Ref posicion del player
    

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        _actualHealth = _maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (_playerTransform == null) return; //Si no esta el jugador en la escena, no hace nada 
        // Calculamos la distancia constante entre el Mimik y el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer <= _detectionRange)
        {
            _animator.SetTrigger("IsAttacking");// Activa un bool en el animator
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController _playerDamage = collision.gameObject.GetComponent<PlayerController>();
            
            if(_playerDamage != null)
            {
            _animator.SetTrigger("IsAttacking");
            _playerDamage.TakeDamage(_attackDamage);
            }
        }     
    }


    public void TakeDamage(int damage) //Funcion para restar vida
    {
        _actualHealth -= damage;

        if(_actualHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
