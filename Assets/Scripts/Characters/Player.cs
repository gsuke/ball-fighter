using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Character
{

    public int frame, maxFrame, attackCount, weapon;
    public int[] weaponNumber;

    // コンポーネント
    private AudioSource audioSource;
    private Animator animator;

    // オブジェクト
    private GameObject changeWeaponEffect, damageEffect, myDamageText;
    private AvoidEffect avoidEffect;
    private AudioClip audioAvoid, audioChangeWeapon, audioDamage;
    private GameController gc;
    private Animator displayFilter;
    private PlayerCamera playerCamera;
	private CharacterControllerGravity characterControllerGravity;

    public override void Start()
    {
        base.Start();

        // コンポーネントの取得
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
		characterControllerGravity = GetComponent<CharacterControllerGravity> ();

        // オブジェクトの取得
        changeWeaponEffect = transform.FindChild("ChangeWeaponEffect").gameObject;
        damageEffect = transform.FindChild("DamageEffect").gameObject;
        avoidEffect = transform.FindChild("AvoidEffect").GetComponent<AvoidEffect>();
        audioAvoid = Resources.Load("SE/Avoid") as AudioClip;
        audioChangeWeapon = Resources.Load("SE/ChangeWeapon") as AudioClip;
        audioDamage = Resources.Load("SE/Explosion") as AudioClip;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        myDamageText = GameObject.Find("MyDamageText");
        displayFilter = GameObject.Find("DisplayFilter").GetComponent<Animator>();
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
    }

    public override void Update()
    {
        base.Update();

        // 回避中の処理
        if (action == "Avoid")
        {
            frame++;
            // 回避処理終了
            if (frame == maxFrame)
            {
                FinishAction();
                invincible = false;
            }
        }

        // 攻撃中の処理
        else if (action == "Attack")
        {
            frame++;
            if (frame < maxFrame / 2)
            {
                // 何もしない
            }
            else if (frame == maxFrame / 2)
            {
                attackCount++;
                CreateAttack(gc.weaponPrefab[weaponNumber[weapon]], gc.weaponMgn[weaponNumber[weapon]], attackCount); // 攻撃の生成
            }
            else if (frame < maxFrame)
            {
                // 何もしない
            }
            else
            {
                FinishAction();
            }
        }

        // 武器変更中の処理
        else if (action == "ChangeWeapon")
        {
            frame++;
            if (frame >= maxFrame)
            {
                FinishAction();
            }
        }

        // ダメージ中の処理
        else if (action == "Damage")
        {
            frame++;
            if (frame < maxFrame / 3)
            {
                // 何もしない
            }
            else if (frame == maxFrame / 3)
            {
                animator.SetTrigger("DamageInvincibleEffect");
            }
            else if (frame < maxFrame)
            {
                // 何もしない
            }
            else
            {
                FinishAction();
                invincible = false;
            }
        }

		// 死亡時にリスタート
		if (hp <= 0) {
			hp = maxHp;
			transform.position = new Vector3 (10, 30, 10);

			if (characterControllerGravity) {
				characterControllerGravity.ResetGravity ();
			}
		}

    }

    // アクション変更時の関数
    void ChangeAction(string actionName, int maxFrame)
    {
        action = actionName;
        this.frame = 0;
        this.maxFrame = maxFrame;
    }

    // アクション終了時の関数
    void FinishAction()
    {
        action = "";
    }

    // ダメージを受ける処理
    public override void Damage(int dmg)
    {
        base.Damage(dmg);

        ChangeAction("Damage", 180);
        invincible = true;
        audioSource.PlayOneShot(audioDamage); // 効果音
        damageEffect.GetComponent<ParticleSystem>().Play(); // ダメージパーティクル

        // 操作キャラクターだった場合のエフェクト
        if (gc.player == this.gameObject)
        {
            displayFilter.SetTrigger("Damage"); // 画面を赤色に点滅
            playerCamera.Damage(); // カメラを回転
            myDamageText.GetComponent<Text>().text = dmg.ToString(); // ダメージテキストの文字変更
            myDamageText.GetComponent<Animator>().SetTrigger("Damage"); // ダメージテキストのアニメーション再生
        }
    }

    // 回避開始時の処理
    public virtual void Avoid()
    {
        ChangeAction("Avoid", 30);
        invincible = true;
        audioSource.PlayOneShot(audioAvoid); // 効果音
        animator.SetTrigger("AvoidEffect"); // アニメーション再生
        avoidEffect.EffectStart(); // 回避エフェクト
    }

    // 攻撃開始時の処理
    public virtual void Attack()
    {
        ChangeAction("Attack", gc.weaponFrame[weaponNumber[weapon]]);
    }

    // 武器変更開始時の関数
    public virtual void ChangeWeapon(int n)
    {
        ChangeAction("ChangeWeapon", 45);
        weapon = n;
        animator.SetTrigger("ChangeWeaponEffect"); // アニメーション再生
        changeWeaponEffect.GetComponent<ParticleSystem>().Play(); // パーティクル再生
        audioSource.PlayOneShot(audioChangeWeapon);
        attackCount = 0;
    }
}
