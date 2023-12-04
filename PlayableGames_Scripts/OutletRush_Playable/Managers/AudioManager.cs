using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public enum EAudioName
    {
        NONE = -1,
        StackSound,
        MoneyStackSound,
        UpgradeSound,
        CheckOutSound,
        MoneyTakeSound
    }

    public class AudioManager : MonoBehaviour
    {
        // [LunaPlaygroundField("BGM_On/OFF (Default : On)")] 
        [SerializeField] private bool _isBGM = false;

        [Header("=====AudioManager SerializeField=====")]
        [Space]
        [SerializeField] private List<AudioClip>        _audioClipList = null;
        [SerializeField] private AudioSource            _BGMSource = null;
        [SerializeField] private AudioSource            _stackSFXSource = null;
        [SerializeField] private AudioSource            _displayStackSFXSource = null;
        [SerializeField] private AudioSource            _moneySFXSource = null;
        [SerializeField] private AudioSource            _moneyStackSFXSource = null;
        [SerializeField] private AudioSource            _CheckoutSFXSource = null;
        [SerializeField] private AudioSource            _UpgradeSFXSource = null;
        [SerializeField] private AudioSource            _moneyTakeSource = null;

        //===============================================================
        //Static Properties
        //===============================================================
        public  static AudioManager NullableInstance => _instance;
        private static AudioManager                     _instance = null;

        //===============================================================
        //Fields
        //===============================================================
        private Dictionary<EAudioName,AudioClip>        _dicAudioClip = null;
        private bool                                    _isGameEnded = false;

        //===============================================================
        //Functions
        //===============================================================
        public void Init() 
        {
            if(_instance == null)
            _instance = this;
            
            SetUpDic();
            PlayBGM();
        }

        private void SetUpDic()
        {
            _dicAudioClip = new Dictionary<EAudioName, AudioClip>();

            foreach (var audioClip in _audioClipList)
            {
                var soundName = EAudioName.NONE;

                foreach (EAudioName enumName in System.Enum.GetValues(typeof(EAudioName)))
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

        public void PlayBGM() // 현재까지는 BGM 없음
        {
            if ( _isBGM )
                _BGMSource.Play();
        }

        public void PlaySFX(EAudioName soundName , bool isMoney = false, bool isDisplay = false,float plusPitchAmount = 0, float vloume = 1f)
        {
            if ( _isGameEnded )
                return;

            AudioSource source = null;
            AudioClip clip = null;
            
            switch (soundName)
            {
                case EAudioName.StackSound : 
                {
                    if (isMoney)
                        source = _moneySFXSource;
                    else if (isDisplay)
                        source = _displayStackSFXSource;
                    else  
                        source = _stackSFXSource;
                    break;
                }

                case EAudioName.MoneyStackSound :
                {
                    source = _moneyStackSFXSource;
                    break;
                }

                case EAudioName.CheckOutSound :
                {
                    source = _CheckoutSFXSource;
                    break;
                }

                case EAudioName.UpgradeSound :
                {
                    source = _UpgradeSFXSource;
                    break;
                }

                case EAudioName.MoneyTakeSound : 
                {
                    source = _moneyTakeSource;
                    break;
                }
            }

            clip = _dicAudioClip[soundName];
            source.pitch += plusPitchAmount;
            source.volume = vloume;
            source.PlayOneShot(clip);
        }

        public void ResetPitch(EAudioName soundName, bool isMoney = false, bool isDisplay = false)
        {   
            if ( _isGameEnded )
                return;

            AudioSource source = null;
            
            switch (soundName)
            {
                case EAudioName.StackSound : 
                {
                    if (isMoney)
                        source = _moneySFXSource;
                    else if (isDisplay)
                        source = _displayStackSFXSource;
                    else    
                        source = _stackSFXSource;
                    break;
                }

                case EAudioName.MoneyStackSound :
                {
                    source = _moneyStackSFXSource;
                    break;
                }

                case EAudioName.CheckOutSound :
                {
                    source = _CheckoutSFXSource;
                    break;
                }

                case EAudioName.UpgradeSound :
                {
                    source = _UpgradeSFXSource;
                    break;
                }
            }

            source.pitch = 1;
        }

        private void StopAllSound()
        {
            _isGameEnded = true;
            _stackSFXSource.Stop();
            _moneySFXSource.Stop();
            _CheckoutSFXSource.Stop();
        }
    }
}

