using UnityEngine;

public class bChamarCheckpointColisor : MonoBehaviour
{
    #region Variáveis

    #region Variáveis Privadas

    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */
    private bool _encontrouLevelManager;

    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */
    private bLevelManager _levelManager;

    #endregion

    #endregion

    #region Métodos

    #region Métodos da Unity

    #region Métodos Privados

    private void Awake()
    {
        _levelManager = FindObjectOfType<bLevelManager>();
    }
    private void Start()
    {
        _encontrouLevelManager = _levelManager != null;
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player") || !_encontrouLevelManager) return;
        _levelManager.Salvar();
        PlayerPrefs.SetInt(gameObject.name, 1);
        Destroy(gameObject);
    }

    #endregion

    #endregion

    #endregion
}