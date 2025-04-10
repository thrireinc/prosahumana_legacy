using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(bParallax))]
public class eCustomInspectorParallax : Editor
{
    #region Variáveis

    private SerializedProperty _camadas, _velocidadesCamadas, _numeroDeCamadas, _suavidadeMovimentacao, _tamanhoFundo;
    private int _indexAtual;
    private bool _foldoutCamadas;

    #endregion
    
    #region Métodos da Unity

    private void OnEnable()
    {
        _camadas = serializedObject.FindProperty("camadas");
        _velocidadesCamadas = serializedObject.FindProperty("velocidadesCamadas");
        _numeroDeCamadas = serializedObject.FindProperty("numeroDeCamadas");
        _suavidadeMovimentacao = serializedObject.FindProperty("suavidadeMovimentacao");
        _tamanhoFundo = serializedObject.FindProperty("tamanhoFundo");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical();
        
            EditorGUILayout.LabelField("Atributos", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_tamanhoFundo, new GUIContent("Tamanho do Fundo"));
            EditorGUILayout.PropertyField(_suavidadeMovimentacao, new GUIContent("Suavização do Efeito"));
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Referências", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            
                _foldoutCamadas = EditorGUILayout.Foldout(_foldoutCamadas, "Número de camadas");
                EditorGUILayout.LabelField("", GUILayout.Width(150));
                _numeroDeCamadas.intValue = EditorGUILayout.IntField( _numeroDeCamadas.intValue);
                
            EditorGUILayout.EndHorizontal();
            
            _camadas.arraySize = _velocidadesCamadas.arraySize = _numeroDeCamadas.intValue;

            EditorGUILayout.LabelField("", GUILayout.Height(15));
            
            if (_foldoutCamadas)
            {
                foreach (var camada in _camadas)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                        EditorGUILayout.PropertyField(_camadas.GetArrayElementAtIndex(_indexAtual), new GUIContent(""));
                        EditorGUILayout.LabelField("    ", GUILayout.Width(50));
                        _velocidadesCamadas.GetArrayElementAtIndex(_indexAtual).floatValue = EditorGUILayout.FloatField(_velocidadesCamadas.GetArrayElementAtIndex(_indexAtual).floatValue);

                    EditorGUILayout.EndHorizontal();

                    _indexAtual++;
                }
                _indexAtual = 0;
            }

        EditorGUILayout.EndVertical();
        
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }

    #endregion
}
