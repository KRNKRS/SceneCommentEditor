using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneCommentEditor;

namespace SceneCommentEditor
{
    [ExecuteInEditMode]
    public class CommentManager : MonoBehaviour
    {
        private List<GameObject> comments = new List<GameObject>();
        public bool resize = true;
        public int minFontSize = 1;
        public int maxFontSize = 11;
        public float thresholdDistance = 50;
        [SerializeField, HideInInspector]
        private AnimationCurve distanceCurve;

        void OnEnable()
        {
            this.gameObject.hideFlags = HideFlags.DontSaveInBuild;
            distanceCurve = distanceCurve ?? new AnimationCurve();
            if (distanceCurve.keys.Length == 0)
            {
                distanceCurve.AddKey(0, maxFontSize);
                distanceCurve.AddKey(1, minFontSize);
            }
        }

        public void AddComment()
        {
            var newObject = new GameObject("New Comment");
            var display = newObject.AddComponent<CommentDisplay>();
            newObject.transform.SetParent(this.transform);
            display.SetManager(this);
            newObject.gameObject.hideFlags = HideFlags.DontSaveInBuild;
            comments.Add(newObject);
        }

        public int GetFontSizeByDistance(Vector3 from, Vector3 to)
        {
            var distance = Vector3.Distance(from, to);
            var value = distance / thresholdDistance;
            return (int)distanceCurve.Evaluate(value);
        }
    }
}