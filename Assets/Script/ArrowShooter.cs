using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // 矢のプレハブ
    public float arrowSpeed = 10f; // 矢の速度
    private float maxArrowSpeed = 10f;
    public float spawnDistance = 1.5f; // 矢の生成位置をカメラの前方に少し離す距離
    public bool keyHold = false;
    public float maxHoldTime = 0;
    private float holdTime = 0;

    void Update()
    {
        if (Input.GetMouseButton(0)) // マウス左クリック
        {
            keyHold = true; //キーを押し続けるとkeyholdが実行
            holdTime += Time.deltaTime;
            Debug.Log(holdTime);
            if (maxHoldTime <= holdTime)
            {
                //キーを押し続けた時間をmaxHoldTimeで制限する.
                holdTime = maxHoldTime;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //このあとarrowSpeedの処理を書く
            ShootArrow();
            //キーを押し続けた時間をリセットする
            holdTime = 0;
        }


    }

    void ShootArrow()
    {
        // カメラのスクリーン上でのマウス座標を取得
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;

        // マウス位置をワールド座標に変換
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        Debug.Log(ray);

        // レイキャストでヒットした位置に矢を飛ばす
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = (targetPosition - mainCamera.transform.position).normalized;

            // 矢の生成位置をカメラの前方に設定
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spawnDistance;

            // 矢を生成
            GameObject arrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.LookRotation(direction));

            // 矢を飛ばす
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
    //    // マウス位置をワールド座標に変換
    //    Vector3 mousePosition = Input.mousePosition;
    //    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
    //    RaycastHit hit;

    //    // レイキャストで地平面 (Y = 0) にヒットした位置を取得
    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default")))
    //    {
    //        // マウス視点からの位置を取得
    //        Vector3 mouseWorldPosition = hit.point;

    //        // 矢の生成位置を地平面のマウス位置に設定（Y座標は0）
    //        Vector3 spawnPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, Camera.main.transform.position.z);

    //        // 矢のターゲット位置を少し前方に設定
    //        Vector3 targetPosition = spawnPosition + Vector3.forward;

    //        // 水平方向の方向ベクトルを計算
    //        Vector3 direction = (targetPosition - spawnPosition).normalized;

    //        // 矢を生成
    //        GameObject arrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.LookRotation(direction));

    //        // 矢を飛ばす
    //        Rigidbody rb = arrow.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.AddForce(direction * arrowSpeed);
    //        }
    //    }
    //}
}
