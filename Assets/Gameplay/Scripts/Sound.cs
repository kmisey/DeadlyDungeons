using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

    public AudioSource music;
    public AudioSource soundfx;
    public AudioSource soundfx2;

    public AudioClip[] swordhit;
    public AudioClip[] maleAttacks;
    public AudioClip[] femaleAttacks;
    public AudioClip[] monsterattack;
    public AudioClip[] playerhurt;

    public AudioClip[] menumusic;
    public AudioClip[] playmusic;

    public AudioClip click;
    public AudioClip click2;

    public AudioClip reward;


    public AudioClip monsterdeath;
    public AudioClip openChest;
    public AudioClip crithit;
    public AudioClip craftItem;
    public AudioClip storeSound;
    public AudioClip StartGame;
    public AudioClip deathMusic;

    int gender = 0;

    Player p;
    public static Sound Instance { get; set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        p = Player.Instance;
        soundfx = GetComponent<AudioSource>();
        GetVolume();
    }

    public void GetVolume() {
        if (!PlayerPrefs.HasKey("SaveString")) {

            return;
        }
        if (PlayerPrefs.GetInt("soundon") == 1) {
            soundfx.volume = .5f;
            soundfx2.volume = .5f;
        }
        else {
            soundfx.volume = 0;
            soundfx2.volume = 0;
        }

        if (PlayerPrefs.GetInt("musicon") == 1) {
            music.volume = 0.15f;
        }
        else {
            music.volume = 0;
        }
    }

    public void startMenuMusic() {
        music.clip = menumusic[Random.Range(0, menumusic.Length)];
        music.Play();
    }

    public void startPlayMusic() {
        music.clip = playmusic[Random.Range(0, playmusic.Length)];
        music.Play();
        StartCoroutine(playNextSong());
    }

    IEnumerator playNextSong() {
        yield return new WaitForSeconds(music.clip.length);
        startPlayMusic();
    }

	public void play(AudioClip clip) {
            soundfx.clip = clip;
            soundfx.Play();
    }

    public void play(AudioClip[] clips) {
            soundfx.clip = (clips[Random.Range(0, clips.Length)]);
            soundfx.Play();
    }

    public void playAttacks() {
        if(p.Gender == 0) {
            soundfx.clip = (maleAttacks[Random.Range(0, maleAttacks.Length)]);
            soundfx.Play();
        }
        else {
            soundfx.clip = (femaleAttacks[Random.Range(0, femaleAttacks.Length)]);
            soundfx.Play();
        }
    }



    public void play2nd(AudioClip clip) {
            soundfx2.clip = clip;
            soundfx2.Play();
    }

    public void play2nd(AudioClip[] clips) {
            soundfx2.clip = (clips[Random.Range(0, clips.Length)]);
            soundfx2.Play();
    }

    public void stopMusic() {
        music.Stop();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
