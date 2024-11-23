using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{

    public GameObject set; //setのprefab
    public List<GameObject> setList = new List<GameObject>();
    public float generateDistanece = 40f; //生成距離
    public int generateCount = 1;
    public bool isGenerated = false;

    private float lastGeneratedPositon = 0f;

    public float speed = 1.0f;
    public float maxSpeed = 20.0f;
    public float acceleration = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed += acceleration * Time.deltaTime;

        //if (speed > maxSpeed) {
        //    speed = maxSpeed;
        //}
        speed = Mathf.Min(speed, maxSpeed);
        //speed = Mathf.Clamp(speed, 0, maxSpeed);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Z軸方向の移動量を加算
        //lastGeneratedPositon += generateDistanece * Time.deltaTime;
        lastGeneratedPositon = (int)transform.position.z;

        Debug.Log(lastGeneratedPositon);

        return;

        //一定以上移動したらPrefabを生成
        if (lastGeneratedPositon % generateDistanece == 0 && !isGenerated)
        {
            // 生成位置を計算
            Vector3 generatePosition =  Vector3.forward * generateDistanece * generateCount;

            // Prefabを生成
            GameObject setObje = Instantiate(set, generatePosition, Quaternion.identity);

            setList.Add(setObje);

            //生成数をカウント
            generateCount ++;

            // 生成位置をリセット
            lastGeneratedPositon = 0f;
            // 生成した
            isGenerated = true;
        }else if (lastGeneratedPositon % generateDistanece != 0)
        {
            isGenerated = false;
        }
    }
}
