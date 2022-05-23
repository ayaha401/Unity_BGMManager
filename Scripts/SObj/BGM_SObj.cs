using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGM
{
    [CreateAssetMenu(menuName = "BGMManager/BGMData")]
    public class BGM_SObj : ScriptableObject
    {
        [Header("����")]
        public AudioClip audioClip;

        [Header("����")]
        [Range(0.0f, 1.0f)] public float clipVolume = 1.0f;

        [Header("�����ݒ�")]
        public bool playOnAwake = true;
        public bool loop = true;
    }
}

