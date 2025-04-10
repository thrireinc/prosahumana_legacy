using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class eCustomInspectorInimigo : eCustomInspectorPersonagem
{
    #region Variáveis
    
    private SerializedProperty _podeRondar, _podeSeguir, _podeAumentarVelocidade, _limiteRonda, _tempoStum, _campoDeVisaoNumero, _direcaoInicial, _danoDado, _delayAumentarVelocidade, _utilizarPosicaoInicial, _indexAumentoVelocidade;
    private bool _foldoutInimigo;
    
    #endregion
    
    #region Métodos da Unity
    
    protected new void OnEnable()
    {
        base.OnEnable();
        
        _podeRondar = serializedObject.FindProperty("podeRondar");
        _podeSeguir = serializedObject.FindProperty("podeSeguir");
        _podeAumentarVelocidade = serializedObject.FindProperty("podeAumentarVelocidade");
        _limiteRonda = serializedObject.FindProperty("limiteRonda");
        _tempoStum = serializedObject.FindProperty("tempoStum");
        _campoDeVisaoNumero = serializedObject.FindProperty("campoDeVisaoNumero");
        _direcaoInicial = serializedObject.FindProperty("direcaoInicial");
        _danoDado = serializedObject.FindProperty("danoDado");
        _delayAumentarVelocidade = serializedObject.FindProperty("delayAumentarVelocidade");
        _utilizarPosicaoInicial = serializedObject.FindProperty("utilizarPosicaoInicial");
        
        _indexAumentoVelocidade = serializedObject.FindProperty("indexAumentoVelocidade");
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        _foldoutInimigo = EditorGUILayout.Foldout(_foldoutInimigo, "Características do Inimigo");
        
        if (_foldoutInimigo)
        {
            EditorGUILayout.BeginVertical();

                EditorGUILayout.LabelField("Atributos", EditorStyles.boldLabel);
                
                if (_podeAumentarVelocidade.boolValue)
                    EditorGUILayout.PropertyField(_delayAumentarVelocidade, new GUIContent("Delay entre aumentos de velocidade"));
                
                EditorGUILayout.PropertyField(_tempoStum, new GUIContent("Tempo que ficará estunado"));
                EditorGUILayout.PropertyField(_campoDeVisaoNumero, new GUIContent("Campo de Visão"));
                EditorGUILayout.PropertyField(_danoDado, new GUIContent("Dano dado caso o jogador encoste"));
                EditorGUILayout.PropertyField(_direcaoInicial, new GUIContent("Direção Inicial"));
                if (_podeRondar.boolValue)
                    EditorGUILayout.PropertyField(_limiteRonda, new GUIContent("Limite da Ronda"));
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Ações", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_podeRondar, new GUIContent("Pode Rondar"));
                EditorGUILayout.PropertyField(_podeSeguir, new GUIContent("Pode Seguir"));
                EditorGUILayout.PropertyField(_podeAumentarVelocidade, new GUIContent("Pode Aumentar Velocidade"));
                if (_podeAumentarVelocidade.boolValue)
                    EditorGUILayout.PropertyField(_indexAumentoVelocidade, new GUIContent("Index de Aumento"));
                EditorGUILayout.PropertyField(_utilizarPosicaoInicial, new GUIContent("Utilizar Posição Inicial"));
                EditorGUILayout.Space();

                EditorGUILayout.EndVertical();
        }
        EditorGUILayout.Space();
        
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
    
    #endregion
}
