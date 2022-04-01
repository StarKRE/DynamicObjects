using DynamicObjects.Unity;
using UnityEditor;

namespace DynamicObjects.UnityEditor
{
    [CustomEditor(typeof(MonoObjectProxy))]
    public sealed class MonoObjectProxyEditor : Editor
    {
        private SerializedProperty monoObject;

        private void OnEnable()
        {
            this.monoObject = this.serializedObject.FindProperty(nameof(this.monoObject));
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(this.monoObject);
        }
    }
}