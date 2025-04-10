using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(bCamera))]
public class eCustomInspectorCamera : Editor
{
    #region Variáveis
    
    private SerializedProperty _forcaMovimento, _target, _limiteEsquerdo, _limiteDireito, _limiteBaixo, _limiteCima;
    
    #endregion
    
    #region Métodos da Unity

    private void OnEnable()
    {
        _forcaMovimento = serializedObject.FindProperty("forcaMovimento");
        _target = serializedObject.FindProperty("target");
        _limiteEsquerdo = serializedObject.FindProperty("limiteEsquerdo");
        _limiteDireito = serializedObject.FindProperty("limiteDireito");
        _limiteBaixo = serializedObject.FindProperty("limiteBaixo");
        _limiteCima = serializedObject.FindProperty("limiteCima");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical();
		
            EditorGUILayout.LabelField("Atributos", EditorStyles.boldLabel);
            
            EditorGUILayout.PropertyField(_forcaMovimento, new GUIContent("Velocidade"));
            EditorGUILayout.PropertyField(_target, new GUIContent("Target"));
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Limites da Câmera");
            EditorGUILayout.BeginHorizontal();
            
                EditorGUILayout.LabelField("E: ", GUILayout.Width(25));
                EditorGUILayout.PropertyField(_limiteEsquerdo, new GUIContent(""));
                
                EditorGUILayout.LabelField("    ", GUILayout.Width(50));
                
                EditorGUILayout.LabelField("D: ", GUILayout.Width(25));
                EditorGUILayout.PropertyField(_limiteDireito, new GUIContent(""));
                
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("C: ", GUILayout.Width(25));
            EditorGUILayout.PropertyField(_limiteCima, new GUIContent(""));
                
            EditorGUILayout.LabelField("    ", GUILayout.Width(50));
                
            EditorGUILayout.LabelField("B: ", GUILayout.Width(25));
            EditorGUILayout.PropertyField(_limiteBaixo, new GUIContent(""));
                
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }

    #endregion
}
