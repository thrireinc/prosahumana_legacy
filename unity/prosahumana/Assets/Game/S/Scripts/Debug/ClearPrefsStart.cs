using UnityEngine;

public class ClearPrefsStart : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
