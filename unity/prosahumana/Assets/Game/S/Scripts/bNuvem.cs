using UnityEngine;

public class bNuvem : MonoBehaviour
{
    [SerializeField] private float forcaMovimento;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }
    private void Update()
    {
        Mover();
    }

    private void Mover()
    {
        transform.Translate(Vector3.left * (forcaMovimento * Time.deltaTime));
        
        if (_cam.WorldToViewportPoint(transform.position).x < 0 - 5f)
            Destroy(gameObject);
    }
}
