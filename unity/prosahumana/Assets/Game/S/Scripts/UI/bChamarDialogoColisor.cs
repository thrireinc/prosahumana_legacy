using System.Collections;
using UnityEngine;

public class bChamarDialogoColisor : MonoBehaviour
{
    [SerializeField] private Animator objetoDeAnimacao;
    [SerializeField] private bool podeDeletar;
    
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bool _encontrouGerenciadorDeDialogos, _encontrouObjetoDeAnimacao, _encontrouLevelManager;
    private bool _podeComecarCutscene, _podeComecarDialogo;
    private bLevelManager _levelManager;
    /* Essa(s) variável(is) não aparecerá(ão) no Inspector */ private bDialogos _gerenciadorDeDialogos;

    private void Awake()
    {
        _gerenciadorDeDialogos = FindObjectOfType<bDialogos>();
        _levelManager = FindObjectOfType<bLevelManager>();
    }
    private  void Start()
    {
        _encontrouGerenciadorDeDialogos = _gerenciadorDeDialogos != null;
        _encontrouObjetoDeAnimacao = objetoDeAnimacao != null;
        _encontrouLevelManager = _levelManager != null;
        
        _podeComecarCutscene = _podeComecarDialogo = true;
    }

    private void OnTriggerStay2D(Collider2D collider2d)
    {
        if (!_encontrouGerenciadorDeDialogos) return;

        if (collider2d.CompareTag("Player") && _podeComecarDialogo)
        {
            _podeComecarDialogo = false;
            _gerenciadorDeDialogos.GsNomeDialogo = name;
            _gerenciadorDeDialogos.Begin();
        }
        else if (!_gerenciadorDeDialogos.GsEstaEmDialogo && _encontrouObjetoDeAnimacao && _podeComecarCutscene && _encontrouLevelManager)
        {
            _podeComecarCutscene = false;
            objetoDeAnimacao.SetTrigger(name);
            StartCoroutine(AlterarEstado());
        }
        
        if (podeDeletar)
            PlayerPrefs.SetInt(name, 1);
    }

    private IEnumerator AlterarEstado()
    {
	    _levelManager.GsEstaEmCutscene = true;
        yield return new WaitForSeconds(objetoDeAnimacao.speed * 3);
        _levelManager.GsEstaEmCutscene = false;
        _podeComecarCutscene = true;
        Destroy(gameObject);
    }
}
