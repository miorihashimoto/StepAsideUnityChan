using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //�J�����I�u�W�F�N�g
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        //�J�����I�u�W�F�N�g���擾
        this.camera= GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //���̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g��Z���W���J�����I�u�W�F�N�g��Z���W��菬�����Ȃ����ꍇ(�I�u�W�F�N�g����ʊO�ɏo���ꍇ)���̃I�u�W�F�N�g������
        if (this.transform.position.z < this.camera.transform.position.z)
        {
            Destroy(this.gameObject);
        }

    }
}
