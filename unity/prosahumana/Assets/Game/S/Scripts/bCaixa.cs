using UnityEngine;

public class bCaixa : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Privadas
    
    private bool _encontrouRigidbody;
    private Rigidbody2D rb2d;
    private Vector3 _posInicial;
    
    #endregion
    
    #endregion
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _encontrouRigidbody = rb2d != null;
        _posInicial = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("destruir")) return;
        transform.position = _posInicial;
    }
    
    #endregion
    
    #endregion
}
