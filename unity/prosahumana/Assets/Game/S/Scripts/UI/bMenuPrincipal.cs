using UnityEngine;
using  UnityEngine.UI;

public class bMenuPrincipal : MonoBehaviour
{
    [SerializeField] [Tooltip("Referência para o objeto que representa o texto do botão continuar.")] private Text txtBtContinuar;
    [SerializeField] [Tooltip("Referência para o objeto que representa o texto do botão continuar.")] private Text txtBtIniciarJogo;

    private bool _encontrouTxtBtContinuar, _encontrouTxtBtIniciarJogo;
    
    private void Start()
    {
        _encontrouTxtBtContinuar = txtBtContinuar != null;
        _encontrouTxtBtIniciarJogo = txtBtIniciarJogo != null;

        if (!PlayerPrefs.HasKey("levelIndex"))
        {
            PlayerPrefs.DeleteAll();
            
            if (_encontrouTxtBtIniciarJogo)
                txtBtIniciarJogo.gameObject.SetActive(true);
        }
        else if (_encontrouTxtBtContinuar)
            txtBtContinuar.gameObject.SetActive(true);
    }
}
