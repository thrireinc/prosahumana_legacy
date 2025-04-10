using System.Collections;
using UnityEngine;
using VIDE_Data;
using UnityEngine.UI;

public class bDialogos : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Públicas
    
    public string GsNomeDialogo {get => nomeDialogo; set => nomeDialogo = value;}
    public bool GsEstaEmDialogo { get; private set; }

    #endregion
    
    #region Variáveis Privadas
    
    [SerializeField] [Tooltip("O tempo que o dialogo demora.")] private float tempoTexto;
    [SerializeField] [Tooltip("O nome do diálogo.")] private string nomeDialogo;
    [SerializeField] [Tooltip("Referência para o container do diálogo.")] private GameObject container;
    [SerializeField] [Tooltip("Referência para o texto do player.")] private Text textoPlayer;
    [SerializeField] [Tooltip("Referência para o texto do(s) NPC(s).")] private Text textoNpc;
    [SerializeField] [Tooltip("Referência para a imagem de quem está falando.")] private Image img;
    [SerializeField] private AudioSource _som;
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _podeAtualizarUI, _podePassar, _encontrouSom;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private VIDE_Assign _vide;

    #endregion
    
    #endregion
    
    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private void Awake()
    {
        _vide = GetComponent<VIDE_Assign>();
    }
    private void Start()
    {
        _podeAtualizarUI = true;
        _encontrouSom = _som != null;
        container.SetActive(false);
        VD.LoadDialogues();
    }
    private void Update()
    {
        if (_podePassar && Input.anyKey)
            Passar();
    }
    
    #endregion
    
    #endregion
    
    #region Métodos Personalizados
    
    #region Métodos Públicos
    
    public void Passar()
    {
        VD.Next();
    }
    public void Begin()
    {
        GsEstaEmDialogo = true;
        _vide.assignedDialogue = nomeDialogo;
        _vide.preload = true;
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(_vide);
    }
    
    #endregion
    
    #region Métodos Privados
    
    private void UpdateUI(VD.NodeData data)
    {
        if (!_podeAtualizarUI) return;
        textoPlayer.transform.gameObject.SetActive(false);
        textoNpc.transform.gameObject.SetActive(false);
        _podeAtualizarUI = _podePassar = false;
        Text t;

        container.SetActive(true);
        img.gameObject.SetActive(true);
        img.sprite = data.sprite;
        
        if (_encontrouSom && !_som.loop)
        {
            _som.loop = true;
            _som.Play();
        }
        
        if (data.isPlayer)
        {
            textoPlayer.text = "";
            textoPlayer.transform.gameObject.SetActive(true);
            t = textoPlayer;
        }
        else
        {
            textoNpc.text = "";
            textoNpc.transform.gameObject.SetActive(true);
            t = textoNpc;
        }

        StartCoroutine(TypeWriter(t, data));
    }
    private void End(VD.NodeData data)
    {
        GsEstaEmDialogo = _podePassar = false;
        container.SetActive(false);
        img.gameObject.SetActive(false);
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }
    private void OnDisable()
    {
        if (container != null)
            End(null);
    }
    
    private IEnumerator TypeWriter(Text t, VD.NodeData data)
    {
        for (var x = 0; x < data.comments[data.commentIndex].Length; x++)
        {
            t.text = data.comments[data.commentIndex].Substring(0, x);
            yield return new WaitForSeconds(tempoTexto);
        }

        t.text = data.comments[data.commentIndex];
        _podeAtualizarUI = _podePassar = true;
        
        if (_encontrouSom && _som.loop)
        {
            _som.loop = false;
            _som.Stop();
        }
        
        StopCoroutine(TypeWriter(t, data));
    }
    
    #endregion
    
    #endregion

    #endregion
}
