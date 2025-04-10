using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(bInimigoCamera))]
public class eCustomInspectorInimigoCamera : eCustomInspectorInimigo
{
    #region Variáveis
    
    private SerializedProperty _campoDeVisaoObjeto, _somDano;
    private bool _foldoutInimigoCamera;
    
    #endregion
    
    #region Métodos da Unity
    
    protected new void OnEnable()
    {
        base.OnEnable();
        
        _campoDeVisaoObjeto = serializedObject.FindProperty("campoDeVisaoObjeto");
        _somDano = serializedObject.FindProperty("somDano");
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        _foldoutInimigoCamera = EditorGUILayout.Foldout(_foldoutInimigoCamera, "Características do Inimigo Câmera");
        
        if (_foldoutInimigoCamera)
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Referências", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_campoDeVisaoObjeto, new GUIContent("Campo de Visão (Objeto)"));
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Sons", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_somDano, new GUIContent("Dano"));
            
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.Space();
        
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
    
    #endregion
}
