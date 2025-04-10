    using UnityEngine;

public class bChave : MonoBehaviour
{
    public bool destruir;

    private void Update()
    {
        if (destruir)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("Player"))
            destruir = true;
    }
}
