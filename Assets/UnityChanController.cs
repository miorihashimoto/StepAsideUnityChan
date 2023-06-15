using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//�ǉ��V

public class UnityChanController : MonoBehaviour
{
    //�A�j���[�V���������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;

    //Unity�������ړ�������R���|�[�l���g������i�ǉ��j
    private Rigidbody myRigidbody;

    //�O�����̑��x�i�ǉ��j
    private float velocityZ = 16f;

    //�������̑��x(�ǉ��Q)
    private float velocityX = 10f;

    //������̑��x(�ǉ��R)
    private float velocityY = 10f;

    //���E�̈ړ��ł���͈�(�ǉ��Q)
    private float movableRange = 3.4f;

    //����������������W��(�ǉ��S)
    private float coefficient = 0.99f;

    //�Q�[���I���̔���(�ǉ�4)
    private bool isEnd = false;

    //�Q�[���I�����ɕ\������e�L�X�g(�ǉ��V)
    private GameObject stateText;

    //�X�R�A��\������e�L�X�g(�ǉ��W)
    private GameObject scoreText;

    //���_(�ǉ��W)
    private int score = 0;

    //���{�^�������̔���(�ǉ��X)
    private bool isLButtonDown = false;

    //�E�{�^�������̔���(�ǉ��X)
    private bool isRButtonDown = false;

    //�W�����v�{�^�������̔���(�ǉ��X)
    private bool isJButtonDown = false;


    // Start is called before the first frame update
    void Start()
    {
        //Animator�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //����A�j���[�V�������J�n
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbody�R���|�[�l���g���擾�i�ǉ��j
        this.myRigidbody = GetComponent<Rigidbody>();

        //�V�[������stateText�I�u�W�F�N�g���擾(�ǉ��V)
        this.stateText = GameObject.Find("GameResultText");

        //�V�[������scoreText�I�u�W�F�N�g���擾(�ǉ��W)
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���I���Ȃ�Unity�����̓�������������(�ǉ��S)
        if(this.isEnd)
        {
            this.velocityZ *= this.coefficient;

            this.velocityX *= this.coefficient;

            this.velocityY *= this.coefficient;

            this.myAnimator.speed *= this.coefficient;

        }

        //�������̓��͂ɂ�鑬�x(�ǉ��Q)
        float inputVelocityX = 0;

        //������̓��͂ɂ�鑬�x(�ǉ��R)
        float inputVelocityY = 0;

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������(�ǉ��Q)
        if((Input.GetKey(KeyCode.LeftArrow)||this.isLButtonDown)&&-this.movableRange<this.transform.position.x)//�ǉ��X
        {
            //�������ւ̑��x����(�ǉ��Q)
            inputVelocityX = -this.velocityX;
        }

        else if((Input.GetKey(KeyCode.RightArrow)||this.isRButtonDown)&&this.transform.position.x<this.movableRange)//�ǉ��X
        {
            //�E�����ւ̑��x����(�ǉ��Q)
            inputVelocityX = this.velocityX;
        }

        //�W�����v���Ă��Ȃ��Ƃ��ɃX�y�[�X�������ꂽ��W�����v����(�ǉ��R)
        if((Input.GetKeyDown(KeyCode.Space)||this.isJButtonDown)&&this.transform.position.y<0.5f)//�ǉ��X
        {
            //�W�����v�A�j�����Đ�(�ǉ��R)
            this.myAnimator.SetBool("Jump", true);

            //������ւ̑��x����(�ǉ��R)
            inputVelocityY = this.velocityY;
        }

        else
        {
            //���݂�y���̑��x����(�ǉ��R)
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����(�ǉ��R)
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        //Unity�����ɑ��x��^����i�ǉ��j(�ύX)(�ύX�Q)
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
     }

    //�g���K�[���[�h�ő��̃I�u�W�F�N�g�ƐڐG�����ꍇ�̏���(�ǉ��S)
    void OnTriggerEnter(Collider other)
    {
        //��Q���ɏՓ˂����ꍇ(�ǉ��S)
        if(other.gameObject.tag=="CarTag"||other.gameObject.tag=="TrafficConeTag")
        {
            this.isEnd = true;

            //stateText��GAME OVER��\��(�ǉ��V)
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //�S�[���n�_�ɓ��B�����ꍇ(�ǉ��S)
        if(other.gameObject.tag=="GoalTag")
        {
            this.isEnd = true;

            //stateText��GAME CLEAR��\��(�ǉ��V)
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //�R�C���ɏՓ˂����ꍇ(�ǉ��T)
        if(other.gameObject.tag=="CoinTag")
        {
            //�X�R�A�����Z(�ǉ��W)
            this.score += 10;

            //ScoreText�Ɋl�������_����\��(�ǉ��W)
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            //�p�[�e�B�N�����Đ�(�ǉ��U)
            GetComponent<ParticleSystem>().Play();

            //�ڐG�����R�C���̃I�u�W�F�N�g��j��(�ǉ��T)
            Destroy(other.gameObject);
        }

    }

    //�W�����v�{�^�����������ꍇ�̏���(�ǉ��X)
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    //�W�����v�{�^���𗣂����ꍇ�̏���(�ǉ��X)
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    //���{�^���������������ꍇ�̏���(�ǉ��X)
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    //���{�^���𗣂����ꍇ�̏���(�ǉ��X)
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //�E�{�^���������������ꍇ�̏���(�ǉ��X)
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    //�E�{�^���𗣂����ꍇ�̏���(�ǉ��X)
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;    
    }
}
