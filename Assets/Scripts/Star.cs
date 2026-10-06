using UnityEngine;

public class Star : MonoBehaviour
{
    private AudioSource _starAduioSource;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    public int value; // Variable contador
    [SerializeField]private AudioClip _starAudio;
   
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
            
            GameManager.Instance.AddStar(); // Llama a la funcion de crear monedas del GameManager
            PlaySFX();//Suena el sonido 
            _spriteRenderer.enabled = false; // Desactiva el sprite render para que no se vea en pantalla
            _collider.enabled = false; // Desactiva el collider
            Destroy(gameObject, 0.5f); //0.5f es que tarde 0.5 segundos en destruir el objeto para que asi suene 
            CoinCounter.instance.IncreaseCoins(value); // Valor para dar valor a contar
        }
    }
}
