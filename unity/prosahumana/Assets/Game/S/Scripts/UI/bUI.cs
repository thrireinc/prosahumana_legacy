using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bUI : MonoBehaviour
{
    #region Variáveis

    #region Variáveis Públicas

    public float GsDelayFadeOut => delayFadeOut;
    public bool GsEncontrouEffector {get; private set;}
    public bool GsEncontrouTxtCheckpoint {get; private set;}
    public bool GsEncontrouTxtNumeroDeBalas {get; private set;}
    public bool GsEncontrouImgBala {get; private set;}
    public bool GsPodeMostrarBala {get => podeMostrarBalas; set => podeMostrarBalas = value;}
    public bool GsEstaFade {get; private set;}
    public char GsDirecaoFade {get; set;}
    public Text GsTxtNumeroDeBalas {get => txtNumeroDeBalas; private set => txtNumeroDeBalas = value;}
    public Image GsImgBala {get => imgBala; private set => imgBala = value;}

    #endregion
    
    #region Variáveis Privadas

    [SerializeField] [Tooltip("Controlador de velocidade do fade in da cena.")] private float delayFadeIn;
    [SerializeField] [Tooltip("Controlador de velocidade do fade out da cena.")] private float delayFadeOut;
    [SerializeField] [Tooltip("Controlador da HUD de balas.")] private bool podeMostrarBalas;
    [SerializeField] [Tooltip("Referência para o objeto que representa o Player.")] private bMain target;
    [SerializeField] [Tooltip("Referência para a imagem da bala.")] private Image imgBala;
    [SerializeField] [Tooltip("Referência para a imagem de efeito de fade.")] private Image effector;
    [SerializeField] [Tooltip("Referência para a imagem de preenchimento da barra de vida.")] private Image preenchimentoVida;
    [SerializeField] [Tooltip("Referência para o objeto que representa o texto que mostra o número de balas.")] private Text txtNumeroDeBalas;
    [SerializeField] [Tooltip("Referência para o objeto que representa o texto que mostra salvando.")] private Text txtCheckpoint;
    [SerializeField] [Tooltip("Referência para o objeto que representa o texto que mostra o número de colecionáveis pegados.")] private Text txtColecionaveis;
    [SerializeField] [Tooltip("Referência para o objeto que representa o total de vida do jogador.")] private Slider sldrVida;
    [SerializeField] [Tooltip("Cores do gradiente que a barra de vida fará.")] private Gradient gradienteSldrVida;
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouTarget, _encontrouSldrVida, _encontrouAcoesUI, _encontrouPreenchimentoVida, _encontrouTxtColecionaveis;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _podeChamarGameover;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bLevelManager _levelManager;
    
    #endregion

    #endregion
    
    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private void Awake()
    {
        if (target == null && !ReferenceEquals(GameObject.FindWithTag("Player"), null) && !ReferenceEquals(GameObject.FindWithTag("Player").GetComponent<bMain>(), null))
            target = GameObject.FindWithTag("Player").GetComponent<bMain>();

        _levelManager = FindObjectOfType<bLevelManager>();
    }
    private void Start()
    {
        _encontrouTarget = target != null;
        GsEncontrouTxtNumeroDeBalas = txtNumeroDeBalas != null;
        _encontrouSldrVida = sldrVida != null;
        _encontrouPreenchimentoVida = preenchimentoVida != null;
        GsEncontrouTxtCheckpoint = txtCheckpoint != null;
        _encontrouAcoesUI = _levelManager != null;
        GsEncontrouImgBala = imgBala != null;
        _encontrouTxtColecionaveis = txtColecionaveis != null;
        
        GsEncontrouEffector = effector != null;

        _podeChamarGameover = true;

        GsDirecaoFade = 'i';

        if (GsEncontrouEffector)
            StartCoroutine(Fade());

        if (PlayerPrefs.HasKey("p_arma"))
        {
            txtNumeroDeBalas.gameObject.SetActive(true);
            imgBala.gameObject.SetActive(true);
            podeMostrarBalas = true;
            AtualizarNumeroBalas();
        }

        if (!PlayerPrefs.HasKey("p_vida") || !_encontrouSldrVida) return;
        sldrVida.maxValue = PlayerPrefs.GetInt("p_vida");
        sldrVida.value = sldrVida.maxValue;
    }
    private void Update()
    {
        if (!_encontrouTarget) return;

        if (GsEncontrouTxtNumeroDeBalas && GsEncontrouImgBala && podeMostrarBalas)
            AtualizarNumeroBalas();
        
        if (_encontrouSldrVida)
            AtualizarVida();
        
        if (_encontrouTxtColecionaveis)
            AtualizarColecionaveis();
    }

    #endregion
    
    #endregion
    
    #region Métodos Personalizados
    
    #region Métodos Públicos

    public IEnumerator Fade()
    {
        switch (GsDirecaoFade)
        {
            case 'i':
                GsEstaFade = true;
                effector.CrossFadeAlpha(0, delayFadeIn, false);
                yield return new WaitForSeconds(delayFadeIn);
                effector.gameObject.SetActive(false);
                GsEstaFade = false;
                break;
            
            case 'o':
                GsEstaFade = true;
                effector.gameObject.SetActive(true);
                effector.CrossFadeAlpha(0, 0, false);
                effector.CrossFadeAlpha(1, delayFadeOut, false);
                break;
        }
        StopCoroutine(Fade());
    }
    public IEnumerator FeedbackCheckpoint()
    {
        txtCheckpoint.gameObject.SetActive(true);
        for (var x = 0; x < 8; x++)
        {
            txtCheckpoint.color = new Color(txtCheckpoint.color.r, txtCheckpoint.color.g, txtCheckpoint.color.b, 0.85f);
            yield return new WaitForSeconds(0.25f);
            txtCheckpoint.color = new Color(txtCheckpoint.color.r, txtCheckpoint.color.g, txtCheckpoint.color.b, 1f);
            yield return new WaitForSeconds(0.25f);
        }
        txtCheckpoint.gameObject.SetActive(false);
        StopCoroutine(FeedbackCheckpoint());
    }

    #endregion
    
    #region Métodos Privados
    
    private void AtualizarNumeroBalas()
    {
        var b = PlayerPrefs.GetInt("p_tiros");
        txtNumeroDeBalas.text = target.GsBalasInfinitas ? "∞" : b.ToString();
    }
    private void AtualizarVida()
    {
        sldrVida.value = target.GsVidasInfinitas ? sldrVida.maxValue : PlayerPrefs.GetInt("p_vida");
        
        if (_encontrouPreenchimentoVida)
            preenchimentoVida.color = gradienteSldrVida.Evaluate(sldrVida.value / sldrVida.maxValue);

        if (PlayerPrefs.GetInt("p_vida") > 0 || !_podeChamarGameover || !_encontrouAcoesUI) return;
        _podeChamarGameover = false;
        _levelManager.Recarregar();
    }

    private void AtualizarColecionaveis()
    {
        txtColecionaveis.text = PlayerPrefs.GetInt("p_colecionavel").ToString();
    }

    #endregion
    
    #endregion
    
    #endregion
}
