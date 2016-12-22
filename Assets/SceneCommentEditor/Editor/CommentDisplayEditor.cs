using System.Text;
using UnityEngine;
using UnityEditor;
using SceneCommentEditor;

namespace SceneCommentEditor
{
    [CustomEditor(typeof(CommentDisplay))]
    [CanEditMultipleObjects]
    public class CommentDisplayEditor : Editor
    {
        private SerializedProperty text;
        private SerializedProperty buttonSize; 
        private Vector2 scroll;

        void OnEnable()
        {
            text = serializedObject.FindProperty("text");
            buttonSize = serializedObject.FindProperty("buttonSize");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                var inspectorFontSize = 14;
                var initLineNum = 3;
                var lineNum = initLineNum;
                EditorGUI.BeginChangeCheck();
                {
                    EditorGUILayout.LabelField("Comment");
                    var lineCount = text.stringValue.Split('\n').Length;
                    if(lineCount >= 2)
                    {
                        lineNum = lineCount + 1;
                    }
                    text.stringValue = EditorGUILayout.TextArea(text.stringValue, GUILayout.ExpandWidth(true), GUILayout.Height(inspectorFontSize * lineNum));
                }
                if (EditorGUI.EndChangeCheck())
                {
                    //TODO:ボタン色変更
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}