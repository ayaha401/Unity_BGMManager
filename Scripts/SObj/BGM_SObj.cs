using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGM
{
    [CreateAssetMenu(menuName = "BGMManager/BGMData")]
    public class BGM_SObj : ScriptableObject
    {
        [Header("‰¹Œ¹")]
        public AudioClip audioClip;

        [Header("‰¹—Ê")]
        [Range(0.0f, 1.0f)] public float clipVolume = 1.0f;

        [Header("‰¹Œ¹–¼")]
        public string clipName;
    }
}

