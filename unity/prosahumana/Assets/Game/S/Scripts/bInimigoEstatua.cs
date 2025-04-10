using UnityEngine;

public class bInimigoEstatua : bInimigo
{
    #region Variáveis
    
    #region Variáveis Privadas
    
    [SerializeField] [Tooltip("O tempo que este inimigo ficará sobreaquecido sem atirar.")] private int tempoDeSobrecarga;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private float _tempoParaProximoTiro;
    
    #endregion
    
    #endregion
    
    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private new void Update()
    {
        base.Update();
        
        if (!estaEstunado)
            DispararEmSequencia();
    }
    
    #endregion
    
    #endregion
    
    #region Métodos Personalizados
    
    #region Métodos Privados
    
    private void DispararEmSequencia()
    {
        if (numeroDeBalas == tmpNumeroDeBalas)
            _tempoParaProximoTiro = (Time.time + numeroDeBalas * delayTiro) + tempoDeSobrecarga;

        if (numeroDeBalas > 0 && (Time.time > tempoAtual + delayTiro) && acaoAtirar)
            Atirar();
        else if (Time.time > _tempoParaProximoTiro)
        {
            colisorPrincipal.enabled = true;
            numeroDeBalas = tmpNumeroDeBalas;
        }
        else if (colisorPrincipal.enabled)
            colisorPrincipal.enabled = false;
    }
    
    #endregion
    
    #endregion
    
    #endregion
}
