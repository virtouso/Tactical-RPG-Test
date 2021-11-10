using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public abstract class BaseAudioManager : MonoBehaviour
{

    [Inject] private IUtilityFile _utilityFile;
    [SerializeField] private AudioMixer _audioMixer;
    [Inject] private ILogger _logger;

    
    public float MusicVolume
    {
        get
        {
            _audioMixer.GetFloat(MixerKeys.Music, out float value);
            return value;
        }
        set
        {
            _utilityFile.SaveFloat(MixerKeys.Music, value);
            _audioMixer.SetFloat(MixerKeys.Music, value);
        }
    }


    public float AfxVolume
    {
        get
        {
            _audioMixer.GetFloat(MixerKeys.Afx, out float value);
            return value;
        }
        set
        {
            _utilityFile.SaveFloat(MixerKeys.Afx, value);
            _audioMixer.SetFloat(MixerKeys.Afx, value);
        }
    }


    
    private void InitAfxVolume()
    {
        _audioMixer.SetFloat(MixerKeys.Afx, _utilityFile.LoadFloat(MixerKeys.Afx));
    }

    private void InitMusicVolume()
    {
       _logger.ShowNormalLog( _utilityFile.LoadFloat(MixerKeys.Music).ToString(),Color.blue, Channels.Ui);
        _audioMixer.SetFloat(MixerKeys.Music, _utilityFile.LoadFloat(MixerKeys.Music));

    }

    
    protected virtual void Start()
    {
        InitAfxVolume();
        InitMusicVolume();
    }
    
}



