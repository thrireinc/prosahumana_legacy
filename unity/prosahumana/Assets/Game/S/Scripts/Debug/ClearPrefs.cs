using System;
using UnityEngine;

public class ClearPrefs : MonoBehaviour
{
    private bool _canDebug, _startDebug, _startResetPrefs;
    private string _debugState;

    private void Start()
    {
        _startDebug = _startResetPrefs = true;
    }

    private void Update()
    {
        KeyDetection();
        
        if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D)) && _startDebug)
            VateDebug();
        
        if (!_canDebug) return;
        
        if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R)) && _startResetPrefs)
            ResetPrefs();
    }

    private void VateDebug()
    {
        _startDebug = false;
        _canDebug = !_canDebug;
        _debugState = _canDebug ? "ativado" : "desativado";
        Debug.Log("Debug " + _debugState);
    }
    private void ResetPrefs()
    {
        _startResetPrefs = false;
        PlayerPrefs.DeleteAll();
        Debug.Log("Chaves foram deletadas");
    }
    private void KeyDetection()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.R))
            _startDebug = true;
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R))
            _startResetPrefs = true;
    }
}
