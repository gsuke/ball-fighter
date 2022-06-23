using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    private int hpValue;
    public int maxHp, atk;
    public int hp
    {
        get
        {
            return hpValue;
        }
        set
        {
            if (value < 0)
            {
                hpValue = 0;
            }
            else if (value > maxHp)
            {
                hpValue = maxHp;
            }
            else
            {
                hpValue = value;
            }
        }
    }
    public float speed;
    public bool invincible = false, useStatusWindow = true;
    public GameObject target = null;
    public string charactorName, action,
        role; // role: 役割 ( "Player" or "Monster" )
    private GameObject foundTargetEffect;

    public virtual void Start()
    {
        foundTargetEffect = Resources.Load("Prefabs/Effects/FoundTargetEffect") as GameObject;

        // useStatusWindowにチェックが入っている場合はStatusWindowを生成
        if (useStatusWindow)
        {
            GameObject statusWindow = Resources.Load("Prefabs/StatusWindow") as GameObject;
            GameObject go = Instantiate(statusWindow);
            go.GetComponent<StatusWindow>().target = this.gameObject;
        }

        hp = maxHp; // hpの調整
    }

    public virtual void Update()
    {
		// 落下したら死亡
		if (transform.position.y < -10.0f) {
			hp = 0;
		}
    }

    // ダメージを受けるときの関数
    public virtual void Damage(int dmg)
    {
        hp -= dmg;
    }

    // 自分自身を破棄する関数
    public void CharacterDestroy()
    {
        Destroy(this.gameObject); // 自分自身を破棄
    }

    // 攻撃をする(攻撃オブジェクトを生成する)関数
    public void CreateAttack(GameObject prefab, float mgn, int attackCount = 0)
    {
        GameObject go = (GameObject)Instantiate(prefab, transform.position, transform.rotation); // 攻撃の生成
        go.transform.SetParent(transform); // 親の設定
        Attack attack = go.GetComponent<Attack>(); // Attackスクリプトの取得

        // さまざまな値を渡す
        attack.attackCount = attackCount;
        attack.atk = atk;
        attack.mgn = mgn;
        attack.role = role;
    }

    // ターゲットを探す処理
    public void SearchTarget(float d)
    {
        // 変数宣言
        float targetDistance, minDistance;
        bool flag = false;

        // 初期値のセット
        if (target == null)
        {
            minDistance = 9999;
        }
        else
        {
            minDistance = (target.transform.position - transform.position).magnitude;
        }

        // オブジェクトの走査
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Character"))
        { // キャラクタータグがあるゲームオブジェクト
            Character goC = go.GetComponent<Character>(); // キャラクタータグの取得
            if (goC == null)
            {
                continue;
            }
            if (role != goC.role)
            { // 自分と相手のroleが異なるとき (たまにこの行でエラーが発生する…再現性は低め？)
                targetDistance = (go.transform.position - transform.position).magnitude; // 距離計算
                if (targetDistance < d)
                { // 条件より距離が近いかどうか
                    if (targetDistance < minDistance)
                    { // 距離が今までで最小の場合
                        target = go;
                        minDistance = targetDistance;
                        flag = true;
                    }
                }
            }
        }

        // ターゲットの取得が行われたなら
        if (flag)
        {
            Instantiate(foundTargetEffect, transform.position, Quaternion.identity); // エフェクト生成
        }
    }

    // ターゲットを解除する処理(解除した場合trueを返す)
    public bool RemoveTarget(float d)
    {
        bool flag = false;
        if ((target.transform.position - transform.position).magnitude > d)
        {
            target = null;
            flag = true;
        }
        return flag;
    }

}
