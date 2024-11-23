using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // ��̃v���n�u
    public float arrowSpeed = 10f; // ��̑��x
    private float maxArrowSpeed = 10f;
    public float spawnDistance = 1.5f; // ��̐����ʒu���J�����̑O���ɏ�����������
    public bool keyHold = false;
    public float maxHoldTime = 0;
    private float holdTime = 0;

    void Update()
    {
        if (Input.GetMouseButton(0)) // �}�E�X���N���b�N
        {
            keyHold = true; //�L�[�������������keyhold�����s
            holdTime += Time.deltaTime;
            Debug.Log(holdTime);
            if (maxHoldTime <= holdTime)
            {
                //�L�[���������������Ԃ�maxHoldTime�Ő�������.
                holdTime = maxHoldTime;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //���̂���arrowSpeed�̏���������
            ShootArrow();
            //�L�[���������������Ԃ����Z�b�g����
            holdTime = 0;
        }


    }

    void ShootArrow()
    {
        // �J�����̃X�N���[����ł̃}�E�X���W���擾
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;

        // �}�E�X�ʒu�����[���h���W�ɕϊ�
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        Debug.Log(ray);

        // ���C�L���X�g�Ńq�b�g�����ʒu�ɖ���΂�
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = (targetPosition - mainCamera.transform.position).normalized;

            // ��̐����ʒu���J�����̑O���ɐݒ�
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spawnDistance;

            // ��𐶐�
            GameObject arrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.LookRotation(direction));

            // ����΂�
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //rb.velocity = direction * arrowSpeed;
                rb.AddForce(direction * arrowSpeed, ForceMode.Impulse);
            }
        }
    }

    //void ShootArrow()
    //{
    //    // �}�E�X�ʒu�����[���h���W�ɕϊ�
    //    Vector3 mousePosition = Input.mousePosition;
    //    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
    //    RaycastHit hit;

    //    // ���C�L���X�g�Œn���� (Y = 0) �Ƀq�b�g�����ʒu���擾
    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default")))
    //    {
    //        // �}�E�X���_����̈ʒu���擾
    //        Vector3 mouseWorldPosition = hit.point;

    //        // ��̐����ʒu��n���ʂ̃}�E�X�ʒu�ɐݒ�iY���W��0�j
    //        Vector3 spawnPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, Camera.main.transform.position.z);

    //        // ��̃^�[�Q�b�g�ʒu�������O���ɐݒ�
    //        Vector3 targetPosition = spawnPosition + Vector3.forward;

    //        // ���������̕����x�N�g�����v�Z
    //        Vector3 direction = (targetPosition - spawnPosition).normalized;

    //        // ��𐶐�
    //        GameObject arrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.LookRotation(direction));

    //        // ����΂�
    //        Rigidbody rb = arrow.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.AddForce(direction * arrowSpeed);
    //        }
    //    }
    //}
}
