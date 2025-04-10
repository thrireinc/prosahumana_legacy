using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(bInimigoEstatua))]
public class eCustomInspectorInimigoEstatua : eCustomInspectorInimigo
{
    #region Variáveis

    private SerializedProperty _tempoDeSobrecarga, _tempoStum;
    private bool _foldoutInimigoEstatua;
    
    #endregion
	
    #region Métodos da Unity
	
    protected new void OnEnable()
    {
        base.OnEnable();
        
        _tempoDeSobrecarga = serializedObject.FindProperty("tempoDeSobrecarga");
        _tempoStum = serializedObject.FindProperty("tempoStum");
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        _foldoutInimigoEstatua = EditorGUILayout.Foldout(_foldoutInimigoEstatua, "Características do Inimigo Estátua");
        
        if (_foldoutInimigoEstatua)
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Atributos", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(_tempoDeSobrecarga, new GUIContent("Tempo de Sobrecarga"));
            EditorGUILayout.PropertyField(_tempoStum, new GUIContent("Tempo de Stum"));

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.Space();
        
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
    #endregion
}
