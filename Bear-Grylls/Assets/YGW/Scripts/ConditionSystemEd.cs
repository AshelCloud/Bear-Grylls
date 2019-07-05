using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YGW
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ConditionSystem))]
    public class ConditionSystemEd : Editor
    {
        private ConditionSystem M;
        MonoScript script;

        public GUIStyle FlatBox
        {
            get
            {
                return Style(new Color(0.35f, 0.35f, 0.35f, 0.1f));
            }
        }

        public static GUIStyle Style(Color color)
        {
            GUIStyle currentStyle = new GUIStyle(GUI.skin.box)
            {
                border = new RectOffset(-1, -1, -1, -1)
            };

            Color[] pix = new Color[1];
            pix[0] = color;
            Texture2D bg = new Texture2D(1, 1);
            bg.SetPixels(pix);
            bg.Apply();


            currentStyle.normal.background = bg;
            return currentStyle;
        }

        protected SerializedProperty Hygiene, Thirst, Temperature, Hungry, Coldness, Fatigue;

        private void OnEnable()
        {
            M = (ConditionSystem)target;
            script = MonoScript.FromMonoBehaviour(M);
            FindProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawAnimalInspector();
            serializedObject.ApplyModifiedProperties();
        }

        private void FindProperties()
        {
            Hygiene = serializedObject.FindProperty("hygiene");
            Thirst = serializedObject.FindProperty("thirst");
            Temperature = serializedObject.FindProperty("temperature");
            Hungry = serializedObject.FindProperty("hungry");
            Coldness = serializedObject.FindProperty("coldness");
            Fatigue = serializedObject.FindProperty("fatigue");
        }

        private void DrawAnimalInspector()
        {
            M.IsHuman = GUILayout.Toggle(M.IsHuman, new GUIContent("Is Human", "Is This Object an Human ? "), EditorStyles.miniButton);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.PropertyField(Thirst, new GUIContent("Thirst", "Thirst Value"));
            EditorGUILayout.PropertyField(Temperature, new GUIContent("Temperature", "Temperature Value"));
            EditorGUILayout.PropertyField(Hungry, new GUIContent("Hungry", "Hungry Value"));
            EditorGUILayout.PropertyField(Fatigue, new GUIContent("Fatigue", "Fatigue Value"));

            if (M.IsHuman)
            {
                EditorGUILayout.PropertyField(Hygiene, new GUIContent("Hygiene", "Hygiene Value"));
                EditorGUILayout.PropertyField(Coldness, new GUIContent("Coldness", "Coldness Value"));
            }

            EditorGUILayout.EndVertical();
        }
    }
}
