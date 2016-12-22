using UnityEngine;
using UnityEditor;
using SceneCommentEditor;

namespace SceneCommentEditor
{
    public class CommentAddMenu
    {
        private static CommentManager manager;

        [MenuItem("GameObject/Create Scene Comment", false, 0)]
        public static void Create()
        {
            if(manager == null)
            {
                var obj = GameObject.Find("SceneCommentEditor");
                if (obj == null)
                {
                    obj = new GameObject("SceneCommentEditor");
                    manager = obj.AddComponent<CommentManager>();
                }
                if(obj.GetComponent<CommentManager>() == null)
                {
                    manager = obj.AddComponent<CommentManager>();
                }
                manager = obj.GetComponent<CommentManager>();
            }
            manager.AddComment();
        }
    }
}