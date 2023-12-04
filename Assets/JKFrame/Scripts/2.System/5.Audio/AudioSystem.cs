using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JKFrame
{
    public static class AudioSystem
    {
        private static AudioModule audioModule;
        public static void Init()
        {
            audioModule = JKFrameRoot.RootTransform.GetComponentInChildren<AudioModule>();
            audioModule.Init();
        }
        public static float GlobalVolume { get => audioModule.GlobalVolume; set { audioModule.GlobalVolume = value; } }
        public static float BGVolume { get => audioModule.BGVolume; set { audioModule.BGVolume = value; } }
        public static float EffectVolume { get => audioModule.EffectVolume; set { audioModule.EffectVolume = value; } }
        public static bool IsMute { get => audioModule.IsMute; set { audioModule.IsMute = value; } }
        public static bool IsLoop { get => audioModule.IsLoop; set { audioModule.IsLoop = value; } }
        public static bool IsPause { get => audioModule.IsPause; set { audioModule.IsPause = value; } }

        /// <summary>
        /// ���ű�������
        /// </summary>
        /// <param name="clip">����Ƭ��</param>
        /// <param name="loop">�Ƿ�ѭ��</param>
        /// <param name="volume">������-1�������ã����õ�ǰ����</param>
        /// <param name="fadeOutTime">�����������ѵ�ʱ��</param>
        /// <param name="fadeInTime">�����������ѵ�ʱ��</param>
        public static void PlayBGAudio(AudioClip clip, bool loop = true, float volume = -1, float fadeOutTime = 0, float fadeInTime = 0)
            => audioModule.PlayBGAudio(clip, loop, volume, fadeOutTime, fadeInTime);

        /// <summary>
        /// ʹ����Ч���鲥�ű������֣��Զ�ѭ��
        /// </summary>
        /// <param name="fadeOutTime">�����������ѵ�ʱ��</param>
        /// <param name="fadeInTime">�����������ѵ�ʱ��</param>
        public static void PlayBGAudioWithClips(AudioClip[] clips, float volume = -1, float fadeOutTime = 0, float fadeInTime = 0)
            => audioModule.PlayBGAudioWithClips(clips, volume, fadeOutTime, fadeInTime);

        /// <summary>
        /// ֹͣ��������
        /// </summary>
        public static void StopBGAudio() => audioModule.StopBGAudio();

        /// <summary>
        /// ��ͣ��������
        /// </summary>
        public static void PauseBGAudio() => audioModule.PauseBGAudio();

        /// <summary>
        /// �������ű�������
        /// </summary>
        public static void UnPauseBGAudio() => audioModule.UnPauseBGAudio();

        /// <summary>
        /// ����һ����Ч����,���Ұ���ĳ����Ϸ��������
        /// ���ǲ��õ��ģ���Ϸ��������ʱ����˲�����󶨣�������Ч������
        /// </summary>
        /// <param name="clip">��ЧƬ��</param>
        /// <param name="autoReleaseClip">�������ʱ���Զ�����audioClip</param>
        /// <param name="component">�������</param>
        /// <param name="volumeScale">���� 0-1</param>
        /// <param name="is3d">�Ƿ�3D</param>
        /// <param name="callBack">�ص�����-�����ֲ�����ɺ�ִ��</param>
        public static void PlayOneShot(AudioClip clip, Component component = null, bool autoReleaseClip = false, float volumeScale = 1, bool is3d = true, Action callBack = null)
            => audioModule.PlayOneShot(clip, component, autoReleaseClip, volumeScale, is3d, callBack);

        /// <summary>
        /// ����һ����Ч����
        /// </summary>
        /// <param name="clip">��ЧƬ��</param>
        /// <param name="position">���ŵ�λ��</param>
        /// <param name="autoReleaseClip">�������ʱ���Զ�����audioClip</param>
        /// <param name="volumeScale">���� 0-1</param>
        /// <param name="is3d">�Ƿ�3D</param>
        /// <param name="callBack">�ص�����-�����ֲ�����ɺ�ִ��</param>
        public static void PlayOneShot(AudioClip clip, Vector3 position, bool autoReleaseClip = false, float volumeScale = 1, bool is3d = true, Action callBack = null)
            => audioModule.PlayOneShot(clip, position, autoReleaseClip, volumeScale, is3d, callBack);
    }
}