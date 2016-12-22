using UnityEditor;
using SceneCommentEditor;

namespace SceneCommentEditor
{
    [CustomEditor(typeof(CommentManager))]
    [CanEditMultipleObjects]
    public class CommentManagerEditor : Editor
    {
        private SerializedProperty minFontSize;
        private SerializedProperty maxFontSize;
        private SerializedProperty thresholdDistance;
        private SerializedProperty distanceCurve;

        void OnEnable()
        {
            minFontSize = serializedObject.FindProperty("minFontSize");
            maxFontSize = serializedObject.FindProperty("maxFontSize");
            thresholdDistance = serializedObject.FindProperty("thresholdDistance");
            distanceCurve = serializedObject.FindProperty("distanceCurve");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                EditorGUI.BeginChangeCheck();
                {
                    EditorGUILayout.PropertyField(minFontSize);
                    EditorGUILayout.PropertyField(maxFontSize);
                    EditorGUILayout.PropertyField(thresholdDistance);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    distanceCurve.animationCurveValue.keys[0].time = 0;
                    distanceCurve.animationCurveValue.keys[0].value = maxFontSize.intValue;
                    distanceCurve.animationCurveValue.keys[1].time = 0;
                    distanceCurve.animationCurveValue.keys[1].value = minFontSize.intValue;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}