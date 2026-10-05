using TMPro;
using UnityEngine;

public class mimik : MonoBehaviour
{
    
    [SerializeField]private int _maxHealth = 20;
    [SerializeField]private int _actualHealth;
    private Animator _animator;
    

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        _actualHealth = _maxHealth;
    }

    /*void OisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController _playerDamage = collision.gameObject.GetComponent<PlayerController>();
            _animator.SetTrigger("IsAttacking");
            _playerDamage.TakeDamage(20);
        }     
    }*/


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
