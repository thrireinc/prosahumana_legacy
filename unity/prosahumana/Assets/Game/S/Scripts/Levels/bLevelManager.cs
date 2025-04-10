using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bLevelManager : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Públicas

    public bUI GsUI {get; private set;}
    public bool GsEstaEmCutscene {get; set;}

    #endregion
    
    #region Variáveis Privadas
    
    [SerializeField] [Tooltip("Offset de carregamente de cena.")] private float delayOffset;
    [SerializeField] private bool mudarGlobal;
    [SerializeField] private AudioClip musicaFundo;
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _estaPausado;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouUI, _encontrouMusicaFundo, _encontrouSomGlobal;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bMain _instanciaPlayer;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private AudioSource _somGlobal;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bChamarCheckpointColisor[] _checkpoints;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bChamarDialogoColisor[] _dialogos;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bGlobal[] _globais;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bColetavel[] _coletaveis;

    #endregion

    #endregion
    
    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private void Awake()
    {
        GsUI = FindObjectOfType<bUI>();
	    _instanciaPlayer = FindObjectOfType<bMain>();
        _checkpoints = FindObjectsOfType<bChamarCheckpointColisor>();
        _dialogos = FindObjectsOfType<bChamarDialogoColisor>();
        _globais = FindObjectsOfType<bGlobal>();
        _coletaveis = FindObjectsOfType<bColetavel>();

        if (GameObject.FindWithTag("global") != null)
            _somGlobal = GameObject.FindWithTag("global").GetComponent<AudioSource>();
    }
    private void Start()
    {
        _encontrouUI = GsUI != null;
        _encontrouMusicaFundo = musicaFundo != null;
        _encontrouSomGlobal = _somGlobal != null;

        if (GameObject.FindWithTag("arma") && PlayerPrefs.HasKey("p_arma"))
            Destroy(GameObject.FindWithTag("arma"));
        
        foreach (var checkpoint in _checkpoints)
            if (PlayerPrefs.HasKey(checkpoint.name))
                Destroy(checkpoint.gameObject);

        foreach (var dialogo in _dialogos)
            if (PlayerPrefs.HasKey(dialogo.name))
                Destroy(dialogo.gameObject);

        foreach (var coletavel in _coletaveis)
            if (PlayerPrefs.HasKey(coletavel.name))
                Destroy(coletavel.gameObject);

        foreach (var global in _globais)
            if (global.gameObject != _somGlobal.gameObject)
                Destroy(global.gameObject);

        if (!mudarGlobal || !_encontrouMusicaFundo || !_encontrouSomGlobal || _somGlobal.clip == musicaFundo) return;
        _somGlobal.clip = musicaFundo;
        _somGlobal.enabled = false;
        _somGlobal.loop = _somGlobal.enabled = true;
        _somGlobal.Play();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PausarOuDespausar();
    }

    #endregion
    
    #endregion
    
    #region Métodos Personalizados
    
    #region Métodos Públicos
    
    public void Sair()
	{
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
         #else
             Application.Quit();
         #endif
    }
    public void Salvar()
    {
        var position = _instanciaPlayer.transform.position;
        
        if (_encontrouUI && GsUI.GsEncontrouTxtCheckpoint)
            StartCoroutine(GsUI.FeedbackCheckpoint());
        
        PlayerPrefs.SetFloat("p_x", position.x);
        PlayerPrefs.SetFloat("p_y", position.y);
        PlayerPrefs.SetFloat("p_z", position.z);
        PlayerPrefs.SetInt("levelIndex", SceneManager.GetActiveScene().buildIndex);
    }
    public void Recarregar()
    {
        StartCarregarFase(5);
    }
    public void StartCarregarFase(int index)
    {
        StartCoroutine(CarregarFase(index));
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos", LoadSceneMode.Single);
    }
    public void Opcoes()
    {
        SceneManager.LoadScene("Opcoes", LoadSceneMode.Single);
    }

    #endregion
    
    #region Métodos Privados
    
    private void PausarOuDespausar()
    {
        if (_estaPausado)
        {
            _estaPausado = false;
            Time.timeScale = 1;
            SceneManager.UnloadScene("Pause");
        }
        else
        {
            _estaPausado = true;
            Time.timeScale = 0;
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        }
    }
    private IEnumerator CarregarFase(int index)
    {
        var t = 0f;

	    if (PlayerPrefs.HasKey("levelIndex") && SceneManager.GetActiveScene().buildIndex == 0)
            index = PlayerPrefs.GetInt("levelIndex");
        
        if (_encontrouUI)
        {
            t = GsUI.GsDelayFadeOut;

            if (GsUI.GsEncontrouEffector)
            {
	            GsUI.GsDirecaoFade = 'o';		 //me mate:'D
                StartCoroutine(GsUI.Fade());
            }
        }
        
        yield return new WaitForSeconds(t + delayOffset);
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
    
    #endregion
    
    #endregion
    
    #endregion
}
