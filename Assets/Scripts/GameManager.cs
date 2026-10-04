using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Sirve para dar acceso desde otro script de manera mas sencilla con GameManager.Instance.monedas
    public int monedas;
    
    [SerializeField]private int coins; // Sirve para que las variables privadas se puedan ver en el inspector
    [SerializeField] private int stars;
    [SerializeField] Text _starText; // Texto esrellas
    [SerializeField] private int _currentStars; // Cantidad de estrellas
    [SerializeField] private int _maxStars; // Maximo de estrellas para la victoria



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

    public void AddStar()
    {
        stars +=1;
        _starText.text = stars.ToString() + "|" + _currentStars.ToString();
        if (stars == _maxStars)
        {
            Win();
        }
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

        CanvasManager.Instance.ChangeCanvasStatus(CanvasManager.Instance.pauseCanvas, CanvasManager.Instance.resumeButton);
    }



    public bool IsPaused() // nos devuelve verdadero o falso depende de la booleana anterior
    {
        return _isPaused;
    }

    public void Win()
    {
        //CanvasManager.Instance.ChangeCanvasStatus(CanvasManager.Instance.victoryCanvas, CanvasManager.Instance.retryButton);
    }
    
    public void GameOver()
    {

    }

}
