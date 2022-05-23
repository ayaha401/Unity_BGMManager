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

        // AudioSourceの初期状態を設定
        private static void AudioSourceInit(AudioSource source, BGM_SObj data)
        {
            source.clip = data.audioClip;
            source.volume = data.clipVolume;
            source.playOnAwake = data.playOnAwake;
            source.loop = data.loop;
        }

        // ディクショナリにDSを追加
        private static void AddAudioSObj(BGM_SObj seData, GameObject soundPlayerObj, Dictionary<string, BGM_DS> _seDS_Dic)
        {
            AudioSource newAudioSource = soundPlayerObj.AddComponent<AudioSource>();
            AudioSourceInit(newAudioSource, seData);
            BGM_DS ds = new BGM_DS(newAudioSource, seData.name, seData.clipVolume);
            _seDS_Dic.Add(seData.name, ds);
        }

        // AudioSourceを生成
        public static void GenerateAudioSource(BGMListSObj list)
        {
            foreach (BGM_SObj data in list.bgmDatas)
            {
                if (bgmDS_Dic.ContainsKey(data.name)) continue;

                AddAudioSObj(data, rootObj, bgmDS_Dic);
            }
        }

        // 重複しないで再生
        public static void AudioPlay(string audioName)
        {
            if (!bgmDS_Dic.ContainsKey(audioName)) { return; }

            AudioSource source = bgmDS_Dic[audioName].audioSource;
            source.volume = volume * bgmDS_Dic[audioName].clipVolume;
            source.Play();
        }

        // 重複して再生可能
        public static void AudioPlayOneShot(string audioName)
        {
            if (!bgmDS_Dic.ContainsKey(audioName)) { return; }

            AudioSource source = bgmDS_Dic[audioName].audioSource;
            source.volume = volume * bgmDS_Dic[audioName].clipVolume;
            source.PlayOneShot(source.clip);
        }

        // 再生を停止させる
        public static void AudioStop(string audioName)
        {
            if (!bgmDS_Dic.ContainsKey(audioName)) { return; }
            AudioSource source = bgmDS_Dic[audioName].audioSource;
            source.Stop();
        }

        // 基本ボリュームを変更
        public static void SetVolume(float _volume)
        {
            float clampVolume = Mathf.Clamp(_volume, 0.0f, 1.0f);
            volume = clampVolume;
        }

        // 基本ボリュームを送る
        public static float GetVolume()
        {
            return volume;
        }
    }
}

