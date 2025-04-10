using UnityEngine;

public class bPorta : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Privadas
    
    [SerializeField] private string tagChave;
    [SerializeField] private bool tocarSom;
    [SerializeField] private AudioSource som;

    private int _quantidadeDeChavesInicial, _quantidadeDeChavesAtual;
    private bool _encontrouSom;

    #endregion
    
    #endregion
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private void Start()
    {
        for (var x = 0; x < FindObjectsOfType<bChave>().Length; x++)
        {
            if (FindObjectsOfType<bChave>()[x].CompareTag(tagChave))
                _quantidadeDeChavesInicial++;
        }

        _encontrouSom = som != null;
    }
    private void Update()
    {
        CalcularChave();
    }

    #endregion
    
    #endregion
    
    #region Métodos Personalizados
    
    #region Métodos Privados
    
    private void CalcularChave()
    {
        _quantidadeDeChavesAtual = _quantidadeDeChavesInicial - GameObject.FindGameObjectsWithTag(tagChave).Length;
        if (_quantidadeDeChavesInicial != _quantidadeDeChavesAtual) return;
        if (_encontrouSom && tocarSom)
            som.Play();
                
        Destroy(gameObject);
    }
    
    #endregion
    
    #endregion
}
