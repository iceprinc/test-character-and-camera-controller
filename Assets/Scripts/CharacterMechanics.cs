using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    public float speedMove;
    public float jumpPower;
    private float gravityForce;
    private Vector3 moveVector;

    private CharacterController chController;
    private Animator chAnimator;
    private MobileController mContr;
    void Start()
    {
        chController = GetComponent<CharacterController>();
        chAnimator = GetComponent<Animator>();
        //if android && !unity editor
        mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
    }
    void Update()
    {
        CharacterMove();
        GamingGravity(); 
    }
    private void CharacterMove()
    {
        if (chController.isGrounded)
        {
            chAnimator.ResetTrigger("Jump");
            chAnimator.SetBool("Falling",false);

            moveVector = Vector3.zero;


            //if android && !unity editor
                moveVector.x = mContr.Horizontal() * speedMove;
                moveVector.z = mContr.Vertical() * speedMove;
            
            //else
                //moveVector.x = Input.GetAxis("Horizontal") * speedMove;
                //moveVector.z = Input.GetAxis("Vertical") * speedMove;

            //анимация ходьбы
            if (moveVector.x != 0 || moveVector.z != 0)
                chAnimator.SetBool("Move", true);
            else
                chAnimator.SetBool("Move", false);

            //поворот
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }
        else
        {
            if (gravityForce < -1.1f)
                chAnimator.SetBool("Falling",true);
        }

        moveVector.y = gravityForce;
        chController.Move(moveVector*Time.deltaTime);
    }
    private void GamingGravity()
    {
        if (!chController.isGrounded)
            gravityForce -= 4f * Time.deltaTime;
        else
            gravityForce = -1;

        if (Input.GetKeyDown(KeyCode.Space) && chController.isGrounded)
        {
            gravityForce = jumpPower;
            chAnimator.SetTrigger("Jump");
        }
    }
}
