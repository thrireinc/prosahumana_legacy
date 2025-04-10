using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(bInimigoChao))]
public class eCustomInspectorInimigoChao : eCustomInspectorInimigo
{
    #region Variáveis
    
    private bool _foldoutInimigoChao;
    
    #endregion
    
    #region Métodos da Unity
    
    protected new void OnEnable()
    {
        base.OnEnable();
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        _foldoutInimigoChao = EditorGUILayout.Foldout(_foldoutInimigoChao, "Características do Inimigo Chão");
        
        if (_foldoutInimigoChao)
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Referências", EditorStyles.boldLabel);

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.Space();
        
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
    
    #endregion
}
