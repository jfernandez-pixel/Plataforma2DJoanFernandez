using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Sirve para dar acceso desde otro script de manera mas sencilla con GameManager.Instance.monedas
    public int monedas;
    [SerializeField]private int coins; // Sirve para que las variables privadas se puedan ver en el inspector

    private bool _isPaused = false;

    void Awake()
    {
        if(Instance != null && Instance != this) // Si tienes dos game manager en la escena se destruira --Comprueba si hay alguien que se ha asingado esto
        {
            Destroy (gameObject);
        }
        else 
        {
            Instance = this;
        }
    }

    void Start()
    {
        AudioManager.Instance.StartSoundtrack();
    }

    public void AddCoins() // Funcion para llamar a las monedas
    {
        coins += 1;
    }

    public void Pause() // Para controlar el cuadno parar el tiempo y asi lgo poder poner el menu de pausa
    {
        if(_isPaused) //Si la funcion is paused
        {
            _isPaused = false; // No esta pausado
            Time.timeScale = 1; // Reanuda el tiempo
            AudioManager.Instance.StartSoundtrack(); // Inicia la musica 
        }
        else // y sino 
        {
            _isPaused = true; //Esta pausado
            AudioManager.Instance.PasueSoundtrack(); // Pausa la musica
            Time.timeScale = 0; // Para el tiempo, lo congela
        }
    }

    public bool IsPaused() // nos devuelve verdadero o falso depende de la booleana anterior
    {
        return _isPaused;
    }
}
