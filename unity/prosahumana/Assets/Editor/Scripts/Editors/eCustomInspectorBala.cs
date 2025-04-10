using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(bBala))]
public class eCustomInspectorBala : Editor
{
    #region Variáveis
    
    private SerializedProperty _forcaMovimentoHorizontal, _distanciaMaxima;
    
    #endregion
    
    #region Métodos da Unity

    private void OnEnable()
    {
        _forcaMovimentoHorizontal = serializedObject.FindProperty("forcaMovimentoHorizontal");
        _distanciaMaxima = serializedObject.FindProperty("distanciaMaxima");
    }

    public override void OnInspectorGUI()
    {
        var b = (bBala)target;

        EditorGUILayout.BeginVertical();

            serializedObject.Update();
            
            EditorGUILayout.LabelField("Atributos", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_forcaMovimentoHorizontal, new GUIContent("Velocidade"));
            EditorGUILayout.PropertyField(_distanciaMaxima, new GUIContent("Distância Máxima da Bala"));

        EditorGUILayout.EndVertical();

        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
    
    #endregion
}
