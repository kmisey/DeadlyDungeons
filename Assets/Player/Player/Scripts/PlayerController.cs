using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Transform parent;

    public GameObject deathMenuObject;

    public float rotationSpeed;

    public float attackcooldowntimer;
    public float movspeed;
    public GameObject attackCheck;
    public static PlayerController Instance { get; set; }
    public Animator DamageAnimation;
    public bool canMove { get; set; }


    int calculatedDamage = 0;
    int calculatedLifesteal = 0;

    bool attackstatus = false;

    Sound sound;
    Player player;
    PlayerModelSwitcher switcher;
    VirtualJoystick joystick;
    BlockButton block;

    public int health { get; set; }

    void Awake() {
        Instance = this;
    }

    void Start () {
        health = 100;
        sound = Sound.Instance;
        canMove = true;
        block = BlockButton.Instance;
        player = Player.Instance;
        switcher = PlayerModelSwitcher.Instance;
        joystick = VirtualJoystick.Instance;
        calculatedDamage = (int)(900.0f / ((float)player.Armor));
        calculatedLifesteal = ((int)(player.Damage * (player.Lifesteal) * .3));
        Debug.Log(calculatedLifesteal);

    }

	void FixedUpdate () {
        if (block.buttondown) {

        }
        else if(canMove && joystick.InputDirection.magnitude > 0.1 && joystick.buttondown) {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, joystick.rotation, rotationSpeed);
            parent.GetComponent<CharacterController>().Move(new Vector3(joystick.InputDirection.z * -movspeed, 0, joystick.InputDirection.x * movspeed));
            updateAnims("Movement", 1);
        }
        else if (!joystick.buttondown) {
            updateAnims("Movement", 0);
            parent.transform.Translate(Vector3.zero);
        }
	}

    public void updateAnims(string triggername) {
        for(int i = 0;i < switcher.anims.Length; i++) {
            switcher.anims[i].SetTrigger(triggername);
        }
    }

    void updateAnims(string name, float value) {
        if(switcher.anims[0] == null) {
            return;
        }
        for (int i = 0; i < switcher.anims.Length; i++) {
            switcher.anims[i].SetFloat(name, value);
        }
    }

    void updateAnims(string name, bool value) {
        if (switcher.anims[0] == null) {
            return;
        }
        for (int i = 0; i < switcher.anims.Length; i++) {
            switcher.anims[i].SetBool(name, value);
        }
    }

    public void startAttack() {
        StartCoroutine(attack());
    }

    IEnumerator attack() {
        StartCoroutine(fireAttack());
        yield return new WaitForSeconds(.25f);
        Collider hitbox = attackCheck.GetComponent<Collider>();
        Collider[] col = Physics.OverlapBox(hitbox.bounds.center, hitbox.bounds.extents, transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in col) {
            if (c.name.Contains("ae_")) {
                addHealth(calculatedLifesteal);
                if(Random.Range(0, 100) <= (int)(player.Crit * 100)) {
                    c.GetComponent<Enemy>().applyDamage(player.Damage*2);
                    sound.play(sound.crithit);
                    Debug.Log("crit!");
                }
                else {

                    sound.play(sound.swordhit);
                    c.GetComponent<Enemy>().applyDamage(player.Damage);
                }

                //play sound

                break;
            }
        }
        yield return null;
    }

    void addHealth(int life) {
        int temp = health + life;
        if(temp > 100) {
            health = 100;
        }
        else {
            health = temp;
        }
        updateHealthBar();
    }

    IEnumerator fireAttack() {
        attackstatus = true;
        sound.playAttacks();
        int rand = Random.Range(0, 4);
        updateAnims("AttackNumber", rand);
        updateAnims("fireAttack");
        while (switcher.anims[0].GetCurrentAnimatorStateInfo(0).IsName("Attacking Blend Tree")) {
            yield return null;
        }
        attackstatus = false;
    }

    IEnumerator wait(float seconds) {
        yield return new WaitForSeconds(seconds);
    }

    public void applyDamage(int damage) {
        if (damage > 0 && health > 0) {
            if (!block.buttondown) {
                sound.play2nd(sound.playerhurt);
                int temp = health - calculatedDamage;
                if (temp <= 0) {
                    health = 0;
                    dead();
                    sound.stopMusic();
                    sound.play(sound.deathMusic);
                    if (Waves.Instance == null) {
                        deathDungInit();
                    }
                    else {
                        deathWaveInit();
                    }
                }
                else if (temp <= 100) {
                    DamageAnimation.SetTrigger("damage");
                    health = temp;
                }
                updateHealthBar();
            }
            else {
            }
        }
    }

    public void applyTrueDamage(int damage) {
        if(damage > 0) {
            if (!block.buttondown) {
                sound.play2nd(sound.playerhurt);
                int temp = health - damage;
                if (temp <= 0) {
                    health = 0;
                    updateHealthBar();
                    dead();
                    sound.stopMusic();
                    sound.play(sound.deathMusic);
                    if (Waves.Instance == null) {
                        deathDungInit();
                    }
                    else {
                        deathWaveInit();
                    }
                }
                else if (temp <= 100) {
                    DamageAnimation.SetTrigger("damage");
                    health = temp;
                }
                updateHealthBar();
            }
            else {
            }
        }
    }

    public void updateHealthBar() {
        PlayerUI.Instance.updateHealthBar((float)health);
    }

    void deathWaveInit() {
        deathMenuObject.SetActive(true);
        Waves.Instance.setEndingText();
        Waves.Instance.giveReward();
    }

    void deathDungInit() {
        deathMenuObject.SetActive(true);
    }

    void dead() {
        canMove = false;
        updateAnims("death");
    }

}
