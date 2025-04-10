using UnityEngine;
using UnityEngine.SceneManagement;

public class bPassarFaseColisor : MonoBehaviour  
{
    #region Variáveis
    
    #region Variáveis Privadas
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouLevelManager, _encontrouGerenciadorDeDialogos;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bLevelManager _levelManager;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bDialogos _gerenciadorDeDialogos;

    #endregion
    
    #endregion
    
    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados

    private void Awake()
    {
        _levelManager = FindObjectOfType<bLevelManager>();
        _gerenciadorDeDialogos = FindObjectOfType<bDialogos>();
    }
    private void Start()
    {
        _encontrouLevelManager = _levelManager != null;
        _encontrouGerenciadorDeDialogos = _gerenciadorDeDialogos != null;
    }
    private void OnTriggerStay2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player") || !_encontrouLevelManager || ((!_encontrouGerenciadorDeDialogos || _gerenciadorDeDialogos.GsEstaEmDialogo) && (_encontrouGerenciadorDeDialogos))) return;
        _levelManager.StartCarregarFase(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    #endregion
    
    #endregion
    
    #endregion
}
