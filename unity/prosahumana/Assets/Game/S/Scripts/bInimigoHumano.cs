using UnityEngine;

public class bInimigoHumano : bInimigo
{
    private new void Start()
    {
        base.Start();
        danoDado = PlayerPrefs.GetInt("p_vida");
    }
}
