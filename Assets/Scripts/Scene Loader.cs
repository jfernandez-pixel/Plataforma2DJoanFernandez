using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _loadingBar;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }


   /* public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }*/

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string sceneName) // Sirve para poder pausar a mitad de del codigo y lgo se reanude, sirve para contadores

    {
        yield return null;
        _loadingCanvas.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        float fakeLoadPercentage = 0.01f;

        while(!asyncLoad.isDone) 
        {
            _loadingBar.fillAmount = asyncLoad.progress; //vida de carga real

            fakeLoadPercentage += 0.01f;
            Mathf.Clamp01(fakeLoadPercentage);

            _loadingBar.fillAmount = fakeLoadPercentage;

            if(asyncLoad.progress >= 0.9f && fakeLoadPercentage >= 0.99f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }

        Time.timeScale = 1;
        _loadingCanvas.SetActive(false);
    }

    public void GameOVer(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
