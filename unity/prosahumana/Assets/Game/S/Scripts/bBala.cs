using UnityEngine;

public class bBala : MonoBehaviour
{
	#region Variáveis
	
	#region Variáveis Públicas
	public Transform GsReferencia {get => referencia; set => referencia = value;}
	#endregion

	#region Variáveis Privadas
	[SerializeField] [Tooltip("O número de unidades que a bala se movimentará por frame horizontalmente.")] private float forcaMovimentoHorizontal;
	[SerializeField] [Tooltip("A distÂncia máxima em que a bala chegará antes de ser destruída.")] private float distanciaMaxima;
	[SerializeField] [Tooltip("Referêcia para o objeto que atirou a bala.")] private Transform referencia;

	/* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private float _dir;
	/* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouReferencia, _encontrouRb;
	/* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Vector3 _posInicial;
	private Rigidbody2D _rb2d;
	#endregion
	
	#endregion
	
	#region Métodos
	
	#region Métodos da Unity

	#region Métodos Privados

	private void Awake()
	{
		_rb2d = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_encontrouReferencia = referencia != null;
		_encontrouRb = _rb2d != null;
		
		_posInicial = transform.position;
		_dir = _encontrouReferencia && _posInicial.x > referencia.position.x ? 1 : -1;
		transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x) * _dir, transform.localScale.y, transform.localScale.z);
	}
	private void Update()
	{
		if (!_encontrouReferencia) return;
		Mover();
		
		if (transform.position.x >= _posInicial.x + distanciaMaxima || transform.position.x <= _posInicial.x - distanciaMaxima)
			Destruir();
	}

	private void OnCollisionEnter2D(Collision2D collision2d)
	{
		Destruir();
	}
	#endregion

	#endregion
	
	#region Métodos Personalizados
	
	#region Métodos Privados
    private void Mover()
    {
	    if (_encontrouRb)
		    _rb2d.velocity = forcaMovimentoHorizontal * new Vector3(_dir, 0, 0);
	    else
		    transform.Translate(Time.deltaTime * forcaMovimentoHorizontal * new Vector3(_dir, 0, 0));
    }
    private void Destruir()
    {
	    Destroy (gameObject);
    }
    #endregion

    #endregion
    
    #endregion
}
