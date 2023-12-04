using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JKFrame
{
    public class AudioModule : MonoBehaviour
    {
        private static GameObjectPoolModule poolModule;

        [SerializeField, LabelText("�������ֲ�����")]
        private AudioSource BGAudioSource;
        [SerializeField, LabelText("Ч��������Ԥ����")]
        private GameObject EffectAudioPlayPrefab;
        [SerializeField, LabelText("�����Ԥ�貥��������")]
        private int EffectAudioDefaultQuantity = 20;

        //��������Ч��������Ч���ֲ�����
        private List<AudioSource> audioPlayList;

        #region ���������ſ���
        [SerializeField, Range(0, 1), OnValueChanged("UpdateAllAudioPlay")]
        private float globalVolume;
        public float GlobalVolume
        {
            get => globalVolume;
            set
            {
                if (globalVolume == value) return;
                globalVolume = value;
                UpdateAllAudioPlay();
            }
        }
        [SerializeField]
        [Range(0, 1)]
        [OnValueChanged("UpdateBGAudioPlay")]
        private float bgVolume;
        public float BGVolume
        {
            get => bgVolume;
            set
            {
                if (bgVolume == value) return;
                bgVolume = value;
                UpdateBGAudioPlay();
            }
        }
        [SerializeField]
        [Range(0, 1)]
        [OnValueChanged("UpdateEffectAudioPlay")]
        private float effectVolume;
        public float EffectVolume
        {
            get => effectVolume;
            set
            {
                if (effectVolume == value) return;
                effectVolume = value;
                UpdateEffectAudioPlay();
            }
        }
        [SerializeField]
        [OnValueChanged("UpdateMute")]
        private bool isMute = false;
        public bool IsMute
        {
            get => isMute;
            set
            {
                if (isMute == value) return;
                isMute = value;
                UpdateMute();
            }
        }
        [SerializeField]
        [OnValueChanged("UpdateLoop")]
        private bool isLoop = true;
        public bool IsLoop
        {
            get => isLoop;
            set
            {
                if (isLoop == value) return;
                isLoop = value;
                UpdateLoop();
            }
        }
        [SerializeField]
        [OnValueChanged("UpdatePause")]
        private bool isPause = false;
        public bool IsPause
        {
            get => isPause;
            set
            {
                if (isPause == value) return;
                isPause = value;
                UpdatePause();
            }
        }
        /// <summary>
        /// ����ȫ������������
        /// </summary>
        private void UpdateAllAudioPlay()
        {
            UpdateBGAudioPlay();
            UpdateEffectAudioPlay();
        }
        /// <summary>
        /// ���±�������
        /// </summary>
        private void UpdateBGAudioPlay()
        {
            BGAudioSource.volume = bgVolume * globalVolume;
        }
        /// <summary>
        /// ������Ч���ֲ�����
        /// </summary>
        private void UpdateEffectAudioPlay()
        {
            if (audioPlayList == null) return;
            //�������
            for (int i = audioPlayList.Count - 1; i >= 0; i--)
            {
                if (audioPlayList[i] != null)
                {
                    SetEffectAudioPlay(audioPlayList[i]);
                }
                else
                {
                    audioPlayList.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// ������Ч���ֲ�����
        /// </summary>
        /// <param name="audioSource"></param>
        private void SetEffectAudioPlay(AudioSource audioPlay, float spatial = -1)
        {
            audioPlay.mute = isMute;
            audioPlay.volume = effectVolume * globalVolume;
            if (spatial != -1)
            {
                audioPlay.spatialBlend = spatial;
            }
            if (isPause)
            {
                audioPlay.Pause();
            }
            else
            {
                audioPlay.UnPause();
            }
        }
        /// <summary>
        /// ����ȫ�����־������
        /// </summary>
        private void UpdateMute()
        {
            BGAudioSource.mute = isMute;
            UpdateEffectAudioPlay();
        }
        /// <summary>
        /// ���±�������ѭ��
        /// </summary>
        private void UpdateLoop()
        {
            BGAudioSource.loop = isLoop;
        }
        /// <summary>
        /// ���±���������ͣ
        /// </summary>
        private void UpdatePause()
        {
            if (isPause)
            {
                BGAudioSource.Pause();
            }
            else
            {
                BGAudioSource.UnPause();
            }
        }
        #endregion

        public void Init()
        {
            Transform poolRoot = new GameObject("AudioPlayerPoolRoot").transform;
            poolRoot.SetParent(transform);
            poolModule = new GameObjectPoolModule();
            poolModule.Init(poolRoot);
            poolModule.InitObjectPool(EffectAudioPlayPrefab, -1, EffectAudioDefaultQuantity);
            audioPlayList = new List<AudioSource>(EffectAudioDefaultQuantity);
            audioPlayRoot = new GameObject("audioPlayRoot").transform;
            audioPlayRoot.SetParent(transform);
            UpdateAllAudioPlay();
        }

        #region ��������
        private static Coroutine fadeCoroutine;

        public void PlayBGAudio(AudioClip clip, bool loop = true, float volume = -1, float fadeOurTime = 0, float fadeInTime = 0)
        {
            IsLoop = loop;
            if (volume != -1)
            {
                BGVolume = volume;
            }
            fadeCoroutine = StartCoroutine(DoVolumeFade(clip, fadeOurTime, fadeInTime));
        }

        private IEnumerator DoVolumeFade(AudioClip clip, float fadeOurTime, float fadeInTime)
        {
            float currTime = 0;
            if (fadeOurTime <= 0) fadeOurTime = 0.0001f;
            if (fadeInTime <= 0) fadeInTime = 0.0001f;

            //����������Ҳ���ǵ���
            while (currTime < fadeOurTime)
            {
                yield return CoroutineTool.WaitForFrames();
                if (!isPause) currTime += Time.deltaTime;
                float ratio = Mathf.Lerp(1, 0, currTime / fadeOurTime);
                BGAudioSource.volume = bgVolume * globalVolume * ratio;
            }

            BGAudioSource.clip = clip;
            BGAudioSource.Play();
            currTime = 0;
            //���������Ҳ���ǵ���
            while (currTime < fadeInTime)
            {
                yield return CoroutineTool.WaitForFrames();
                if (!IsPause) currTime += Time.deltaTime;
                float ratio = Mathf.InverseLerp(0, 1, currTime / fadeInTime);
                BGAudioSource.volume = bgVolume * globalVolume * ratio;
            }
            fadeCoroutine = null;
        }

        private static Coroutine bgWithClipCoroutine;
        /// <summary>
        /// ʹ����Ч���鲥�ű������֣��Զ�ѭ��
        /// </summary>
        /// <param name="clips"></param>
        /// <param name="volume"></param>
        /// <param name="fadeOutTime"></param>
        /// <param name="fadeInTime"></param>
        public void PlayBGAudioWithClips(AudioClip[] clips, float volume = -1, float fadeOutTime = 0, float fadeInTime = 0)
        {
            bgWithClipCoroutine = MonoSystem.Start_Coroutine(DoPlayBGAudioWithClips(clips, volume));
        }

        private IEnumerator DoPlayBGAudioWithClips(AudioClip[] clips, float volume = -1, float fadeOutTime = 0, float fadeInTime = 0)
        {
            if (volume != -1) BGVolume = volume;
            int currIndex = 0;
            while (true)
            {
                AudioClip clip = clips[currIndex];
                fadeCoroutine = StartCoroutine(DoVolumeFade(clip, fadeOutTime, fadeInTime));
                float time = clip.length;
                //ʱ��ֻҪ���ã�һֱ���
                while (time > 0)
                {
                    yield return CoroutineTool.WaitForFrames();
                    if (!isPause) time -= Time.deltaTime;
                }
                //��������˵������ʱ�������޸������ţ��������Whileѭ��
                currIndex++;
                if (currIndex >= clips.Length) currIndex = 0;
            }
        }

        public void StopBGAudio()
        {
            if (bgWithClipCoroutine != null) MonoSystem.Stop_Coroutine(bgWithClipCoroutine);
            if (fadeCoroutine != null) MonoSystem.Stop_Coroutine(fadeCoroutine);
            BGAudioSource.Stop();
            BGAudioSource.clip = null;
        }
        public void PauseBGAudio()
        {
            IsPause = true;
        }
        public void UnPauseBGAudio()
        {
            IsPause = false;
        }
        #endregion

        #region ��Ч����
        private Transform audioPlayRoot;
        /// <summary>
        /// ��ȡ���ֲ�����
        /// </summary>
        /// <param name="is3D"></param>
        /// <returns></returns>
        private AudioSource GetAudioPlay(bool is3D = true)
        {
            //�Ӷ�����л�ȡ������
            GameObject audioPlay = poolModule.GetObject("AudioPlay", audioPlayRoot);
            if (audioPlay.IsNull())
            {
                audioPlay = GameObject.Instantiate(EffectAudioPlayPrefab, audioPlayRoot);
                audioPlay.name = EffectAudioPlayPrefab.name;
            }
            AudioSource audioSource = audioPlay.GetComponent<AudioSource>();
            SetEffectAudioPlay(audioSource, is3D ? 1f : 0f);
            audioPlayList.Add(audioSource);
            return audioSource;
        }
        /// <summary>
        /// ���ղ�����
        /// </summary>
        /// <param name="audioSource"></param>
        /// <param name="clip"></param>
        /// <param name="autoReleaseClip"></param>
        /// <param name="callBack"></param>
        private void RecycleAudioPlay(AudioSource audioSource, AudioClip clip, bool autoReleaseClip, Action callBack)
        {
            StartCoroutine(DoRecycleAudioPlay(audioSource, clip, autoReleaseClip, callBack));
        }

        private IEnumerator DoRecycleAudioPlay(AudioSource audioSource, AudioClip clip, bool autoReleaseClip, Action callBack)
        {
            //�ӳ� Clip �ĳ��ȣ��룩
            yield return CoroutineTool.WaitForSeconds(clip.length);
            //�Żس���
            if (audioSource != null)
            {
                audioPlayList.Remove(audioSource);
                poolModule.PushObject(audioSource.gameObject);
                if (autoReleaseClip) ResSystem.UnloadAsset(clip);
                callBack?.Invoke();
            }
        }

        public void PlayOneShot(AudioClip clip, Component component = null, bool autoReleaseClip = false, float volumeScale = 1, bool is3d = true, Action callBack = null)
        {
            //��ʼ�����ֲ�����
            AudioSource audioSource = GetAudioPlay(is3d);
            if (component == null) audioSource.transform.SetParent(null);
            else
            {
                audioSource.transform.SetParent(component.transform);
                audioSource.transform.localPosition = Vector3.zero;
                //��������ʱ���ͷŸ�����
                component.OnDestroy(OnOwerDestory, audioSource);
            }
            //����һ����Ч
            audioSource.PlayOneShot(clip, volumeScale);
            //�����������Լ��ص�����
            callBack += () => PlayOverRemoveOwnerDesotryAction(component);         // ���Ž���ʱ�Ƴ���������Action
            RecycleAudioPlay(audioSource, clip, autoReleaseClip, callBack);
        }

        //��������ʱ����ǰ����
        private void OnOwerDestory(GameObject go, AudioSource audioSource)
        {
            audioSource.transform.SetParent(audioPlayRoot);
        }
        //���Ž���ʱ�Ƴ���������Action
        private void PlayOverRemoveOwnerDesotryAction(Component owner)
        {
            if (owner != null) owner.RemoveOnDestroy<AudioSource>(OnOwerDestory);
        }

        public void PlayOneShot(AudioClip clip, Vector3 position, bool autoReleaseClip = false, float volumeScale = 1, bool is3d = true, Action callBack = null)
        {
            //��ʼ�����ֲ�����
            AudioSource audioSource = GetAudioPlay(is3d);
            audioSource.transform.position = position;

            //����һ����Ч
            audioSource.PlayOneShot(clip, volumeScale);
            //�����������Լ��ص�����
            RecycleAudioPlay(audioSource, clip, autoReleaseClip, callBack);
        }
        #endregion
    }
}
