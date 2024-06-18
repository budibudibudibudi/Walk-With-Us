using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixerGroup musicMixer;
    [SerializeField] AudioMixerGroup sfxMixer;
    [SerializeField] Audio[] sounds;

    public static AudioManager instance;

    private float musicVolume;
    private float sfxVolume;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (var item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.audioclip;
            item.source.loop = item.isloop;
            item.source.volume = item.volume;

            switch (item.audiotype)
            {
                case Audio.AudioType.soundEffect:
                    sfxVolume = item.volume;
                    item.source.outputAudioMixerGroup = sfxMixer;
                    sfxMixer.audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
                    break;
                case Audio.AudioType.music:
                    musicVolume = item.volume;
                    item.source.outputAudioMixerGroup = musicMixer;
                    musicMixer.audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
                    break;
            }
            if (item.playonawake)
                item.source.Play();
        }
    }

    public float GetVolume(Audio.AudioType audioType)
    {
        switch (audioType)
        {
            case Audio.AudioType.soundEffect:
                return sfxVolume;
            case Audio.AudioType.music:
                return musicVolume;
        }
        return 0;
    }
    public void SetVolume(Audio.AudioType audioType, float amount)
    {
        switch (audioType)
        {
            case Audio.AudioType.soundEffect:
                sfxVolume = amount;
                sfxMixer.audioMixer.SetFloat("SFX", Mathf.Log10(amount) * 20);
                break;
            case Audio.AudioType.music:
                musicVolume = amount;
                musicMixer.audioMixer.SetFloat("Music", Mathf.Log10(amount) * 20);
                break;
        }
    }

    public void PlayMusic(string _clipName)
    {
        Audio temp = Array.Find(sounds, t => t.clipname == _clipName);
        temp?.source.Play();
    }
    public void StopMusic(string _clipName)
    {
        Audio temp = Array.Find(sounds, t => t.clipname == _clipName);
        temp?.source.Stop();
    }
    public void PauseMusic(string _clipName)
    {
        Audio temp = Array.Find(sounds, t => t.clipname == _clipName);
        temp?.source.Pause();
    }
    public void UnpauseMusic(string _clipName)
    {
        Audio temp = Array.Find(sounds, t => t.clipname == _clipName);
        temp?.source.UnPause();
    }
    public bool CheckAudioIsPlaying(string _clipName)
    {
        Audio temp = Array.Find(sounds, t => t.clipname == _clipName);
        if (temp == null)
        {
            Debug.LogError("Audio tidak ditemukan");
            return false;
        }
        return temp.source.isPlaying;
    }
    public void StopAllMusic()
    {
        foreach (var item in sounds)
        {
            item.source.Stop();
        }
    }
}