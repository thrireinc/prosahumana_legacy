using UnityEditor;

[CustomEditor(typeof(bInimigoHumano))]
public class eCustomInspectorHumano : eCustomInspectorInimigo
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}
