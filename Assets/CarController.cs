using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //カメラオブジェクト
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        //カメラオブジェクトを取得
        this.camera= GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //このスクリプトがアタッチされているオブジェクトのZ座標がカメラオブジェクトのZ座標より小さくなった場合(オブジェクトが画面外に出た場合)そのオブジェクトを消す
        if (this.transform.position.z < this.camera.transform.position.z)
        {
            Destroy(this.gameObject);
        }

    }
}
