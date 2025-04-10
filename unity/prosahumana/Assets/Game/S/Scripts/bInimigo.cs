using UnityEngine;
using Random = UnityEngine.Random;

public class bInimigo : bPersonagem
{
    #region Variáveis

    #region Variáveis Públicas
    
    public int GsDanoDado {get => danoDado; set => danoDado = value;}
    
    #endregion
    
    #region Variáveis Protegidas
    [SerializeField] [Tooltip("O dano que o inimigo dará caso o Player encoste nele.")] protected int danoDado;
    [SerializeField] [Tooltip("O som de dano que o inimigo dá")] protected AudioSource somDano;

    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected int tmpNumeroDeBalas;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected bool encontrouPlayer, encontrouSomDano;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected bool estaEstunando, estaAtacando;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ protected bMain player;
    
    #endregion
    
    #region Variáveis Privadas
    
    [SerializeField] [Tooltip("A direção inicial da personagem")] private int direcaoInicial;
    [SerializeField] [Tooltip("A distância máxima que o inimigo enxergará para cada lado.")] private float campoDeVisaoNumero;
    [SerializeField] [Tooltip("A distância máxima que o inimigo irá para cada lado enquanto rondar.")] protected float limiteRonda;
    [SerializeField] [Tooltip("O tempo que o inimigo ficará estunado a cada vez que isso acontencer.")] private float tempoStum;
    [SerializeField] [Tooltip("O delay entre aumentos de velocidade")] private float delayAumentarVelocidade;
    [SerializeField] private float indexAumentoVelocidade;
    [SerializeField] [Tooltip("Variável que controla se o inimigo pode ou não rondar.")] private bool podeRondar;
    [SerializeField] [Tooltip("Variável que controla se o inimigo pode ou não seguir o Player.")] private bool podeSeguir;
    [SerializeField] [Tooltip("Variável que controla se o inimigo pode ou não aumentar sua velocidade.")] private bool podeAumentarVelocidade;
	[SerializeField] private bool utilizarPosicaoInicial;

    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private int[] direcoes = {-1, 1};
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Vector3 _posInicial;
    private bool _encontrouJogadorDoLado;
    
    #endregion
    
    #region Variáveis Estáticas
    
    private static readonly int EstaEstunado = Animator.StringToHash("estaEstunado");
    private static readonly int EstaAndando = Animator.StringToHash("estaAndando");
	private static readonly int EstaAtacando = Animator.StringToHash("estaAtacando"); //cu

    #endregion

    #endregion

    #region Métodos
    
    #region Métodos da Unity

    #region Métodos Protegidos
    
    protected new void Start()
    {
        base.Start();
        encontrouPlayer = player != null;
        encontrouSomDano = somDano != null;

        estaEstunando = false;
        
        h = direcaoInicial > 0 ? 1 : direcaoInicial < 0 ? -1 : podeRondar ? direcoes[Random.Range(0, direcoes.Length)] : 0;
        direcaoInicial = (int) h;
        tmpNumeroDeBalas = numeroDeBalas;
        _posInicial = transform.position;
    }
    protected new void Update()
    {
        base.Update();

        if (estaEstunado || estaEstunando)
        {
            h = 0;
            
            if (encontrouSomDano && !somDano.loop)
            {
                somDano.Play();
                somDano.loop = true;
            }
            
            if (!estaEstunado) return;
            
            if (encontrouSomDano)
            {
                somDano.Stop();
                somDano.loop = false;
            }

            Desestunar();
        }
        else if (!estaAtacando)
        {
            if (encontrouSomAndar && !somAndar.loop)
            {
                somAndar.loop = true;
                somAndar.Play();
            }

            MoverHorizontalmente();
        }
        else if (encontrouSomAndar)
        {
            somAndar.loop = false;
            somAndar.Stop();
        }

    }

    protected void OnCollisionEnter2D(Collision2D collision2d)
    {
        if (!collision2d.collider.CompareTag("bala") || estaEstunado) return;
        tempoAtual = Time.time;
        estaEstunado = true;
    }
    
    #endregion
    
    #region Métodos Privados

    private new void Awake()
    {
        base.Awake();
        player = !ReferenceEquals(GameObject.FindWithTag("Player").GetComponent<bMain>(), null) ? GameObject.FindWithTag("Player").GetComponent<bMain>() : player;
    }
    private void FixedUpdate()
    {
        Mover();
    }
    
    #endregion
    
    #endregion
    
    #region Métodos Personalizados

    #region Métodos Protegidos
    
    protected virtual void Desestunar()
    {
        if (!(Time.time >= tempoAtual + tempoStum)) return;
        
        if (encontrouSpriteRenderer)
            spriteRenderer.color = Color.white;

        estaEstunado = false;
        h = direcao;
    }
    protected virtual void MoverHorizontalmente()
    {
        _encontrouJogadorDoLado = utilizarPosicaoInicial ? (_posInicial.x > player.transform.position.x + 0.65f && _posInicial.x < player.transform.position.x + campoDeVisaoNumero) || (_posInicial.x < player.transform.position.x - 0.65f && _posInicial.x > player.transform.position.x - campoDeVisaoNumero)  : (transform.position.x > player.transform.position.x + 0.65f && transform.position.x < player.transform.position.x + campoDeVisaoNumero) || (transform.position.x < player.transform.position.x - 0.65f && transform.position.x > player.transform.position.x - campoDeVisaoNumero); 
        if (podeSeguir && _encontrouJogadorDoLado)
        {
            estaAndando = true;
            Seguir();
        }
        else if (podeRondar)
        {
            estaAndando = true;
            Rondar();
        }
        else
        {
            h = 0;
            estaAndando = false;
        }
        
        
        AtualizarPosicao(new Vector2(h * velocidadeTmp, 0) * Time.deltaTime);
    }
    protected override void AtualizarAnimacao()
    {
        animator.SetBool(EstaEstunado, estaEstunado);
        animator.SetBool(EstaAndando, estaAndando);
        animator.SetBool(EstaAtacando, estaAtacando);
    }
    
    #endregion

    #region Métodos Privados

    private void Rondar()
    {
        if (transform.position.x <= _posInicial.x - limiteRonda)
            h = 1;
        else if (transform.position.x >= _posInicial.x + limiteRonda)
            h = -1;
    }
    private void Seguir()
    {
        if (!encontrouPlayer) return;
        h = player.transform.position.x > transform.position.x ? 1 : -1;

        if (!(Time.time >= tempoAtual + delayAumentarVelocidade) || !podeAumentarVelocidade) return;
        tempoAtual = Time.time;
        velocidadeTmp += indexAumentoVelocidade;
    }
    
    #endregion
    
    #endregion
    
    #endregion
}
