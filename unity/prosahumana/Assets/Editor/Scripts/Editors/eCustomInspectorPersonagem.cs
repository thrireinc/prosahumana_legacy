using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(bPersonagem), true)]
public class eCustomInspectorPersonagem : Editor
{
    #region Variáveis

    protected float tmp;
    
    private SerializedProperty _forcaMovimentoHorizontal, _delayTiro, _numeroDeBalas, _bala, _armas, _acaoAtirar, _colisorPrincipal, _balasInfinitas, _somAtirar, _somTomarDano, _somAndar;
    private bool _foldoutPersonagem;
    
    #endregion
    
	#region Métodos da Unity
	
	protected void OnEnable()
	{
		_forcaMovimentoHorizontal = serializedObject.FindProperty("forcaMovimentoHorizontal");
		_delayTiro = serializedObject.FindProperty("delayTiro");
		_numeroDeBalas = serializedObject.FindProperty("numeroDeBalas");
		_bala = serializedObject.FindProperty("bala");
		_armas = serializedObject.FindProperty("armas");
		_acaoAtirar = serializedObject.FindProperty("acaoAtirar");
		_colisorPrincipal = serializedObject.FindProperty("colisorPrincipal");
		_balasInfinitas = serializedObject.FindProperty("balasInfinitas");
		_somAtirar = serializedObject.FindProperty("somAtirar");
		_somTomarDano = serializedObject.FindProperty("somTomarDano");
		_somAndar = serializedObject.FindProperty("somAndar");
	}
	public override void OnInspectorGUI()
	{
		tmp = EditorGUIUtility.labelWidth;
		
		serializedObject.Update();
		_foldoutPersonagem = EditorGUILayout.Foldout(_foldoutPersonagem, "Características Principais");
		
		if (_foldoutPersonagem)
		{
			EditorGUILayout.BeginVertical();
			
	            EditorGUILayout.LabelField("Atributos", EditorStyles.boldLabel);
	            EditorGUILayout.PropertyField(_forcaMovimentoHorizontal, new GUIContent("Velocidade"));
	            if (_acaoAtirar.boolValue)
	            {
		            EditorGUILayout.BeginHorizontal();

						GUI.enabled = !_balasInfinitas.boolValue;
						EditorGUILayout.PropertyField(_numeroDeBalas, new GUIContent("Número de Balas"));
						GUI.enabled = true;
						
						EditorGUILayout.Space(25);
						
						EditorGUIUtility.labelWidth = 65;
						EditorGUILayout.PropertyField(_balasInfinitas, new GUIContent("Infinitas?"));
						EditorGUIUtility.labelWidth = tmp;
						
		            EditorGUILayout.EndHorizontal();
		            
		            EditorGUILayout.PropertyField(_delayTiro, new GUIContent("Delay de Tiro"));
	            }
	            
	            EditorGUILayout.Space();
	            EditorGUILayout.LabelField("Referências", EditorStyles.boldLabel);
	            EditorGUILayout.PropertyField(_colisorPrincipal, new GUIContent("Colisor Principal"));
	            if (_acaoAtirar.boolValue)
	            {
		            EditorGUILayout.PropertyField(_bala, new GUIContent("Bala"));
		            EditorGUILayout.PropertyField(_armas, new GUIContent("Arma"));
	            }

	            EditorGUILayout.Space();
	            EditorGUILayout.LabelField("Ações", EditorStyles.boldLabel);
	            EditorGUILayout.PropertyField(_acaoAtirar, new GUIContent("Pode Atirar"));
	            EditorGUILayout.Space();
	            
	            EditorGUILayout.LabelField("Sons", EditorStyles.boldLabel);
	            EditorGUILayout.PropertyField(_somTomarDano, new GUIContent("Tomar Dano"));
	            EditorGUILayout.PropertyField(_somAtirar, new GUIContent("Atirar"));
	            EditorGUILayout.PropertyField(_somAndar, new GUIContent("Andar"));
	            
            EditorGUILayout.EndVertical();
		}
		EditorGUILayout.Space();
		
        if (EditorGUI.EndChangeCheck())
	        serializedObject.ApplyModifiedProperties();
	}
	
	#endregion
}
