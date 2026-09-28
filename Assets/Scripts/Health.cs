using UnityEngine;

public class Health : MonoBehaviour
{

    private AudioSource _healthAduioSource;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    [SerializeField]private int _healthAmount = 10;
    [SerializeField]private AudioClip _healthAudio;
    
    
    void Awake()
    {
        _healthAduioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    void PlaySFX()
    {
        _healthAduioSource.PlayOneShot(_healthAudio);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController HealthScirpt = collision.GetComponent<PlayerController>(); //_playerScript
            HealthScirpt.AddHealth(_healthAmount);
            PlaySFX();
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
    
}