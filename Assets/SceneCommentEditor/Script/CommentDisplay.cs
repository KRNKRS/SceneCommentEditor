using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using SceneCommentEditor;

namespace SceneCommentEditor
{
    [ExecuteInEditMode]
    public class CommentDisplay : MonoBehaviour
    {
        private CommentManager manager;
        [SerializeField]
        private string text = "New Comment";
        private GUISkin windowButtonSkin;
        private GUISkin initButtonSkin;
        private GUIStyle localStyle;
        [SerializeField, HideInInspector]
        private Vector2 buttonSize = new Vector2(100, 19);

        public void SetManager(CommentManager _manager)
        {
            manager = _manager;
        }

#if UNITY_EDITOR

        void Awake()
        {
            if(manager == null)
            {
                var obj = GameObject.Find("SceneCommentEditor");
                if (obj == null)
                {
                    obj = new GameObject("SceneCommentEditor");
                    manager = obj.AddComponent<CommentManager>();
                }
                if (obj.GetComponent<CommentManager>() == null)
                {
                    manager = obj.AddComponent<CommentManager>();
                }
                manager = obj.GetComponent<CommentManager>();
            }
            windowButtonSkin = Resources.Load("CommentWindowButton") as GUISkin;
            initButtonSkin = Resources.Load("InitializeButtonSkin") as GUISkin;
        }

        void Start()
        {
            UnityEditor.SceneView.onSceneGUIDelegate += OnSceneView;
            var labelStyle = windowButtonSkin.GetStyle("label");
            localStyle = new GUIStyle();
            localStyle.normal.textColor = labelStyle.normal.textColor;
            localStyle.margin = labelStyle.margin;
            localStyle.padding = labelStyle.padding;
            localStyle.fontSize = 11;
        }

        void OnDestroy()
        {
            UnityEditor.SceneView.onSceneGUIDelegate -= OnSceneView;
        }

        void OnSceneView(SceneView sceneView)
        {
            var sceneCamera = SceneView.currentDrawingSceneView.camera;
            var pos = sceneCamera.WorldToScreenPoint(this.transform.position);

            Handles.BeginGUI();
            {
                var buttonStyle = windowButtonSkin.GetStyle("button");
                var margin = buttonStyle.margin.left * 2;
                localStyle.fontSize = manager.GetFontSizeByDistance(this.transform.position, sceneCamera.transform.position);
                var width = localStyle.CalcSize(new GUIContent(text)).x + localStyle.fontSize + margin;
                var height = localStyle.CalcSize(new GUIContent(text)).y + localStyle.fontSize + margin;

                var buttonRect = new Rect(pos.x, SceneView.currentDrawingSceneView.position.height - pos.y, width, height);
                if (GUI.Button(buttonRect, "", buttonStyle))
                {
                    EditorGUIUtility.PingObject(this.gameObject);
                }
                var labelRect = new Rect(buttonRect.x + margin, buttonRect.y, width, height);
                GUI.Label(labelRect, text, localStyle);
            }
            Handles.EndGUI();
        }
#endif
    }
}