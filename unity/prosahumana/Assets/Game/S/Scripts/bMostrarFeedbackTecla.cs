using System;
using UnityEngine;

public class bMostrarFeedbackTecla : MonoBehaviour
{
    [SerializeField] private GameObject tecla;
    private bool _encontrouTecla;

    private void Start()
    {
        _encontrouTecla = tecla != null;
    }

    private void Update()
    {
        if (tecla != null)
            tecla.transform.localScale = new Vector3(transform.parent.transform.parent.transform.localScale.x, tecla.transform.localScale.y, tecla.transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (_encontrouTecla && collider2d.CompareTag("Player"))
            tecla.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collider2d)
    {
        if (_encontrouTecla && collider2d.CompareTag("Player"))
            tecla.SetActive(false);
    }
}
