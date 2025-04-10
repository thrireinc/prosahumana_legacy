using UnityEngine;

public class bParallax : MonoBehaviour
{
    #region Variáveis

    // Variáveis Privadas
    [SerializeField] [Tooltip("O número total de camadas presentes no parallax.")] private int numeroDeCamadas;
    [SerializeField] [Tooltip("O valor da suavização que será aplicada no parallax.")] private float suavidadeMovimentacao;
    [SerializeField] [Tooltip("O valor do tamanho do fundo.")] private float tamanhoFundo;
    [SerializeField] [Tooltip("A velocidade em que a camada se movimentará.")] private float[] velocidadesCamadas;
    [SerializeField] [Tooltip("Referência para o objeto que representa a camada.")] private GameObject[] camadas;
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouCam;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Vector3 _posicaoAnteriorCamera;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private Camera _cam;

    #endregion
    
    #region Métodos da Unity

    private void Awake()
    {
        _cam = Camera.main;
    }
    private void Start()
    {        
        _encontrouCam = _cam != null;
        _posicaoAnteriorCamera = _encontrouCam ? _cam.transform.position : _posicaoAnteriorCamera;
        
        for (var x = 0; x < numeroDeCamadas; x++)
            velocidadesCamadas[x] = camadas[x].transform.position.z * -1;
    }
    private void LateUpdate()
    {
        MudarFundo();
    }
    
    #endregion
    
    #region Métodos Personalizados

    private void MudarFundo()
    {
        if (!_encontrouCam) return;
        
        for (var x = 0; x < numeroDeCamadas; x++)
        {
            if (_cam.WorldToViewportPoint(camadas[x].transform.position).x - tamanhoFundo / 2 < 0 || _cam.WorldToViewportPoint(camadas[x].transform.position).x  + tamanhoFundo / 2 > 1)
            {
                if (_cam.transform.position.x >= camadas[x].transform.position.x + (tamanhoFundo + (tamanhoFundo / 2)))
                    camadas[x].transform.position = new Vector3(camadas[x].transform.position.x + tamanhoFundo * 3, camadas[x].transform.position.y, camadas[x].transform.position.z);
                else if (_cam.transform.position.x <= camadas[x].transform.position.x - (tamanhoFundo + (tamanhoFundo/ 2)))
                    camadas[x].transform.position = new Vector3(camadas[x].transform.position.x - tamanhoFundo * 3, camadas[x].transform.position.y, camadas[x].transform.position.z);
            }

            var parallax = (_posicaoAnteriorCamera.x - _cam.transform.position.x) * velocidadesCamadas[x];
            var proximaPosicao = new Vector3(camadas[x].transform.position.x + parallax, camadas[x].transform.position.y, camadas[x].transform.position.z);
            camadas[x].transform.position = Vector3.Lerp (camadas[x].transform.position, proximaPosicao, suavidadeMovimentacao * Time.deltaTime);
        }
        _posicaoAnteriorCamera = _cam.transform.position;
    }

    #endregion
}
