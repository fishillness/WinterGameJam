using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WinterGameJam
{
    [CreateAssetMenu()]
    public class MusicProperties : ScriptableObject
    {
        [SerializeField] private AudioClip[] musicFiles;
        public AudioClip this[MusicType s] => musicFiles[(int)s];

#if UNITY_EDITOR
        [CustomEditor(typeof(MusicProperties))]
        public class MusicInspector : Editor
        {
            private static readonly int musicCount = Enum.GetValues(typeof(MusicType)).Length;
            private new MusicProperties target => base.target as MusicProperties;
            public override void OnInspectorGUI()
            {
                if (target.musicFiles.Length < musicCount)
                {
                    Array.Resize(ref target.musicFiles, musicCount);
                }

                for (int i = 0; i < target.musicFiles.Length; i++)
                {
                    target.musicFiles[i] = EditorGUILayout.ObjectField(
                        $"{(MusicType)i}: ", target.musicFiles[i], typeof(AudioClip), false) as AudioClip;
                }

                EditorUtility.SetDirty(target);
            }
        }
#endif
    }
}
