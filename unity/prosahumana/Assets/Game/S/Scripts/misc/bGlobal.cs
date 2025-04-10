using System;
using UnityEngine;

public class bGlobal : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
