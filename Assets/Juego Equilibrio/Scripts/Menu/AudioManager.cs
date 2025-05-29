using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSettings[] audioSettings;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceSFX;
    public static AudioManager Instance;
    private float[] _savedVolumes;
    private int _dataLength;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        _dataLength = audioSettings.Length;

        _savedVolumes = new float[_dataLength];

    }

    private void OnEnable()
    {
        for (int i = 0; i < _dataLength; i++)
        {
            float savedVolume = PlayerPrefs.GetFloat(audioSettings[i].Key, 0.5f);
            audioSettings[i].UpdateVolume(savedVolume);
            _savedVolumes[i] = savedVolume;
        }

        MasterManager.OnChangeScene += ChangeAudio;
    }
    private void Start()
    {
        ChangeAudio(0);
    }

    private void OnDisable()
    {
        MasterManager.OnChangeScene -= ChangeAudio;
    }

    public void RevertChanges()
    {
        for (int i = 0; i < _dataLength; i++)
        {
            audioSettings[i].UpdateVolume(_savedVolumes[i]);
        }
    }

    public void ApplyChange()
    {
        for (int i = 0; i < _dataLength; i++)
        {
            audioSettings[i].SaveDataFile();

            _savedVolumes[i] = audioSettings[i].VolumeScaled;
        }
    }

    public void ChangeAudio(int index)
    {
        audioSourceMusic.clip = audioClips[index];
        audioSourceMusic.Play();
    }

    public void PlaySFX(int num)
    {
        audioSourceSFX.PlayOneShot(audioClips[num]);
    }
}
