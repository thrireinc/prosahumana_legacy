using UnityEngine;

public class bInimigoChao : bInimigo
{
    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    protected new void Start()
    {
        base.Start();
        danoDado = PlayerPrefs.GetInt("p_vida");
    }

    #endregion
    
    #endregion
    
    #endregion
}
