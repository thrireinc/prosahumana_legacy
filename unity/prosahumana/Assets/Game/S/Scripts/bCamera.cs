using UnityEngine;

public class bCamera : MonoBehaviour
{
    #region Variáveis
    
    // Variáveis Privadas
    [SerializeField] [Tooltip("A velocidade de movimentação da câmera.")] private float forcaMovimento = 1;
    [SerializeField] [Tooltip("O limite esquerdo que a câmera não ultrapassará.")] private float limiteEsquerdo;
    [SerializeField] [Tooltip("O limite direito que a câmera não ultrapassará.")] private float limiteDireito;
    [SerializeField] [Tooltip("O limite baixo que a câmera não ultrapassará.")] private float limiteBaixo;
    [SerializeField] [Tooltip("O limite cima que a câmera não ultrapassará.")] private float limiteCima;
    [SerializeField] [Tooltip("Referência para o objeto que a câmera seguirá.")] private Transform target;
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouTarget;
    
    #endregion
    
    #region Métodos da Unity

    // Métodos Privados
    private void Awake()
    {
        if (target == null && !ReferenceEquals(GameObject.FindWithTag("Player"), null))
            target = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {

    }
    private void Update()
    {
        _encontrouTarget = !ReferenceEquals(target, null);

        if (_encontrouTarget)
            Mover();
    }
    
    #endregion
    
    #region Métodos Personalizados

    // Métodos Privados
    private void Mover()
    {
        var targetPos = target.position;
        var proximaPos = new Vector3(targetPos.x >= limiteDireito ? limiteDireito : targetPos.x <= limiteEsquerdo ? limiteEsquerdo : targetPos.x, targetPos.y >= limiteCima ? limiteCima : targetPos.y <= limiteBaixo ? limiteBaixo : targetPos.y, transform.position.z);
        var posFinal = Vector3.Lerp(transform.position, proximaPos, forcaMovimento);

        transform.position = proximaPos;
    }

    #endregion
}
