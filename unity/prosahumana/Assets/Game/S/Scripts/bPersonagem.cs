using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class bPersonagem : MonoBehaviour
{
    #region Variáveis

    #region Variáveis Protegidas
    
    [SerializeField] [Tooltip("O número de balas disponíveis para a personagem atirar.")] protected int numeroDeBalas;
    [SerializeField] [Tooltip("O delay entre tiros.")] protected float delayTiro;
    [SerializeField] [Tooltip("O número de unidades que a personagem se movimentará por frame horizontalmente.")] protected float forcaMovimentoHorizontal;
    [SerializeField] [Tooltip("Controlador que define se a quantidade de balas da personagem é ou não infinita")] protected bool acaoAtirar;
    [SerializeField] [Tooltip("Controlador que define se a quantidade de balas da personagem é ou não infinita")] protected bool balasInfinitas;
    [SerializeField] [Tooltip("Referência para o colisor principal da personagem")] protected Collider2D colisorPrincipal;
    [SerializeField] [Tooltip("Referência para o som de tomar dano emitido pela personagem.")] protected AudioSource somTomarDano;
    [SerializeField] [Tooltip("Referência para o som de andar emitido pela personagem.")] protected AudioSource somAndar;
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected int direcao;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected float h, velocidadeTmp, tempoAtual;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected bool estaEstunado, estaAndando;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected bool encontrouAnimator, encontrouBala, encontrouSpriteRenderer, encontrouColisorPrincipal, encontrouSomTomarDano, encontrouSomAndar, encontrouLevelManager;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected bLevelManager levelManager;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected Vector3 posAtual;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected Rigidbody2D rb2d;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected Animator animator;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected SpriteRenderer spriteRenderer;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected Camera cam;
    
    #endregion
    
    #region Variáveis Privadas
    
    [SerializeField] [Tooltip("Referência para o objeto que representa a bala.")] private bBala bala;
    [SerializeField] [Tooltip("Referências sons emitidos pela personagem.")] private AudioSource somAtirar;
    [SerializeField] [Tooltip("Referência para o(s) objeto(s) que representa(m) a(s) arma(s).")] private GameObject[] armas;
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouSomAtirar;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Vector2 _proximoMovimento;

    #endregion

    #endregion

    #region Métodos
    
    #region Métodos da Unity

    #region Métodos Protegidos
    
    protected void Awake()
    {
        spriteRenderer = !ReferenceEquals(GetComponent<SpriteRenderer>(), null) ? GetComponent<SpriteRenderer>() : spriteRenderer;
        rb2d = !ReferenceEquals(GetComponent<Rigidbody2D>(), null) ? GetComponent<Rigidbody2D>() : rb2d;
        animator = !ReferenceEquals(GetComponent<Animator>(), null) ? GetComponent<Animator>() : animator;
        levelManager = FindObjectOfType<bLevelManager>();

        cam = Camera.main;
    }
    protected void Start()
    {
        encontrouBala = bala != null;
        encontrouAnimator = animator != null;
        encontrouSpriteRenderer = spriteRenderer != null;
        encontrouSomTomarDano = somTomarDano != null;
        encontrouSomAndar = somAndar != null;
        encontrouLevelManager = levelManager != null;
        _encontrouSomAtirar = somAtirar != null;

        h = 0;
        direcao = 1;
        posAtual = transform.position;
        velocidadeTmp = forcaMovimentoHorizontal;
        tempoAtual = Time.time;
    }
    protected void Update()
    {
        if (encontrouAnimator)
            AtualizarAnimacao();
    }

    #endregion

    #endregion
    
    #region Métodos Personalizados
    
    #region Métodos Protegidos

    protected abstract void AtualizarAnimacao();
    
    protected void AtualizarPosicao(Vector2 movimento)
    {
        _proximoMovimento += movimento;
    }
    protected void Mover()
    {
        var localScale = transform.localScale;
        var spriteRendererEnabled = spriteRenderer.enabled;
        
        spriteRenderer.enabled = h != 0 && encontrouSpriteRenderer && !spriteRendererEnabled || spriteRendererEnabled;
        direcao = h > 0 ? 1 : h < 0 ? -1 : direcao;
        transform.localScale = new Vector3(direcao, localScale.y, localScale.z);
        rb2d.MovePosition(rb2d.position + _proximoMovimento);
        _proximoMovimento = Vector2.zero;

    }
    protected void Atirar()
    {
        if (encontrouBala)
            foreach (var t in armas)
            {
                var instanciaBala = Instantiate(bala);
                var instanciaBalaTransform = instanciaBala.transform;
                
                instanciaBalaTransform.position = t.transform.position;
                instanciaBalaTransform.rotation = Quaternion.identity;
                
                instanciaBala.GetComponent<bBala>().GsReferencia = gameObject.transform;
            }
        
        if (!balasInfinitas)
            numeroDeBalas--;
        
        if (_encontrouSomAtirar)
            somAtirar.Play();
        
        tempoAtual = Time.time;
    }
    
    #endregion
    
    #endregion
    
    #endregion
}
