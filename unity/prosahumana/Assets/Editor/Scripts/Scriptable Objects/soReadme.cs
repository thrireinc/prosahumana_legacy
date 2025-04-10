using System;
using UnityEngine;

[CreateAssetMenu(fileName = "readme", menuName = "ScriptableObjects/readme", order = 1)]
public class soReadme : ScriptableObject
{
    public string title;
    public Section[] sections;
    public bool loadedLayout;

    [Serializable]
    public class Section
    {
        public string heading, text;
    }
}
