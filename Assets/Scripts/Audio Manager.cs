using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Primer paso crear 
    private AudioSource _audioSource;
    [SerializeField]private AudioClip _level1Soundtrack; // AduioClip crea audios

    void Awake()
    {
        if(Instance != null && Instance != this) // Segundo paso, si tienes dos game manager en la escena se destruira --Comprueba si hay alguien que se ha asingado esto
        {
            Destroy (gameObject);
        }
        else 
        {
            Instance = this;
        }

        _audioSource = GetComponent<AudioSource>(); 
    }

    //Funcion creada para que reproduca el sonido / la inicia
    public void StartSoundtrack()
    {
        _audioSource.clip = _level1Soundtrack; //Nos rellena el audio generator para poner la musica
        _audioSource.Play();
    }
    //Funcion creada para parar la musica
    public void PasueSoundtrack()
    {
        _audioSource.Pause();
    }
}
