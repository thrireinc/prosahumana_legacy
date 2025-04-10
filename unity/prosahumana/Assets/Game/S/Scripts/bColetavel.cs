using UnityEngine;

public class bColetavel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player")) return;
        PlayerPrefs.SetInt("p_colecionavel", PlayerPrefs.GetInt("p_colecionavel") + 1);
        PlayerPrefs.SetInt(name, 1);
        Destroy(gameObject);
    }
}
