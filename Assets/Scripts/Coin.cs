using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource _coinAduioSource;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    [SerializeField]private AudioClip _coinAudio;
   
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
            
            GameManager.Instance.AddCoins(); // Llama a la funcion de crear monedas del GameManager
            PlaySFX();//Suena el sonido 
            _spriteRenderer.enabled = false; // Desactiva el sprite render para que no se vea en pantalla
            _collider.enabled = false; // Desactiva el collider
            Destroy(gameObject, 0.5f); //0.5f es que tarde 0.5 segundos en destruir el objeto para que asi suene 
        }
    }
}
