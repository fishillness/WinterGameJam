using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace WinterGameJam
{
    [CreateAssetMenu()]
    public class SoundProperties : ScriptableObject
    {
        [SerializeField] private AudioClip[] sounds;
        public AudioClip this[SoundType s] => sounds[(int)s];

#if UNITY_EDITOR
        [CustomEditor(typeof(SoundProperties))]
        public class SoundsInspector : Editor
        {
            private static readonly int soundsCount = Enum.GetValues(typeof(SoundType)).Length;
            private new SoundProperties target => base.target as SoundProperties;
            public override void OnInspectorGUI()
            {
                if (target.sounds.Length < soundsCount)
                {
                    Array.Resize(ref target.sounds, soundsCount);
                }

                for (int i = 0; i < target.sounds.Length; i++)
                {
                    target.sounds[i] = EditorGUILayout.ObjectField(
                        $"{(SoundType)i}: ", target.sounds[i], typeof(AudioClip), false) as AudioClip;
                }

                EditorUtility.SetDirty(target);
            }
        }
#endif
    }
}