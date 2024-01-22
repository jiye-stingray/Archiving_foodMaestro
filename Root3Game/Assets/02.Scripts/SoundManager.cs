using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    private AudioSource _bgSoundSource;

    [Header("BGSound")]
    [SerializeField] private AudioClip _titleBackgroundSound;
    [SerializeField] private AudioClip _inGameBackgroundSound;
    [SerializeField] private AudioClip _roomSceneBackgroundSound;

    [SerializeField] private AudioClip _btnClickAudioClip;
   
    public override void Awake()
    {
        base.Awake();
        _bgSoundSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        TitleBgSoundPlay();
    }

    public void BgSoundPlay(AudioClip clip)
    {
        if (clip == null) return;

        _bgSoundSource.clip = clip;
        _bgSoundSource.loop = true;
        _bgSoundSource.Play();
    }

    public void SFXPlay(AudioClip clip)
    {
        if(clip == null) return;

        GameObject go = new GameObject(clip.name);
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length);
    }

    public void TitleBgSoundPlay() => BgSoundPlay(_titleBackgroundSound); // 타이틀 배경음악 재생

    public void InGameBGSoundPlay() => BgSoundPlay(_inGameBackgroundSound);

    public void RoomBGSoundPlay() => BgSoundPlay(_roomSceneBackgroundSound);

    public void BtnClickSFXPlay() => SFXPlay(_btnClickAudioClip);

}
