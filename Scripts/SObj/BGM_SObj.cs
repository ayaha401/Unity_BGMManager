using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGM
{
    [CreateAssetMenu(menuName = "BGMManager/BGMData")]
    public class BGM_SObj : ScriptableObject
    {
        [Header("âπåπ")]
        public AudioClip audioClip;

        [Header("âπó ")]
        [Range(0.0f, 1.0f)] public float clipVolume = 1.0f;

        [Header("âπåπê›íË")]
        public bool playOnAwake = true;
        public bool loop = true;
    }
}

