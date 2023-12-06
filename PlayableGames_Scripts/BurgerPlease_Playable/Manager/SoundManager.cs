using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public enum eSoundName
    {
        NONE = 0,
        UPGRADE = 1,
        CLICK = 2,
        CLICKBLOCK = 3,
        CASH = 4,
    }
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private bool _isBGM = true;
        [SerializeField] private AudioSource            _BGMSource;
        [SerializeField] private AudioSource            _SFXSource;
        [SerializeField] private AudioSource            _moneySFXSource;
        [SerializeField] private List<AudioClip>        _audioClipList;

        public  static SoundManager NullableInstance => _instance;
        private static SoundManager                     _instance;

        private Dictionary<eSoundName,AudioClip>        _dicAudioClip = new Dictionary<eSoundName,AudioClip>();
        private bool                                    _isGameEnded;

        public  System.Action                           OnPlayBGM        {get; private set;}
        public  System.Action<eSoundName>               OnPlaySFX        {get; private set;}
        public  System.Action                           OnStopBGMAndSFX  {get; private set;} 
        //네이밍 규칙 분명히

        private void Awake() 
        {
            if(_instance == null)
            _instance = this;

            OnPlayBGM = PlayBGM;      
            OnPlaySFX = PlaySFX;
            OnStopBGMAndSFX = StopAllSound;

            AudioClipsToDictionary();
        }

        private void AudioClipsToDictionary()
        {
            foreach (var audioClip in _audioClipList)
            {
                var soundName = eSoundName.NONE;

                foreach (eSoundName enumName in System.Enum.GetValues(typeof(eSoundName)))
                {
                    if (audioClip.name.Equals(enumName.ToString(), System.StringComparison.OrdinalIgnoreCase))
                    {
                        soundName = enumName;
                        break;
                    }
                }
                _dicAudioClip[soundName] = audioClip;
            }
        }

        private void PlayBGM()
        {
            if(_isBGM)
            _BGMSource.Play();
        }

        private void PlaySFX(eSoundName soundName)
        {
            if(_isGameEnded) return;

            var clip = _dicAudioClip[soundName];

            if(soundName == eSoundName.CASH)
            _moneySFXSource.PlayOneShot(clip);
            else
            _SFXSource.PlayOneShot(clip);
        }

        private void StopAllSound()
        {
            _isGameEnded = true;
            _SFXSource.Stop();
            // StartCoroutine(CoStartDimBGM());
        }

        // private IEnumerator CoStartDimBGM()
        // {
        //     while(_BGMSource.volume > 0)
        //     {
        //         _BGMSource.volume -= Time.fixedDeltaTime;
        //         yield return null;
        //     }
        //     _BGMSource.Stop();  
        // }
    }
}

