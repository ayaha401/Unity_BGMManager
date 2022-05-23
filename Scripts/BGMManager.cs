using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGM
{
    public struct BGM_DS
    {
        public AudioSource audioSource;
        public string audioName;
        public float clipVolume;
        
        public BGM_DS(AudioSource source, string name, float volume)
        {
            audioSource = source;
            audioName = name;
            clipVolume = volume;
        }
    }

    public class BGMManager : MonoBehaviour
    {
        private static GameObject rootObj = null;
        private static Dictionary<string, BGM_DS> bgmDS_Dic = new Dictionary<string, BGM_DS>();
        private static float volume = 0.5f;

        private void Awake()
        {
            rootObj = this.gameObject;
        }

        // AudioSource�̏�����Ԃ�ݒ�
        private static void AudioSourceInit(AudioSource source, BGM_SObj data)
        {
            source.clip = data.audioClip;
            source.volume = data.clipVolume;
            source.playOnAwake = data.playOnAwake;
            source.loop = data.loop;
        }

        // �f�B�N�V���i����DS��ǉ�
        private static void AddAudioSObj(BGM_SObj seData, GameObject soundPlayerObj, Dictionary<string, BGM_DS> _seDS_Dic)
        {
            AudioSource newAudioSource = soundPlayerObj.AddComponent<AudioSource>();
            AudioSourceInit(newAudioSource, seData);
            BGM_DS ds = new BGM_DS(newAudioSource, seData.name, seData.clipVolume);
            _seDS_Dic.Add(seData.name, ds);
        }

        // AudioSource�𐶐�
        public static void GenerateAudioSource(BGMListSObj list)
        {
            foreach (BGM_SObj data in list.bgmDatas)
            {
                if (bgmDS_Dic.ContainsKey(data.name)) continue;

                AddAudioSObj(data, rootObj, bgmDS_Dic);
            }
        }

        // �d�����Ȃ��ōĐ�
        public static void AudioPlay(string audioName)
        {
            if (!bgmDS_Dic.ContainsKey(audioName)) { return; }

            AudioSource source = bgmDS_Dic[audioName].audioSource;
            source.volume = volume * bgmDS_Dic[audioName].clipVolume;
            source.Play();
        }

        // �d�����čĐ��\
        public static void AudioPlayOneShot(string audioName)
        {
            if (!bgmDS_Dic.ContainsKey(audioName)) { return; }

            AudioSource source = bgmDS_Dic[audioName].audioSource;
            source.volume = volume * bgmDS_Dic[audioName].clipVolume;
            source.PlayOneShot(source.clip);
        }

        // �Đ����~������
        public static void AudioStop(string audioName)
        {
            if (!bgmDS_Dic.ContainsKey(audioName)) { return; }
            AudioSource source = bgmDS_Dic[audioName].audioSource;
            source.Stop();
        }

        // ��{�{�����[����ύX
        public static void SetVolume(float _volume)
        {
            float clampVolume = Mathf.Clamp(_volume, 0.0f, 1.0f);
            volume = clampVolume;
        }

        // ��{�{�����[���𑗂�
        public static float GetVolume()
        {
            return volume;
        }
    }
}

