using UnityEngine;

public class Star : MonoBehaviour
{
    private AudioSource _starAduioSource;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    [SerializeField]private AudioClip _starAudio;
    public int value; // Variable contador
   
    void Awake()
    {
        _starAduioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    void PlaySFX()
    {
        _starAduioSource.PlayOneShot(_starAudio);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            StarCounter.instance.IncreaseStars(value);
        }
    }
}
