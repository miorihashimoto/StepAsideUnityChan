using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    //カメラオブジェクト
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        //回転を開始する角度を設定
        this.transform.Rotate(0, Random.Range(0, 360), 0);

        //カメラオブジェクトを取得
        this.camera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        //回転
        this.transform.Rotate(0, 3, 0);

        //このスクリプトがアタッチされているオブジェクトのZ座標がカメラオブジェクトのZ座標より小さくなった場合(オブジェクトが画面外に出た場合)そのオブジェクトを消す
        if (this.transform.position.z < this.camera.transform.position.z)
        {
            Destroy(this.gameObject);
        }


    }
}
