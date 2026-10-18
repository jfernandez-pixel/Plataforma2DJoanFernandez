using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource _coinAduioSource;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    [SerializeField]private AudioClip _coinAudio;
    public int value; // Variable contador
   
    void Awake()
    {
        _coinAduioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    void PlaySFX()
    {
        _coinAduioSource.PlayOneShot(_coinAudio);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            CoinCounter.instance.IncreaseCoins(value);         
        }
    }
}
