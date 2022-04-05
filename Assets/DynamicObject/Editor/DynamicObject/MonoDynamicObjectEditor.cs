#if UNITY_EDITOR
using DynamicObjects.Unity;
using UnityEditor;
using UnityEngine;

namespace DynamicObjects.UnityEditor
{
    [CustomEditor(typeof(MonoDynamicObject))]
    public sealed class MonoDynamicObjectEditor : Editor
    {
        private MonoDynamicObject dynamicObject;

        private SerializedProperty components;

        private SerializedProperty initializeOnAwake;

        private bool showProperties = true;

        private bool showMethods = true;

        private bool showEvents = true;

        private Color color;

        private bool editMode;

        private void OnEnable()
        {
            this.dynamicObject = (MonoDynamicObject) this.target;
            this.components = this.serializedObject.FindProperty(nameof(this.components));
            this.initializeOnAwake = this.serializedObject.FindProperty(nameof(this.initializeOnAwake));
            
            ColorUtility.TryParseHtmlString("#FF6235", out this.color);
            this.editMode = !EditorApplication.isPlayingOrWillChangePlaymode;
            EditorApplication.playModeStateChanged += this.OnPlayModeChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged += this.OnPlayModeChanged;
        }

        private void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                this.editMode = true;
            }

            if (state == PlayModeStateChange.ExitingEditMode)
            {
                this.editMode = false;
            }
        }

        public override void OnInspectorGUI()
        {
            if (this.editMode)
            {
                this.UpdateObjectInEditor();
            }

            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(this.initializeOnAwake);
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(this.components, includeChildren: true);
            EditorGUILayout.Space(8);
            GUI.enabled = false;
            this.DrawProperties();
            this.DrawHorizontalLine();
            this.DrawMethods();
            this.DrawHorizontalLine();
            this.DrawEvents();
            GUI.enabled = true;
        }

        private void UpdateObjectInEditor()
        {
            try
            {
                this.dynamicObject.UpdateInEditor();
            }
            catch (MonoDynamicObject.ComponentException exeption)
            {
                EditorGUILayout.HelpBox($"Fix Component: {exeption.Component.GetType().Name}", MessageType.Error);
            }
        }

        private void DrawProperties()
        {
            this.showProperties = EditorGUILayout.Foldout(this.showProperties, "Properties", EditorStyles.foldout);
            if (this.showProperties)
            {
                var properties = this.dynamicObject.Info.GetPropertyDefinitions();
                foreach (var definition in properties)
                {
                    EditorGUILayout.TextField(definition.ToString(), EditorStyles.textField);
                }
            }
        }

        private void DrawMethods()
        {
            this.showMethods = EditorGUILayout.Foldout(this.showMethods, "Methods", EditorStyles.foldout);
            if (this.showMethods)
            {
                var methods = this.dynamicObject.Info.GetMethodDefinitions();
                foreach (var definition in methods)
                {
                    EditorGUILayout.TextField(definition.ToString(), EditorStyles.textField);
                }
            }
        }

        private void DrawEvents()
        {
            this.showEvents = EditorGUILayout.Foldout(this.showEvents, "Events", EditorStyles.foldout);
            if (this.showEvents)
            {
                var events = this.dynamicObject.Info.GetEventDefinitions();
                foreach (var definition in events)
                {
                    EditorGUILayout.TextField(definition.ToString(), EditorStyles.textField);
                }
            }
        }

        private void DrawHorizontalLine()
        {
            GUILayout.Space(4);
            var rect = EditorGUILayout.GetControlRect(false, 1);
            rect.height = 1;
            this.color.a = 0.25f;
            EditorGUI.DrawRect(rect, this.color);
        }
    }
}
#endif