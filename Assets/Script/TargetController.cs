using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class TargetController : MonoBehaviour
{
    //的の中心座標を指定
    public Vector3 targetCenter = Vector3.zero;

    public int hp = 1;

    //　距離に基づいたスコア計算用の配列
    public float[] scoreRings = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f };
    public int[] scores = new int[] { 50, 25, 10, 5, 1 };

    //Gizmosで使用する色
    public Color[] ringColor = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta };

    [SerializeField]
    private ScoreController scoreController;

    [SerializeField]
    private bool isGizmo = true;

    [SerializeField]
    private GameObject effectPrefab;

    private void Start()
    {
        //transform.DOMoveX(6, 3.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
        //transform.DOMoveZ(6, 3.0f).SetEase(Ease.Linear).SetLink(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            OnArrowHit(collision.transform.position);
            hp--;
            if (hp <= 0)
            {

                GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                Destroy(effect,1.0f);

                Destroy(gameObject);
            }

        }
    }

    //矢が的に当たったときに呼ばれるメソッド
    public void OnArrowHit(Vector3 ArrowPsition)
    {
        //矢の当たった場所をditanceに代入する
        float distance = Vector2.Distance(transform.position, ArrowPsition);
        int score = CalculateScore(distance);

        //CalculateScoreで計算したscoreをscoreControllerに持っていく
        scoreController.UpdateScoreText(score);

        Debug.Log(score);
    }

    /// <summary>
    /// 距離に基づいてスコア計算
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    private int CalculateScore(float distance)
    {
        //矢が当たった中心からの距離に応じてscoreを計算する
        for (int i = 0; i < scoreRings.Length; i++)
        {
            if (distance <= scoreRings[i])
            {
                return scores[i];
            }
        }
        // 的の外に当たった場合のスコア
        return 0;
    }

    
    //Gizumoを使って点数ごとの当たり判定を描画する
    private void OnDrawGizmos()
    {
        if (isGizmo) {

            if (scoreRings == null || ringColor == null)
                return;
            for (int i = 0; i < scoreRings.Length; i++)
            {
                Gizmos.color = ringColor[i % ringColor.Length];
                // 2D環境での描画
                Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, 0), scoreRings[i]);
            }
        }
    }
}
