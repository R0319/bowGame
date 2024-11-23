using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    // TextMeshProコンポーネントの参照
    private TextMeshProUGUI m_TextMeshPro;

    // 現在の得点
    private int arrowScore;

    // TargetControllerを持っているゲームオブジェクトを取得
    [SerializeField]
    private TargetController targetController;

    private void Start()
    {
        // TextMeshProコンポーネントの参照を取得
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();

        // 得点を計算（例えば、合計点を表示するとします）
        arrowScore = 0;

        UpdateScoreText(arrowScore);
    }


    /// <summary>
    /// TextMeshProのテキストを更新
    /// </summary>
    /// <param name="targetScore"></param>
    public void UpdateScoreText(int targetScore)
    {
        arrowScore += targetScore;
        if (m_TextMeshPro != null)
        {
            m_TextMeshPro.text = "点数:" + arrowScore;
        }
    }
}
