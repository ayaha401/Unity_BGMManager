using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGM
{
    public struct BGM_DS
    {
        public AudioClip clip;
        public float clipVolume;
        public string clipName;

        public BGM_DS(AudioClip source, string name, float volume)
        {
            clip = source;
            clipName = name;
            clipVolume = volume;
        }
    }

    public class BGMManager : MonoBehaviour
    {
        [SerializeField] private BGMListSObj _bgmList = null;

        private static BGMListSObj _staticBGMListSObj = null;
        private static Dictionary<string, BGM_DS> _bgmDic = new Dictionary<string, BGM_DS>();

        private static AudioSource _audioSource = null;

        private static bool _managerMaked = false;
        private static float _volume = 0.5f;

        private void Awake()
        {
            if (_managerMaked == false)
            {
                DontDestroyOnLoad(this);
                _managerMaked = true;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            _staticBGMListSObj = _bgmList;
            MakeDictionary();
            _audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
        
        }

        private static void MakeDictionary()
        {
            foreach (BGM_SObj data in _staticBGMListSObj.bgmDatas)
            {
                if (_bgmDic.ContainsKey(data.clipName) == false)
                {
                    BGM_DS bgmDS = new BGM_DS(data.audioClip, data.clipName, data.clipVolume);
                    _bgmDic.Add(data.clipName, bgmDS);
                }
            }
        }

        // BGM���Đ�
        public static void BGMPlay(string bgmName)
        {
            if (_bgmDic.ContainsKey(bgmName) == false)
            {
                Debug.Log("�Ή�����string : " + bgmName + "������܂���");
                return;
            }

            _audioSource.mute = false;
            _audioSource.clip = _bgmDic[bgmName].clip;
            _audioSource.volume = _bgmDic[bgmName].clipVolume * _volume;
            _audioSource.Play();
        }

        // BGM���~
        public static void BGMStop()
        {
            _audioSource.mute = true;
        }

        // ��{�{�����[����ύX
        public static void SetVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0.0f, 1.0f);
            _volume = volume;
        }

        // ��{�{�����[���𑗂�
        public static float GetVolume()
        {
            return _volume;
        }
    }
}

