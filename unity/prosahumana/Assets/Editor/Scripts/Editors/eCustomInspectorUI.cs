using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(bUI))]
public class eCustomInspectorUI : Editor
{
    #region Variáveis
    
    private SerializedProperty _target, _txtNumeroDeBalas, _sldrVida, _gradienteSldrVida, _preenchimentoVida, _txtCheckpoint, _txtColecionaveis, _podeMostrarBalas, _effector, _delayFadeIn, _delayFadeOut, _imgBala;
    private bool _balas, _vida, _checkpoint, _colecionaveis;

    #endregion
    
    #region Métodos da Unity
    
    private void OnEnable()
    {
        _target = serializedObject.FindProperty("target");
        _txtNumeroDeBalas = serializedObject.FindProperty("txtNumeroDeBalas");
        _txtColecionaveis = serializedObject.FindProperty("txtColecionaveis");
        _sldrVida = serializedObject.FindProperty("sldrVida");
        _gradienteSldrVida = serializedObject.FindProperty("gradienteSldrVida");
        _preenchimentoVida = serializedObject.FindProperty("preenchimentoVida");
        _txtCheckpoint = serializedObject.FindProperty("txtCheckpoint");
        _podeMostrarBalas = serializedObject.FindProperty("podeMostrarBalas");
        _effector = serializedObject.FindProperty("effector");
        _delayFadeIn = serializedObject.FindProperty("delayFadeIn");
        _delayFadeOut = serializedObject.FindProperty("delayFadeOut");
        _imgBala = serializedObject.FindProperty("imgBala");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
            serializedObject.Update();
            
            EditorGUILayout.LabelField("Atributos", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_target, new GUIContent("Target"));
            EditorGUILayout.PropertyField(_delayFadeIn, new GUIContent("Delay do Fade In"));
            EditorGUILayout.PropertyField(_delayFadeOut, new GUIContent("Delay do Fade Out"));
            EditorGUILayout.PropertyField(_effector, new GUIContent("Effector"));
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_podeMostrarBalas, new GUIContent("HUD de Balas"));
            EditorGUILayout.Space();
            
            
            _vida = EditorGUILayout.Foldout(_vida, "Configurações da Vida");
            if (_vida)
            {
                EditorGUILayout.PropertyField(_sldrVida, new GUIContent("Slider"));
                EditorGUILayout.PropertyField(_gradienteSldrVida, new GUIContent("Gradiente"));
                EditorGUILayout.PropertyField(_preenchimentoVida, new GUIContent("Referência para Preenchimento"));
                EditorGUILayout.Space();
            }
            
            if (_podeMostrarBalas.boolValue)
            {
                _balas = EditorGUILayout.Foldout(_balas, "Configurações das Balas");
                if (_balas)
                {
                    EditorGUILayout.PropertyField(_txtNumeroDeBalas, new GUIContent("Texto"));
                    EditorGUILayout.PropertyField(_imgBala, new GUIContent("Imagem"));
                    EditorGUILayout.Space();
                }
            }

            _checkpoint = EditorGUILayout.Foldout(_checkpoint, "Configurações do Checkpoint");
            if (_checkpoint)
                EditorGUILayout.PropertyField(_txtCheckpoint, new GUIContent("Texto"));
            
            _colecionaveis = EditorGUILayout.Foldout(_colecionaveis, "Configurações dos Colecionáveis");
            if (_colecionaveis)
                EditorGUILayout.PropertyField(_txtColecionaveis, new GUIContent("Texto"));
                

            EditorGUILayout.EndVertical();

        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
    
    #endregion
}
