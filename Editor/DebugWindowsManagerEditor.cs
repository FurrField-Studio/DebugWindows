using CheatWindows.Scripts;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(DebugWindowsManager))]
    public class DebugWindowsManagerEditor : UnityEditor.Editor
    {
        private static class Styles
        {
            public static GUIStyle boldLabel = new GUIStyle("MiniBoldLabel");
        }
        
        private SerializedProperty _keyToToggleImGuiProperty;
        private SerializedProperty _resetMouseSpriteProperty;
        
        private SerializedProperty _mainWindowPrefabProperty;
        private SerializedProperty _debugWindowsPrefabsProperty;
        
        private SerializedProperty _onImGuiActiveProperty;
        
        private GUIContent _settings = new GUIContent("Settings");
        private GUIContent _windows = new GUIContent("Windows");
        private GUIContent _events = new GUIContent("Events");
        
        protected void OnEnable()
        {
            _keyToToggleImGuiProperty = serializedObject.FindProperty("_keyToToggleImGui");
            _resetMouseSpriteProperty = serializedObject.FindProperty("_resetMouseSprite");
            
            _mainWindowPrefabProperty = serializedObject.FindProperty("_mainWindowPrefab");
            _debugWindowsPrefabsProperty = serializedObject.FindProperty("_debugWindowsPrefabs");

            _onImGuiActiveProperty = serializedObject.FindProperty("_onImGuiActive");
        }
        
         public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            // Settings section.
            EditorGUILayout.LabelField(_settings, Styles.boldLabel);
            using(new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_keyToToggleImGuiProperty);
                EditorGUILayout.PropertyField(_resetMouseSpriteProperty);
            }

            // Windows section.
            EditorGUILayout.LabelField(_windows, Styles.boldLabel);
            using(new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_mainWindowPrefabProperty);
                EditorGUILayout.PropertyField(_debugWindowsPrefabsProperty);
            }
            
            // Events section.
            EditorGUILayout.LabelField(_events, Styles.boldLabel);
            using(new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_onImGuiActiveProperty);
            }

            if(EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}