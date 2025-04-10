using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bMenuLoad : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Privadas

    [SerializeField] private int index;
    [SerializeField] [Tooltip("Controlador do tempo que o load demorará.")] private float tempoLoad;
    [SerializeField] [Tooltip("Controlador da troca de fase.")] private bool apertarBotao;
    [SerializeField] [Tooltip("Referência para a imagem de load.")] private GameObject iconeLoad;
    [SerializeField] [Tooltip("Referência para o texto de passar de fase.")] private GameObject txtApertarTecla;

    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouIconeLoad, _encontrouAnimatorIconeLoad, _encontrouTxtApertarTecla;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _podeAcabarLoad, _podePassarFase;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Animator _animatorIconeLoad;

    #endregion
    
    #endregion
    
    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private void Awake()
    {
        if (ReferenceEquals(iconeLoad, null)) return;
        _encontrouIconeLoad = true;
        _animatorIconeLoad = ReferenceEquals(iconeLoad.GetComponent<Animator>(), null) ? iconeLoad.GetComponent<Animator>() : _animatorIconeLoad;
    }
    private void Start()
    {
        _encontrouAnimatorIconeLoad = _animatorIconeLoad != null;
        _encontrouTxtApertarTecla = txtApertarTecla != null;
        
        StartCoroutine(CarregarLoad());
    }
    private void Update()
    {
        PassarLoad();
    }

    #endregion
    
    #endregion
    
    #region Métodos Personalizados
    
    #region Métodos Privados
    
    private void PassarLoad()
    {
        if (_podeAcabarLoad)
        {
            if (apertarBotao)
            {
                if (_encontrouIconeLoad)
                    iconeLoad.SetActive(false);

                if (_encontrouTxtApertarTecla)
                    txtApertarTecla.SetActive(true);

                if (Input.anyKey)
                    _podePassarFase = true;
            }
            else
                _podePassarFase = true;
        }

        if (!_podePassarFase) return;
        var i = PlayerPrefs.HasKey("levelIndex") ? PlayerPrefs.GetInt("levelIndex") : index;
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }
    private IEnumerator CarregarLoad()
    {
        var t = _encontrouAnimatorIconeLoad ? tempoLoad * _animatorIconeLoad.speed : tempoLoad;
        yield return new WaitForSeconds(t);
        _podeAcabarLoad = true;
        StopCoroutine(CarregarLoad());
    }

    #endregion
    
    #endregion
    
    #endregion
}
