using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class TargetController : MonoBehaviour
{
    //�I�̒��S���W���w��
    public Vector3 targetCenter = Vector3.zero;

    public int hp = 1;

    //�@�����Ɋ�Â����X�R�A�v�Z�p�̔z��
    public float[] scoreRings = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f };
    public int[] scores = new int[] { 50, 25, 10, 5, 1 };

    //Gizmos�Ŏg�p����F
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

    //��I�ɓ��������Ƃ��ɌĂ΂�郁�\�b�h
    public void OnArrowHit(Vector3 ArrowPsition)
    {
        //��̓��������ꏊ��ditance�ɑ������
        float distance = Vector2.Distance(transform.position, ArrowPsition);
        int score = CalculateScore(distance);

        //CalculateScore�Ōv�Z����score��scoreController�Ɏ����Ă���
        scoreController.UpdateScoreText(score);

        Debug.Log(score);
    }

    /// <summary>
    /// �����Ɋ�Â��ăX�R�A�v�Z
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    private int CalculateScore(float distance)
    {
        //������������S����̋����ɉ�����score���v�Z����
        for (int i = 0; i < scoreRings.Length; i++)
        {
            if (distance <= scoreRings[i])
            {
                return scores[i];
            }
        }
        // �I�̊O�ɓ��������ꍇ�̃X�R�A
        return 0;
    }

    
    //Gizumo���g���ē_�����Ƃ̓����蔻���`�悷��
    private void OnDrawGizmos()
    {
        if (isGizmo) {

            if (scoreRings == null || ringColor == null)
                return;
            for (int i = 0; i < scoreRings.Length; i++)
            {
                Gizmos.color = ringColor[i % ringColor.Length];
                // 2D���ł̕`��
                Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, 0), scoreRings[i]);
            }
        }
    }
}
