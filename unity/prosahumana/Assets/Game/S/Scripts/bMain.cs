using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bMain : bPersonagem
{
    #region Variáveis

    #region Variáveis Públicas
    
    public int GsIndexDesestunar {get; private set;}
    public bool GsBalasInfinitas {get => balasInfinitas; private set => balasInfinitas = value;}
    public bool GsVidasInfinitas {get => vidasInfinitas; set => vidasInfinitas = value;}

	public bool ligarDavid, podeAtualizarEstadoDavid;
    
    #endregion
    
    #region Variáveis Privadas
    
    [SerializeField] [Tooltip("O valor total da vida da personagem.")] private int numeroDeVidas;
    [SerializeField] [Tooltip("A intensidade da força de queda da personagem.")] private float indexQueda;
    [SerializeField] [Tooltip("O número de unidades que a personagem se movimentará por frame verticalmente.")] protected float forcaMovimentoVertical;
    [SerializeField] [Tooltip("Controla se o jogador tem ou não vidas infinitas.")] private bool vidasInfinitas;
    [SerializeField] [Tooltip("Controla se o jogador pode ou não não ser estunado infinitamente.")] private bool stumInfinito;
    [SerializeField] [Tooltip("Referência para as partículas que do pulo do Player.")] private ParticleSystem particulasPulo;
    [SerializeField] [Tooltip("Referência para o som de encostou no chão emitido pelo Player.")] private AudioSource somEncostouChao, somAgachar;
    [SerializeField] [Tooltip("Referência para o som de pulo emitido pelo Player.")] private AudioSource somPulo;
    [SerializeField] [Tooltip("Referência para o objeto do David.")] private GameObject david;
    [SerializeField] [Tooltip("Referência para a cabeça do Player.")] private GameObject limiteTeto; 
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private int _numeroDePulosDados, _numeroDeVidasTmp;
	/* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _podeEsconder, _podeMostrarFeedback, _podeAbaixarOuLevantar, _podeEmpurrarCaixa, _podeTomarDano, _podeAtirar;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _estaAbaixado, _estaEscondido, _estaNoChao, _estaPulando, _estaEmAreaDeEsconder, _estaMovendoCaixa;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouSomEncostouChao, _encontrouSomPulo, _encontrouSomAgachar, _encontrouParticulasPulo, _encontrouTetoEmcima, _encontrouCaixaDoLado, _encontrouDavid, _encontrouAnimatorDavid, _encontrouSpriteRendererDavid, _encontrouGerenciadorDeDialogos, _encontrouUI;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private SpriteRenderer _spriteRendererDavid;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Animator _animatorDavid;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Rigidbody2D _caixa;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Transform _checarLimiteTeto;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bDialogos _gerenciadorDeDialogos;
    private bUI _UI;
    
    #endregion
    
    #region Variáveis Constantes
    
    [Tooltip("O número de vezes que o jogador ficará vermelho quando tomar dano.")] public const int RepeticaoFeedbackDano = 6;
    [Tooltip("O multiplicador da grávida para a queda do Player.")] public const int MultiplicadorDeGravidade = 3;
    [Tooltip("O divisor de movimentação para quando o Player andar abaixado.")] public const int DivisorDeMovimentacaoAbaixado = 4;
    [Tooltip("A distância vertical máxima de verificação de teto.")] public const float AlturaDeVerificacaoDeTeto =  0.75f;
    [Tooltip("A distância horizontal máxima de verificação de caixa.")] public const float LarguraDeDeteccaoCaixa =  2.25f;
    [Tooltip("O número de unidades que a personagem se movimentará por frame verticalmente.")] public const float TemporizadorFeedbackDano = 0.15f;
    [Tooltip("O tempo que demorará para o Player tomar um novo dano enquanto estunado.")] public const float TempoDanoStum = 0.035f;
    
    #endregion

    #region Variáveis Estáticas
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private static readonly int EstaAndando = Animator.StringToHash("estaAndando");
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private static readonly int EstaNoChao = Animator.StringToHash("estaNoChao");
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private static readonly int EstaPulando = Animator.StringToHash("estaPulando");
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private static readonly int EstaAbaixado = Animator.StringToHash("estaAbaixado");
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private static readonly int EstaEstunado = Animator.StringToHash("estaEstunado");
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private static readonly int EstaAtirando = Animator.StringToHash("estaAtirando");
    private static readonly int EstaMovendoCaixa = Animator.StringToHash("estaMovendoCaixa");
    private static readonly int PodeEmpurrarCaixa = Animator.StringToHash("podeEmpurrarCaixa");

    #endregion
    
    #endregion
    
    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados

    private new void Awake()
    {
        base.Awake();
        
        _encontrouDavid = david != null;
        _animatorDavid = _encontrouDavid ? !ReferenceEquals(david.GetComponent<Animator>(), null) ? david.GetComponent<Animator>() : null : null;
        _spriteRendererDavid = _encontrouDavid ? !ReferenceEquals(david.GetComponent<SpriteRenderer>(), null) ? david.GetComponent<SpriteRenderer>() : null : null;
        _gerenciadorDeDialogos = FindObjectOfType<bDialogos>();
        _UI = FindObjectOfType<bUI>();
        
        if (!PlayerPrefs.HasKey("p_tiros") || PlayerPrefs.GetInt("p_tiros") <= 0)
            PlayerPrefs.SetInt("p_tiros", numeroDeBalas);
        
        if (!PlayerPrefs.HasKey("p_vida") || PlayerPrefs.GetInt("p_vida") <= 0)
            PlayerPrefs.SetInt("p_vida", numeroDeVidas);

    }
    private new void Start()
    {
        base.Start();

        if (PlayerPrefs.HasKey("levelIndex") && PlayerPrefs.GetInt("levelIndex") == SceneManager.GetActiveScene().buildIndex)
            transform.position = new Vector3(PlayerPrefs.GetFloat("p_x"), PlayerPrefs.GetFloat("p_y"), PlayerPrefs.GetFloat("p_z"));

        if (PlayerPrefs.HasKey("d_ligado"))
            ligarDavid = PlayerPrefs.GetInt("d_ligado") == 1;

        if (PlayerPrefs.HasKey("p_arma"))
            _podeAtirar = true;

        _encontrouAnimatorDavid = _animatorDavid != null;
        _encontrouSpriteRendererDavid = _spriteRendererDavid != null;
        encontrouColisorPrincipal = colisorPrincipal != null;
        _encontrouParticulasPulo = particulasPulo != null;
        _encontrouSomEncostouChao = somEncostouChao != null;
        _encontrouSomPulo = somPulo != null;
        _encontrouGerenciadorDeDialogos = _gerenciadorDeDialogos != null;
        _encontrouUI = _UI != null;
        _encontrouSomAgachar = somAgachar != null;
        
        _numeroDeVidasTmp = numeroDeVidas;
        _podeMostrarFeedback = _podeTomarDano = true;
        _estaMovendoCaixa = _podeAbaixarOuLevantar = _estaAbaixado = false;
        _checarLimiteTeto = limiteTeto != null ? limiteTeto.transform : transform;
    }
    private new void Update()
    {
	    base.Update();
	    
	    Debug.Log(PlayerPrefs.GetInt("d_ligado"));
	    Debug.Log(ligarDavid);
        
	    if (podeAtualizarEstadoDavid)
	    {
	    	Debug.Log("a");
	    	PlayerPrefs.SetInt("d_ligado", david.activeSelf ? 1 : 0);
	    	podeAtualizarEstadoDavid = false;
	    }

        if (ligarDavid && _encontrouDavid)
            david.SetActive(true);
        
        if (Input.GetKeyDown(KeyCode.P))
            Chetar();

        if (estaEstunado || ((encontrouLevelManager && levelManager.enabled && (levelManager.GsUI.GsEstaFade || levelManager.GsEstaEmCutscene)) || (_encontrouGerenciadorDeDialogos && _gerenciadorDeDialogos.GsEstaEmDialogo)))
        {
            ResetarValores();

            if (!estaEstunado) return;
            if (GsIndexDesestunar < 4)
            {
                if (!vidasInfinitas && _podeTomarDano)
                    TomarDano(1, Color.yellow, TempoDanoStum);

                if (Input.GetKeyDown(KeyCode.L))
                    GsIndexDesestunar += 1;
            }
            else
            {
                GsIndexDesestunar = 0;
                Desestunar();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
                _podeEsconder = true;
            
            if (Input.GetButtonDown("Jump") && !_estaMovendoCaixa && _numeroDePulosDados < 2 && !_encontrouTetoEmcima && !_estaEscondido)
                MoverVerticalmente();

            MoverHorizontalmente();
            AtualizarPosicao(posAtual * Time.deltaTime);

            if (encontrouLevelManager && _gerenciadorDeDialogos.GsEstaEmDialogo)
            {
                if (_estaAbaixado)
                {
                    if (_encontrouSomAgachar)
                    {
                        if (h != 0 && _estaNoChao && !somAgachar.loop)
                        {
                            somAgachar.loop = true;
                            somAgachar.Play();
                        }
                        else if (h == 0 || !_estaNoChao)
                        {
                            somAgachar.loop = false;
                            somAgachar.Stop();
                        }
                    }
                }
                else
                {
                    if (encontrouSomAndar)
                    {
                        if (h != 0 && _estaNoChao && !somAndar.loop)
                        {
                            somAndar.loop = true;
                            somAndar.Play();
                        }
                        else if (h == 0 || !_estaNoChao)
                        {
                            somAndar.loop = false;
                            somAndar.Stop();
                        }
                    }
                }
            }

            if (_estaEscondido) return;
            
            if (!_estaMovendoCaixa && !_estaPulando && ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))))
                _podeAbaixarOuLevantar = true;

            _podeEmpurrarCaixa = Input.GetKey(KeyCode.O);
            if (_podeEmpurrarCaixa) _estaMovendoCaixa = false;
            _podeAtirar = Input.GetKey(KeyCode.Z);
            
	        if (_podeAtirar && acaoAtirar && (numeroDeBalas > 0 || balasInfinitas) && (Time.time > tempoAtual + delayTiro))
            {
                Atirar();
                PlayerPrefs.SetInt("p_tiros", PlayerPrefs.GetInt("p_tiros") - 1);
                    
                if (encontrouAnimator)
                    animator.SetTrigger(EstaAtirando);
            }
            else
            {
                if (!encontrouAnimator) return;
                for (var x = 0; x < 2; x++)
                    animator.enabled = !animator.enabled;
            }
        }
    }
    private void FixedUpdate()
    {
        if (estaEstunado || (_encontrouGerenciadorDeDialogos && _gerenciadorDeDialogos.GsEstaEmDialogo) || (encontrouLevelManager && ((levelManager.enabled && levelManager.GsUI.GsEstaFade) || levelManager.enabled && levelManager.GsEstaEmCutscene))) return;
        
        ChecarRaycasts();
        Mover();
        Cair();

        if (_podeEsconder && ((!_estaEscondido && _estaEmAreaDeEsconder) || (_estaEscondido)))
            Esconder();

        if ((_podeAbaixarOuLevantar && _estaNoChao))
        {
            if (h > 0 && h < 1 || h < 0 && h > -1)
                h = 0;

            AbaixadoOuLevantado();
        }

        if (_estaEscondido || _estaAbaixado) return;
        if (_podeEmpurrarCaixa && (_encontrouCaixaDoLado || _estaMovendoCaixa) && Mathf.Abs(transform.position.x - _caixa.transform.position.x) <= LarguraDeDeteccaoCaixa && (Mathf.Abs(transform.position.y - _caixa.transform.position.y) <= 0.215f))
        {
            _caixa.constraints = estaAndando ? ~~RigidbodyConstraints2D.FreezeRotation : ~RigidbodyConstraints2D.FreezePositionY;
            _caixa.transform.parent = transform;
            _estaMovendoCaixa = true;
        }
        else if (_caixa != null)
        {
            _caixa.constraints = ~RigidbodyConstraints2D.FreezePositionY;
            _caixa.transform.parent = null;
            _estaMovendoCaixa = false;
        }
    }   
    
    private void OnCollisionEnter2D(Collision2D collision2d)
    {
        if (vidasInfinitas) return;

        if (collision2d.collider.CompareTag("bala") && !ReferenceEquals(collision2d.collider.GetComponent<bBala>(), null) && !collision2d.collider.CompareTag(tag) && collision2d.collider.GetComponent<bBala>().GsReferencia.name != name && _podeTomarDano)
            TomarDano(2, Color.red, TemporizadorFeedbackDano);

        if (collision2d.collider.CompareTag("inimigo"))
            TomarDano(collision2d.collider.gameObject.GetComponent<bInimigo>() != null ? collision2d.collider.gameObject.GetComponent<bInimigo>().GsDanoDado : 4, Color.red, TemporizadorFeedbackDano / 2);
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("matar"))
            TomarDano(PlayerPrefs.GetInt("p_vida"), Color.white, 0f);

        if (collider2d.CompareTag("recuperarVida"))
            RecuperarVida();
        
        if (collider2d.CompareTag("esconderijo"))
            _estaEmAreaDeEsconder = true;

        if (collider2d.CompareTag("estunar") && !estaEstunado && !stumInfinito)
            estaEstunado = true;

        if (collider2d.CompareTag("checkpoint") && _encontrouDavid)
	        podeAtualizarEstadoDavid = true;

        if (collider2d.CompareTag("arma"))
        {
            _podeAtirar = true;
            PlayerPrefs.SetInt("p_arma", 1);
            
            if (_encontrouUI)
            {
                _UI.GsPodeMostrarBala = true;
                
                if (_UI.GsEncontrouImgBala)
                    _UI.GsImgBala.gameObject.SetActive(true);
                
                if (_UI.GsEncontrouTxtNumeroDeBalas)
                    _UI.GsTxtNumeroDeBalas.gameObject.SetActive(true);
            }
        }
        
        if (collider2d.CompareTag("empurravel") && collider2d.transform.parent.GetComponent<Rigidbody2D>() != null)
        {
            _encontrouCaixaDoLado = true;
            _caixa = collider2d.transform.parent.GetComponent<Rigidbody2D>();
        }
        
        if (collider2d.CompareTag("passarFase"))
        {
            PlayerPrefs.SetInt("p_vida", _numeroDeVidasTmp);
            PlayerPrefs.SetInt("p_tiros", numeroDeBalas);
        }

        if (!collider2d.CompareTag("chao") && (!collider2d.CompareTag("empurravel"))) return;
        if (_encontrouSomEncostouChao)
            somEncostouChao.Play();

        if (_encontrouParticulasPulo)
            particulasPulo.Play();

        _estaNoChao = true;
        _estaPulando = false;
        _numeroDePulosDados = 0;
    }
    private void OnTriggerExit2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("chao"))
            _numeroDePulosDados = 1;

        if (collider2d.CompareTag("esconderijo"))
            _estaEmAreaDeEsconder = false;

        if (!collider2d.CompareTag("empurravel") || _estaMovendoCaixa) return;
        _encontrouCaixaDoLado = false;
        _caixa = null;
    }
    
    #endregion
    
    #endregion

    #region Métodos Personalizados

    #region Métodos Protegidos
    protected virtual void Desestunar()
    {
        estaEstunado = false;
        StopCoroutine(Feedback(Color.black, 0f));
    }
    protected virtual void MoverHorizontalmente()
    {
        var position = transform.position;
        h =  Input.GetAxisRaw("Horizontal") * velocidadeTmp;
        estaAndando = h != 0;
        
        if (estaAndando && _estaEscondido)
            ResetarValores();

        if (cam.WorldToViewportPoint(transform.position).x < 0 || cam.WorldToViewportPoint(transform.position).x > 1)
            transform.position = cam.WorldToViewportPoint(transform.position).x < 0 ? new Vector3(cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, position.y, position.z) : new Vector3(cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x, position.y, position.z);
        else
            posAtual.x = h;
        
    }
    protected override void AtualizarAnimacao()
    {
        animator.SetBool(EstaNoChao, _estaNoChao);
        if (_encontrouDavid && david.activeSelf && _encontrouAnimatorDavid) _animatorDavid.SetBool(EstaNoChao, _estaNoChao); 
        
        animator.SetBool(EstaEstunado, estaEstunado);
        if (_encontrouDavid && david.activeSelf && _encontrouAnimatorDavid) _animatorDavid.SetBool(EstaEstunado, estaEstunado); 
        
        animator.SetBool(EstaAndando, estaAndando);
        if (_encontrouDavid && david.activeSelf && _encontrouAnimatorDavid) _animatorDavid.SetBool(EstaAndando, estaAndando); 
        
        animator.SetBool(EstaPulando, _estaPulando);
        if (_encontrouDavid && david.activeSelf && _encontrouAnimatorDavid) _animatorDavid.SetBool(EstaPulando, _estaPulando); 
        
        animator.SetBool(EstaAbaixado, _estaAbaixado);
        if (_encontrouDavid && david.activeSelf && _encontrouAnimatorDavid) _animatorDavid.SetBool(EstaAbaixado, _estaAbaixado);
        
        animator.SetBool(EstaMovendoCaixa, _estaMovendoCaixa);
        animator.SetBool(PodeEmpurrarCaixa, _podeEmpurrarCaixa);
    }

    #endregion
    
    #region Métodos Privados
    
    private void MoverVerticalmente()
    {
        posAtual.y = _numeroDePulosDados == 0 ? forcaMovimentoVertical / (_numeroDePulosDados + 1) : _numeroDePulosDados < 2 ? forcaMovimentoVertical / (_numeroDePulosDados + (_numeroDePulosDados / 2f)) : 0;

        if (_numeroDePulosDados < 1)
        {
            if (_encontrouParticulasPulo)
                particulasPulo.Play();
            
            if (_encontrouSomPulo)
                somPulo.Play();
        }

        _estaNoChao = false;
        _estaPulando = true;
        _numeroDePulosDados++;
    }
    private void TomarDano(int dano, Color c, float t)
    {
        _numeroDeVidasTmp -= dano;
        PlayerPrefs.SetInt("p_vida", _numeroDeVidasTmp);
        
        if (encontrouSomTomarDano)
            somTomarDano.Play();
        
        if (_podeMostrarFeedback)
            StartCoroutine(Feedback(c, t));
        
        if (_numeroDeVidasTmp <= 0)
            Morrer();
    }
    private void AbaixadoOuLevantado()
    {
        switch (_estaAbaixado)
        {
            case false:
                _estaAbaixado = true;
                _podeAbaixarOuLevantar = false;
                velocidadeTmp /= DivisorDeMovimentacaoAbaixado;
                break;
            case true when !_encontrouTetoEmcima:
                _podeAbaixarOuLevantar = _estaAbaixado = false;
                velocidadeTmp = forcaMovimentoHorizontal;
                break;
        }
    }
    private void Morrer()
    {
        Destroy(gameObject);
    }
    private void Chetar()
    {
        balasInfinitas = !balasInfinitas;
        vidasInfinitas = !vidasInfinitas;
        stumInfinito = !stumInfinito;
    }
    private void Cair()
    {
        posAtual.y -= (Mathf.Abs(Physics2D.gravity.y) * MultiplicadorDeGravidade) * Time.deltaTime;
        
        if (posAtual.y < -(Mathf.Abs(Physics2D.gravity.y) * MultiplicadorDeGravidade) * Time.deltaTime * indexQueda)
            posAtual.y = -(Mathf.Abs(Physics2D.gravity.y) * MultiplicadorDeGravidade) * Time.deltaTime * indexQueda;
    }
    private void Esconder()
    {
        if (!encontrouColisorPrincipal || !encontrouSpriteRenderer) return;
        _podeEsconder = false;
        
        if (!_estaEscondido)
        {
            _estaEscondido = _estaEmAreaDeEsconder = true;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
            colisorPrincipal.enabled = spriteRenderer.enabled = _podeTomarDano = false;
            if (_encontrouSpriteRendererDavid) _spriteRendererDavid.enabled = false;
        }
        else
        {
            _estaEscondido = _estaEmAreaDeEsconder = false;
            rb2d.constraints = RigidbodyConstraints2D.None;
            colisorPrincipal.enabled = spriteRenderer.enabled = _podeTomarDano = true;
            if (_encontrouSpriteRendererDavid) _spriteRendererDavid.enabled = true;
        }
        
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void ResetarValores()
    {
        _estaPulando = estaAndando = _estaAbaixado = _estaEscondido = false;
        _estaNoChao = true;
        
        h = 0;
        velocidadeTmp = forcaMovimentoHorizontal;

        if (_encontrouDavid && _encontrouSpriteRendererDavid)
            _spriteRendererDavid.enabled = true;

        if (encontrouColisorPrincipal)
            colisorPrincipal.enabled = true;
    }
    private void RecuperarVida()
    {
        if (_numeroDeVidasTmp <= numeroDeVidas - 5)
            _numeroDeVidasTmp += 5;
        else
            _numeroDeVidasTmp += numeroDeVidas - _numeroDeVidasTmp;
        
        PlayerPrefs.SetInt("p_vida", _numeroDeVidasTmp);
    }
    private void ChecarRaycasts()
    {
        var limiteTetoPosition = _checarLimiteTeto.position;
        _encontrouTetoEmcima = (Physics2D.Raycast(limiteTetoPosition + Vector3.left * 0.35f, Vector2.up, AlturaDeVerificacaoDeTeto, ~(1 << 8)).collider != null && !Physics2D.Raycast(limiteTetoPosition + Vector3.left * 0.35f, Vector2.up, AlturaDeVerificacaoDeTeto, ~(1 << 8)).collider.CompareTag("inimigo")) || (Physics2D.Raycast(limiteTetoPosition - Vector3.left * 0.35f, Vector2.up, AlturaDeVerificacaoDeTeto, ~(1 << 8)).collider != null && !Physics2D.Raycast(limiteTetoPosition - Vector3.left * 0.35f, Vector2.up, AlturaDeVerificacaoDeTeto, ~(1 << 8)).collider.CompareTag("inimigo"));
    }

    private IEnumerator Feedback(Color c, float t)
    {
        _podeMostrarFeedback = _podeTomarDano = false;
        
        var corTmp = Color.white;
        if (encontrouSpriteRenderer)
            for (var x = 0; x < RepeticaoFeedbackDano * 2; x++)
            {
                if (corTmp == c)
                    corTmp = Color.white;
                else if (corTmp == Color.white)
                    corTmp = c;
                
                yield return new WaitForSeconds(t);
                spriteRenderer.color = corTmp;
            }

        yield return new WaitForSeconds(0.1f);
        _podeMostrarFeedback = _podeTomarDano = true;
        
        StopCoroutine(Feedback(c, t));
    }
    
    #endregion
    
    #endregion
    
    #endregion
}