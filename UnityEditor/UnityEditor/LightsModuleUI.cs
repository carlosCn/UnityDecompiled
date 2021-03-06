using System;
using UnityEngine;

namespace UnityEditor
{
	internal class LightsModuleUI : ModuleUI
	{
		private class Texts
		{
			public GUIContent ratio = EditorGUIUtility.TrTextContent("Ratio", "Amount of particles that have a light source attached to them.", null);

			public GUIContent randomDistribution = EditorGUIUtility.TrTextContent("Random Distribution", "Emit lights randomly, or at regular intervals.", null);

			public GUIContent light = EditorGUIUtility.TrTextContent("Light", "Light prefab to be used for spawning particle lights.", null);

			public GUIContent color = EditorGUIUtility.TrTextContent("Use Particle Color", "Check the option to multiply the particle color by the light color. Otherwise, only the color of the light is used.", null);

			public GUIContent range = EditorGUIUtility.TrTextContent("Size Affects Range", "Multiply the range of the light with the size of the particle.", null);

			public GUIContent intensity = EditorGUIUtility.TrTextContent("Alpha Affects Intensity", "Multiply the intensity of the light with the alpha of the particle.", null);

			public GUIContent rangeCurve = EditorGUIUtility.TrTextContent("Range Multiplier", "Apply a custom multiplier to the range of the lights.", null);

			public GUIContent intensityCurve = EditorGUIUtility.TrTextContent("Intensity Multiplier", "Apply a custom multiplier to the intensity of the lights.", null);

			public GUIContent maxLights = EditorGUIUtility.TrTextContent("Maximum Lights", "Limit the amount of lights the system can create. This module makes it very easy to create lots of lights, which can hurt performance.", null);
		}

		private static LightsModuleUI.Texts s_Texts;

		private SerializedProperty m_Ratio;

		private SerializedProperty m_RandomDistribution;

		private SerializedProperty m_Light;

		private SerializedProperty m_UseParticleColor;

		private SerializedProperty m_SizeAffectsRange;

		private SerializedProperty m_AlphaAffectsIntensity;

		private SerializedMinMaxCurve m_Range;

		private SerializedMinMaxCurve m_Intensity;

		private SerializedProperty m_MaxLights;

		public LightsModuleUI(ParticleSystemUI owner, SerializedObject o, string displayName) : base(owner, o, "LightsModule", displayName)
		{
			this.m_ToolTip = "Controls light sources attached to particles.";
		}

		protected override void Init()
		{
			if (this.m_Ratio == null)
			{
				if (LightsModuleUI.s_Texts == null)
				{
					LightsModuleUI.s_Texts = new LightsModuleUI.Texts();
				}
				this.m_Ratio = base.GetProperty("ratio");
				this.m_RandomDistribution = base.GetProperty("randomDistribution");
				this.m_Light = base.GetProperty("light");
				this.m_UseParticleColor = base.GetProperty("color");
				this.m_SizeAffectsRange = base.GetProperty("range");
				this.m_AlphaAffectsIntensity = base.GetProperty("intensity");
				this.m_MaxLights = base.GetProperty("maxLights");
				this.m_Range = new SerializedMinMaxCurve(this, LightsModuleUI.s_Texts.rangeCurve, "rangeCurve");
				this.m_Intensity = new SerializedMinMaxCurve(this, LightsModuleUI.s_Texts.intensityCurve, "intensityCurve");
			}
		}

		public override void OnInspectorGUI(InitialModuleUI initial)
		{
			ModuleUI.GUIObject(LightsModuleUI.s_Texts.light, this.m_Light, new GUILayoutOption[0]);
			ModuleUI.GUIFloat(LightsModuleUI.s_Texts.ratio, this.m_Ratio, new GUILayoutOption[0]);
			ModuleUI.GUIToggle(LightsModuleUI.s_Texts.randomDistribution, this.m_RandomDistribution, new GUILayoutOption[0]);
			ModuleUI.GUIToggle(LightsModuleUI.s_Texts.color, this.m_UseParticleColor, new GUILayoutOption[0]);
			ModuleUI.GUIToggle(LightsModuleUI.s_Texts.range, this.m_SizeAffectsRange, new GUILayoutOption[0]);
			ModuleUI.GUIToggle(LightsModuleUI.s_Texts.intensity, this.m_AlphaAffectsIntensity, new GUILayoutOption[0]);
			ModuleUI.GUIMinMaxCurve(LightsModuleUI.s_Texts.rangeCurve, this.m_Range, new GUILayoutOption[0]);
			ModuleUI.GUIMinMaxCurve(LightsModuleUI.s_Texts.intensityCurve, this.m_Intensity, new GUILayoutOption[0]);
			ModuleUI.GUIInt(LightsModuleUI.s_Texts.maxLights, this.m_MaxLights, new GUILayoutOption[0]);
			if (this.m_Light.objectReferenceValue)
			{
				Light light = (Light)this.m_Light.objectReferenceValue;
				if (light.type != LightType.Point && light.type != LightType.Spot)
				{
					GUIContent gUIContent = EditorGUIUtility.TrTextContent("Only point and spot lights are supported on particles.", null, null);
					EditorGUILayout.HelpBox(gUIContent.text, MessageType.Warning, true);
				}
			}
		}
	}
}
