using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int health;
    public float attackRange;
    public float rotationSpeed;
    public float moveSpeed;
    public float maxDist;

    public GameObject healthbar;

    bool dead = false;
    int defense;
    GameObject player;
    PlayerController playerC;

    public Animator anim;
    public GameObject enemyModel;
    public Collider attackHitbox;

    public RoomRandomizer parentArea { get; set; }
    public Waves wavesArea { get; set; }

    CharacterController cc;

    Vector3 moving;

    Sound sound;

    // Use this for initialization
    void Start () {
        playerC = PlayerController.Instance;
        player = Player.Instance.gameObject;
        cc = GetComponent<CharacterController>();
        health *= Player.Instance.Level;
        health /= 5;
        defense = health;
        sound = Sound.Instance;
    }

    bool dontmove = false;

    void FixedUpdate() {
        if(player == null || playerC.health <= 0) {

        }
        else if (!dontmove) {
            transform.rotation = Quaternion.LerpUnclamped(enemyModel.transform.rotation, getRotation(), rotationSpeed * Time.deltaTime);
            float range = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));
                if (range > attackRange && range < maxDist && !attackstatus) {
                    anim.SetFloat("movement", 1);
                    moving = (new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
                    cc.Move(moving * moveSpeed * Time.deltaTime);
                }
                else if (range <= attackRange && !attackstatus) {

                    moving = (new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
                    StartCoroutine(startAttack());
                    anim.SetFloat("movement", 0);
                }
                else if (range > maxDist) {
                        anim.SetFloat("movement", 0);
                }
                
        }
    }

    IEnumerator startAttack() {
        yield return StartCoroutine(fireAttack());
    }

    bool attackstatus = false;

    IEnumerator fireAttack() {
        attackstatus = true;
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("attack");
        sound.play(sound.monsterattack);
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("attack")) {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        attackstatus = false;
    }

    public Quaternion getRotation() {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, ((Mathf.Atan2(-moving.z, moving.x) / 3.14f) * 180.0f) + 90.0f, 0));
        return rotation;
    }

    public void applyDamage(int damage) {
        if (!dead) {
            int calculatedDamage = (int)(((float)damage)*.3);
            int temp = health - calculatedDamage;
            if (temp <= 0) {
                health = 0;
                updateHealthBar();
                death();
            }
            else if (temp > 0) {
                health -= calculatedDamage;
                updateHealthBar();
            }
        }
    }

    public void death() {
        dead = true;
        sound.play2nd(sound.monsterdeath);
        anim.SetTrigger("death");
        if (parentArea != null) {
            parentArea.enemyDied();
            Achievements.Instance.checkKc();
        }
        else if(wavesArea != null) {
            wavesArea.enemyDied();
        }
        else {
            GameObject.Find("EXITDOORTUT").SetActive(false);
        }
    }

    void updateHealthBar() {
        healthbar.transform.localScale = new Vector3(((float)health) / ((float)defense), 1.0f, 1.0f);
    }
}
