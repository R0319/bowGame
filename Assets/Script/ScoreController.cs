using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    // TextMeshPro�R���|�[�l���g�̎Q��
    private TextMeshProUGUI m_TextMeshPro;

    // ���݂̓��_
    private int arrowScore;

    // TargetController�������Ă���Q�[���I�u�W�F�N�g���擾
    [SerializeField]
    private TargetController targetController;

    private void Start()
    {
        // TextMeshPro�R���|�[�l���g�̎Q�Ƃ��擾
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();

        // ���_���v�Z�i�Ⴆ�΁A���v�_��\������Ƃ��܂��j
        arrowScore = 0;

        UpdateScoreText(arrowScore);
    }


    /// <summary>
    /// TextMeshPro�̃e�L�X�g���X�V
    /// </summary>
    /// <param name="targetScore"></param>
    public void UpdateScoreText(int targetScore)
    {
        arrowScore += targetScore;
        if (m_TextMeshPro != null)
        {
            m_TextMeshPro.text = "�_��:" + arrowScore;
        }
    }
}
