using UnityEngine;
using UnityEngine.SceneManagement;

public class bAcabarCreditos : MonoBehaviour
{
    public bool acabarCreditos;
    
    private void Update()
    {
        if (acabarCreditos)
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
