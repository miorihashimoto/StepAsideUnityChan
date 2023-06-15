using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    //Unity�����̃I�u�W�F�N�g
    private GameObject unitychan;

    //Unity�����ƃJ�����̋���
    private float difference;

    private float visiblePosZ;

    private GameObject gameObject;


    // Start is called before the first frame update
    void Start()
    {
        //Unity�����̃I�u�W�F�N�g���擾
        this.unitychan = GameObject.Find("unitychan");

        //Unity�����ƃJ�����̈ʒu(Z���W)�̍������߂�
        this.difference = unitychan.transform.position.z - this.transform.position.z;

        this.visiblePosZ = this.transform.position.z - 1;


    }

    // Update is called once per frame
    void Update()
    {
        //Unity�����̈ʒu�ɍ��킹�ăJ�����̈ʒu���ړ�
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);

        if(this.transform.position.z==this.visiblePosZ)
        {
            Debug.Log("���c�Y�n");

            Destroy(this.gameObject);
        }
    }
}
