using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{

    public GameObject set; //set��prefab
    public List<GameObject> setList = new List<GameObject>();
    public float generateDistanece = 40f; //��������
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

        // Z�������̈ړ��ʂ����Z
        //lastGeneratedPositon += generateDistanece * Time.deltaTime;
        lastGeneratedPositon = (int)transform.position.z;

        Debug.Log(lastGeneratedPositon);

        return;

        //���ȏ�ړ�������Prefab�𐶐�
        if (lastGeneratedPositon % generateDistanece == 0 && !isGenerated)
        {
            // �����ʒu���v�Z
            Vector3 generatePosition =  Vector3.forward * generateDistanece * generateCount;

            // Prefab�𐶐�
            GameObject setObje = Instantiate(set, generatePosition, Quaternion.identity);

            setList.Add(setObje);

            //���������J�E���g
            generateCount ++;

            // �����ʒu�����Z�b�g
            lastGeneratedPositon = 0f;
            // ��������
            isGenerated = true;
        }else if (lastGeneratedPositon % generateDistanece != 0)
        {
            isGenerated = false;
        }
    }
}
