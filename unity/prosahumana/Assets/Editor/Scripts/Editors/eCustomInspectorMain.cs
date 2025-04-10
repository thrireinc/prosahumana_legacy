using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(bMain))]
[CanEditMultipleObjects]
public class eCustomInspectorMain : eCustomInspectorPersonagem
{
	#region Variáveis
	
	private SerializedProperty _somEncostouChao, _somPulo, _somAgachar, _numeroDeVidas, _forcaMovimentoVertical, _indexQueda, _particulasPulo, _limiteTeto, _david, _vidasInfinitas, _stumInfinito;
	private bool _foldoutMain, _foldoutParticulas; 
	
	#endregion
	
	#region Métodos da Unity
	
	protected new void OnEnable()
	{
		base.OnEnable();
		
		_somEncostouChao = serializedObject.FindProperty("somEncostouChao");
		_somPulo = serializedObject.FindProperty("somPulo");
		_numeroDeVidas = serializedObject.FindProperty("numeroDeVidas");
		_indexQueda = serializedObject.FindProperty("indexQueda");
		_forcaMovimentoVertical = serializedObject.FindProperty("forcaMovimentoVertical");
		_particulasPulo = serializedObject.FindProperty("particulasPulo");
		_limiteTeto = serializedObject.FindProperty("limiteTeto");
		_david = serializedObject.FindProperty("david");
		_vidasInfinitas = serializedObject.FindProperty("vidasInfinitas");
		_stumInfinito = serializedObject.FindProperty("stumInfinito");
		_somAgachar = serializedObject.FindProperty("somAgachar");
	}
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();
		_foldoutMain = EditorGUILayout.Foldout(_foldoutMain, "Características do Main");

		if (_foldoutMain)
		{
			EditorGUILayout.BeginVertical();

				EditorGUILayout.BeginHorizontal();

					GUI.enabled = !_vidasInfinitas.boolValue;
					EditorGUILayout.PropertyField(_numeroDeVidas, new GUIContent("Vida Total"));
					GUI.enabled = true;
								
					EditorGUILayout.Space(25);
								
					EditorGUIUtility.labelWidth = 65;
					EditorGUILayout.PropertyField(_vidasInfinitas, new GUIContent("Infinitas?"));
					EditorGUIUtility.labelWidth = tmp;
							
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.LabelField("Atributos", EditorStyles.boldLabel);
				EditorGUILayout.PropertyField(_stumInfinito, new GUIContent("Nunca estunar?"));
				EditorGUILayout.PropertyField(_forcaMovimentoVertical, new GUIContent("Força do Pulo"));
				EditorGUILayout.PropertyField(_indexQueda, new GUIContent("Força de Queda"));
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Referências", EditorStyles.boldLabel);
				
				EditorGUILayout.PropertyField(_david, new GUIContent("David"));
				EditorGUILayout.PropertyField(_limiteTeto, new GUIContent("Limite Vertical"));
				
				_foldoutParticulas = EditorGUILayout.Foldout(_foldoutParticulas, "Particulas");
				if (_foldoutParticulas) EditorGUILayout.PropertyField(_particulasPulo, new GUIContent("Pulo"));
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Sons", EditorStyles.boldLabel);
				EditorGUILayout.PropertyField(_somPulo, new GUIContent("Pular"));
				EditorGUILayout.PropertyField(_somAgachar, new GUIContent("Agachar"));
				EditorGUILayout.PropertyField(_somEncostouChao, new GUIContent("Encostar no Chão"));
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Outros", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("Temporizador de Feedback de Dano: " + bMain.TemporizadorFeedbackDano + " segundos");
				EditorGUILayout.LabelField("Tempo de Invunerabilidade quando tomar dano: " + bMain.TemporizadorFeedbackDano * bMain.RepeticaoFeedbackDano + " segundos");
				EditorGUILayout.LabelField("Tempo de dano (estunado): " + bMain.TempoDanoStum);
				EditorGUILayout.LabelField("Multiplicador de Gravidade: " + bMain.MultiplicadorDeGravidade);
				EditorGUILayout.LabelField("Divisor de Movimentação (abaixado): " + bMain.DivisorDeMovimentacaoAbaixado);
				EditorGUILayout.LabelField("Altura de Verificação de Teto: " + bMain.AlturaDeVerificacaoDeTeto);
				EditorGUILayout.LabelField("Largura de Detecção de Caixas: " + bMain.LarguraDeDeteccaoCaixa);
				
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.Space();
		
		if (EditorGUI.EndChangeCheck())
	        serializedObject.ApplyModifiedProperties();
	}
	
	#endregion
}
