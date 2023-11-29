using DigitalRuby.SoundManagerNamespace;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public enum eBGMusicPlayList
    {
        NONE = -1,
        TITLEBG,
        SANTUARYBG,
        DUNGEONBG,
        DUNGEONBGBOSS,
        Dungeon_Shop,
        Dungeon_HiddeonRoom,
        Dungeon_FirstRoom,
        Dugeon_CleardRoom,
    }

    /// <summary>
    /// 재생하고 싶은 음원의 명과 동일하게 설정
    /// </summary>
    public enum eSFXMusicPlayList
    {
        NONE = -1,
        TEST,
        GunShot_Start_AssultRifle,
        GunShot_1_AssultRifle,
        GunShot_2_AssultRifle,
        GunShot_3_AssultRifle,
        GunShot_4_AssultRifle,
        GunShot_5_AssultRifle,
        GunShot_Last_AssultRifle,
        GunShot_Start_ShotGun,
        GunShot_1_ShotGun,
        GunShot_2_ShotGun,
        GunShot_3_ShotGun,
        GunShot_4_ShotGun,
        GunShot_5_ShotGun,
        GunShot_Last_ShotGun,
        GunShot_Start_SniperRifle,
        GunShot_1_SniperRifle,
        GunShot_2_SniperRifle,
        GunShot_3_SniperRifle,
        GunShot_4_SniperRifle,
        GunShot_5_SniperRifle,
        GunShot_Last_SniperRifle,
        GunShot_Start_SubmachineGun,
        GunShot_1_SubmachineGun,
        GunShot_2_SubmachineGun,
        GunShot_3_SubmachineGun,
        GunShot_4_SubmachineGun,
        GunShot_5_SubmachineGun,
        GunShot_Last_SubmachineGun,
        Skill1_AssultRifle,
        Skill1_ShotGun,
        Skill1_SniperRifle,
        Skill1_SubmachineGun,
        Skill2_AssultRifle,
        Skill2_ShotGun,
        Skill2_SniperRifle,
        Skill2_SubmachineGun,
        Skill3_AssultRifle,
        Skill3_ShotGun,
        Skill3_SniperRifle,
        Skill3_SubmachineGun,
        PlayerImpact,
        PlayerDash,
        PlayerDamaged1,
        PlayerDamaged2,
        PlayerDead,
        EnterPortal,
        ExitPortal,
        UI_SkillButton,
        UI_Open,
        UI_Click,
        UI_Close,
        Monster_Dead,
        Relic_GrenadeExplosion,
        Dice
    }

    private HashSet<eSFXMusicPlayList> cooldownSounds = new HashSet<eSFXMusicPlayList>
    {
        eSFXMusicPlayList.Monster_Dead,
        eSFXMusicPlayList.GunShot_Start_AssultRifle,
        eSFXMusicPlayList.GunShot_1_AssultRifle,
        eSFXMusicPlayList.GunShot_2_AssultRifle,
        eSFXMusicPlayList.GunShot_3_AssultRifle,
        eSFXMusicPlayList.GunShot_4_AssultRifle,
        eSFXMusicPlayList.GunShot_5_AssultRifle,
        eSFXMusicPlayList.GunShot_Last_AssultRifle,
        eSFXMusicPlayList.GunShot_Start_ShotGun,
        eSFXMusicPlayList.GunShot_1_ShotGun,
        eSFXMusicPlayList.GunShot_2_ShotGun,
        eSFXMusicPlayList.GunShot_3_ShotGun,
        eSFXMusicPlayList.GunShot_4_ShotGun,
        eSFXMusicPlayList.GunShot_5_ShotGun,
        eSFXMusicPlayList.GunShot_Last_ShotGun,
        eSFXMusicPlayList.GunShot_Start_SniperRifle,
        eSFXMusicPlayList.GunShot_1_SniperRifle,
        eSFXMusicPlayList.GunShot_2_SniperRifle,
        eSFXMusicPlayList.GunShot_3_SniperRifle,
        eSFXMusicPlayList.GunShot_4_SniperRifle,
        eSFXMusicPlayList.GunShot_5_SniperRifle,
        eSFXMusicPlayList.GunShot_Last_SniperRifle,
        eSFXMusicPlayList.GunShot_Start_SubmachineGun,
        eSFXMusicPlayList.GunShot_1_SubmachineGun,
        eSFXMusicPlayList.GunShot_2_SubmachineGun,
        eSFXMusicPlayList.GunShot_3_SubmachineGun,
        eSFXMusicPlayList.GunShot_4_SubmachineGun,
        eSFXMusicPlayList.GunShot_5_SubmachineGun,
        eSFXMusicPlayList.GunShot_Last_SubmachineGun,
        eSFXMusicPlayList.UI_Click,
    };


    public static AudioManager instance;
    private AudioSource BGaudioSource;
    private List<AudioClip> BGAudioClipList;
    private float nextSFXTime = 0f;
    [SerializeField]
    private float sfxCoolTime = 0.5f;

    private const string path = "Sounds/{0}";

    private void Awake()
    {
        this.BGAudioClipList = new List<AudioClip>();
        instance = this;
        this.BGaudioSource = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
        SoundManager.MusicVolume = InfoManager.instance.settingInfo.musicVolume;
        SoundManager.SoundVolume = InfoManager.instance.settingInfo.sfxVolume;
    }

    public void BGMusicControl(eBGMusicPlayList musicTitle = default(eBGMusicPlayList), bool isStop = false)
    {
        if (isStop)
        {
            SoundManagerExtensions.StopLoopingMusicManaged(this.BGaudioSource);
        }
        else
        {
            var audioClip = this.BGAudioClipList.Find(x => x.name == musicTitle.ToString());
            this.BGaudioSource.clip = audioClip;
            SoundManagerExtensions.PlayLoopingMusicManaged(this.BGaudioSource, 1.0f, 3.0f, false);
        }

    }

    public void SceneBGMusicSetting(App.eSceneType sceneType)
    {
        this.BGAudioClipList = null;
        string folderPath = string.Format(path, sceneType.ToString());
        AudioClip[] clips = Resources.LoadAll<AudioClip>(folderPath);
        this.BGAudioClipList = clips.ToList();
        string sfxPath = string.Format(path, "SFX");
        clips = Resources.LoadAll<AudioClip>(sfxPath);
        foreach (var clip in clips)
        {
            this.BGAudioClipList.Add(clip);
        }
    }

    /// <summary>
    /// 재생을 원하는 사운드 이펙트 이넘명을 넣으면 재생.
    /// </summary>
    /// <param name="SFXSoundTitle">재생하고 싶은 사운드 이펙트</param>
    /// <param name="source">객체별 오디오 소스 컴포넌트</param>
    public void PlaySFXOneShot(eSFXMusicPlayList SFXSoundTitle, AudioSource source)
    {
        var audioClip = this.BGAudioClipList.Find(x => x.name == SFXSoundTitle.ToString());
        if (this.cooldownSounds.Contains(SFXSoundTitle))
        {
            if (Time.time >= this.nextSFXTime)
            {
                SoundManagerExtensions.PlayOneShotSoundManaged(source, audioClip);
                this.nextSFXTime = Time.time + this.sfxCoolTime;
            }
        }
        else
        {
            SoundManagerExtensions.PlayOneShotSoundManaged(source, audioClip);
        }
    }

    /// <summary>
    /// 바이브레이션이 필요한 부분에서 호출 (글로벌 설정에 따라 자동으로 호출 조절)
    /// </summary>
    public void Vibrate()
    {
        if (InfoManager.instance.settingInfo.isVivb)
            Handheld.Vibrate();
    }
}