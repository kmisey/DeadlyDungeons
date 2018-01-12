using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Waves : MonoBehaviour {

    public Text waveText;
    public Text endingText;

    public GameObject[] mobs;
    public GameObject[] boss;

    bool spawned = false;

    private int aliveEnemies = 0;
    private int waveNum = 1;

    public static Waves Instance { get; set; }

    Collider area;
    float size_x, size_z;

    public bool doneloading {
        get; set;
    }

    Sound sound;

    void Awake() {
        Instance = this;
    }

    void Start() {
        sound = Sound.Instance;
        sound.play(sound.StartGame);
        sound.startPlayMusic();
        doneloading = false;
        area = GetComponent<Collider>();
        size_x = area.bounds.size.x * Mathf.Cos(Mathf.Deg2Rad * 45);
        size_z = area.bounds.size.z * Mathf.Sin(Mathf.Deg2Rad * 45);
        waveText.text = waveNum.ToString();
        spawnNextWave();
    }

    public void spawnNextWave() {
        int temp = waveNum % 3;
        if(temp == 0) {
            temp = waveNum / 3;
            enemiesLeftToSpawn = temp;
            StartCoroutine(spawnBoss(enemiesLeftToSpawn));
        }
        else {
            enemiesLeftToSpawn = waveNum;
            StartCoroutine(spawnMobs());
        }
    }

    int enemiesLeftToSpawn = 0;

    IEnumerator spawnMobs() {

        stillAlive = true;
        GameObject temp;
        int count = 0;
        bool spawning = true;
        while (spawning) {
            ++count;
            enemiesLeftToSpawn -= 1;
            temp = Instantiate(mobs[Random.Range(0, mobs.Length)],
            new Vector3(Random.Range(-size_x / 2, size_x / 2) + area.transform.position.x, 0, Random.Range(-size_z / 2, size_z / 2) + area.transform.position.z),
            Quaternion.Euler(0, 0, 0)) as GameObject;
            temp.transform.SetParent(area.transform);
            temp.GetComponent<Enemy>().maxDist = 200.0f;
            temp.GetComponent<Enemy>().wavesArea = GetComponent<Waves>();
            aliveEnemies += 1;

            if (count == waveNum || enemiesLeftToSpawn == 0) {
                break;
            }

            else if (aliveEnemies == 4) {
                while (stillAlive) {
                    yield return null;
                }
                aliveEnemies = 0;
            }
            yield return null;
        }
    }

    bool stillAlive = false;

    IEnumerator spawnBoss(int bossNum) {
        stillAlive = true;
        int count = 0;
        bool spawning = true;
        while (spawning) {
            count++;
            enemiesLeftToSpawn -= 1;
            GameObject temp = Instantiate(boss[Random.Range(0, boss.Length)],
            new Vector3(area.transform.position.x, 0, area.transform.position.z),
            Quaternion.Euler(0, 0, 0)) as GameObject;
            temp.transform.SetParent(area.transform);
            temp.GetComponent<Enemy>().wavesArea = GetComponent<Waves>();
            aliveEnemies += 1;

            if (count == bossNum || enemiesLeftToSpawn == 0) {
                break;
            }

            else if (aliveEnemies == 4) {
                while (stillAlive) {
                    yield return null;
                }
                aliveEnemies = 0;
            }
            yield return null;
        }
    }

    public void enemyDied() {
        Debug.Log("enemy died : " + aliveEnemies);
        aliveEnemies -= 1;
        if (enemiesLeftToSpawn <= 0 && aliveEnemies == 0) {
            Debug.Log("Spawn Next Wave");
            waveNum += 1;
            spawnNextWave();
            waveText.text = waveNum.ToString();
        }
        else if(aliveEnemies == 0){
            Debug.Log("Spawn Next Batch of Wave");
            stillAlive = false;
            StartCoroutine(spawnMobs());
        }
    }

    public void setEndingText() {
        if (waveNum > ZPlayerPrefs.GetInt("highest_wave")) {
            ZPlayerPrefs.SetInt("highest_wave", waveNum);
        }
        endingText.text = ("You Survived: " + waveNum + "\nHighest Wave: " + ZPlayerPrefs.GetInt("highest_wave"));
        Achievements.Instance.checkWave();
    }

    public void giveReward() {
        int rolls = 0;
        if (waveNum < 4) {
            rolls = 1;
        }
        else {
            rolls = ((waveNum - (waveNum % 4)) / 4) * 3;
        }
        Player.Instance.addExperience(waveNum * 10);
        GrantReward.Instance.rewardSize = rolls;
    }
}
