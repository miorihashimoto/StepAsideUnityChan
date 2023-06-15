using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//追加７

public class UnityChanController : MonoBehaviour
{
    //アニメーションをするためのコンポーネントを入れる
    private Animator myAnimator;

    //Unityちゃんを移動させるコンポーネントを入れる（追加）
    private Rigidbody myRigidbody;

    //前方向の速度（追加）
    private float velocityZ = 16f;

    //横方向の速度(追加２)
    private float velocityX = 10f;

    //上方向の速度(追加３)
    private float velocityY = 10f;

    //左右の移動できる範囲(追加２)
    private float movableRange = 3.4f;

    //動きを減速させる係数(追加４)
    private float coefficient = 0.99f;

    //ゲーム終了の判定(追加4)
    private bool isEnd = false;

    //ゲーム終了時に表示するテキスト(追加７)
    private GameObject stateText;

    //スコアを表示するテキスト(追加８)
    private GameObject scoreText;

    //得点(追加８)
    private int score = 0;

    //左ボタン押下の判定(追加９)
    private bool isLButtonDown = false;

    //右ボタン押下の判定(追加９)
    private bool isRButtonDown = false;

    //ジャンプボタン押下の判定(追加９)
    private bool isJButtonDown = false;


    // Start is called before the first frame update
    void Start()
    {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のstateTextオブジェクトを取得(追加７)
        this.stateText = GameObject.Find("GameResultText");

        //シーン中のscoreTextオブジェクトを取得(追加８)
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了ならUnityちゃんの動きを減衰する(追加４)
        if(this.isEnd)
        {
            this.velocityZ *= this.coefficient;

            this.velocityX *= this.coefficient;

            this.velocityY *= this.coefficient;

            this.myAnimator.speed *= this.coefficient;

        }

        //横方向の入力による速度(追加２)
        float inputVelocityX = 0;

        //上方向の入力による速度(追加３)
        float inputVelocityY = 0;

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる(追加２)
        if((Input.GetKey(KeyCode.LeftArrow)||this.isLButtonDown)&&-this.movableRange<this.transform.position.x)//追加９
        {
            //左方向への速度を代入(追加２)
            inputVelocityX = -this.velocityX;
        }

        else if((Input.GetKey(KeyCode.RightArrow)||this.isRButtonDown)&&this.transform.position.x<this.movableRange)//追加９
        {
            //右方向への速度を代入(追加２)
            inputVelocityX = this.velocityX;
        }

        //ジャンプしていないときにスペースが押されたらジャンプする(追加３)
        if((Input.GetKeyDown(KeyCode.Space)||this.isJButtonDown)&&this.transform.position.y<0.5f)//追加９
        {
            //ジャンプアニメを再生(追加３)
            this.myAnimator.SetBool("Jump", true);

            //上方向への速度を代入(追加３)
            inputVelocityY = this.velocityY;
        }

        else
        {
            //現在のy軸の速度を代入(追加３)
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jumpステートの場合はJumpにfalseをセットする(追加３)
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        //Unityちゃんに速度を与える（追加）(変更)(変更２)
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
     }

    //トリガーモードで他のオブジェクトと接触した場合の処理(追加４)
    void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合(追加４)
        if(other.gameObject.tag=="CarTag"||other.gameObject.tag=="TrafficConeTag")
        {
            this.isEnd = true;

            //stateTextにGAME OVERを表示(追加７)
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //ゴール地点に到達した場合(追加４)
        if(other.gameObject.tag=="GoalTag")
        {
            this.isEnd = true;

            //stateTextにGAME CLEARを表示(追加７)
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //コインに衝突した場合(追加５)
        if(other.gameObject.tag=="CoinTag")
        {
            //スコアを加算(追加８)
            this.score += 10;

            //ScoreTextに獲得した点数を表示(追加８)
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            //パーティクルを再生(追加６)
            GetComponent<ParticleSystem>().Play();

            //接触したコインのオブジェクトを破棄(追加５)
            Destroy(other.gameObject);
        }

    }

    //ジャンプボタンを押した場合の処理(追加９)
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    //ジャンプボタンを離した場合の処理(追加９)
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    //左ボタンを押し続けた場合の処理(追加９)
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    //左ボタンを離した場合の処理(追加９)
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理(追加９)
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    //右ボタンを離した場合の処理(追加９)
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;    
    }
}
