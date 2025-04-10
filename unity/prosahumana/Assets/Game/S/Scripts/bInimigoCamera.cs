using UnityEngine;

public class bInimigoCamera : bInimigo
{
    #region Variáveis

    #region Variáveis Privadas
    
    [SerializeField] [Tooltip("Referência para o colisor que representa o campo de visão.")] private Collider2D campoDeVisaoObjeto;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouCampoDeVisaoObjeto;
    
    #endregion
    
    #endregion
    
    #region Métodos
    
    #region Métodos da Unity

    #region Métodos Protegidos
    
    protected new void Start()
    {
        base.Start();
        _encontrouCampoDeVisaoObjeto = campoDeVisaoObjeto != null;
    }
    protected new void Update()
    {
        base.Update();
        Perseguir();
    }
    
    #endregion

    #region Métodos Privados
    
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("Player"))
            estaEstunando = true;
    }
    
    #endregion
    
    #endregion
    
    #region Métodos Personalizados

    #region Métodos Privados

    private void Perseguir()
    {
        if (estaEstunado && _encontrouCampoDeVisaoObjeto)
        {
            campoDeVisaoObjeto.enabled = false;
        }
        else if (estaEstunando && encontrouPlayer && player.GsIndexDesestunar >= 4)
        {
            tempoAtual = Time.time;
            estaEstunando = false;
            estaEstunado = true;
        }
        else if (_encontrouCampoDeVisaoObjeto && !campoDeVisaoObjeto.enabled)
            campoDeVisaoObjeto.enabled = true;
    }

    #endregion
    
    #endregion
    
    #endregion
}
