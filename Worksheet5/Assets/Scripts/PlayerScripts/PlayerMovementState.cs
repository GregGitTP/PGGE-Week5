using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using PGGE; 

public class PlayerMovementState : State
{
    float movementSpeed, rotationSpeed, runMultiplier, gravity, jumpForce, x, y, z;

    Transform camera, player;
    CharacterController cc;
    Animator anim;
    MonoBehaviour mb;

    TPCFollowTrackRotation tpc;
    RepositionCamera rc;

    Vector3 velocity = Vector3.zero;
    bool running = false;
    bool jump = false;
    bool crouch = false;

    public PlayerMovementState(FSM _fsm, Transform _camera, Transform _player, MonoBehaviour _mb, CharacterController _cc, Animator _anim, float _movementSpeed, float _rotationSpeed, float _runMultiplier, float _gravity, float _jumpForce, float _x, float _y, float _z) : base(_fsm){
        camera = _camera;
        player = _player;
        mb = _mb;
        cc = _cc;
        anim = _anim;
        movementSpeed = _movementSpeed;
        rotationSpeed = _rotationSpeed;
        runMultiplier = _runMultiplier;
        gravity = _gravity;
        jumpForce = _jumpForce;
        x = _x;
        y = _y;
        z = _z;
    }

    public override void Enter(){
        tpc = new TPCFollowTrackRotation(camera, player, x, y, z);
        rc = new RepositionCamera(camera, player, tpc);
    }

    public override void Exit(){
        tpc = null;
        rc = null;
    }

    public override void Update()
    {
        HandleInputs();
        Move();
        GameConstants.UpdateAmmoTxt();
    }

    public override void FixedUpdate(){
        ApplyGravity();
    }

    public override void LateUpdate(){
        tpc.Update();
        rc.Update();
    }

    private void HandleInputs(){
        if(Input.GetKeyDown(KeyCode.LeftShift)) running = true;
        
        if(Input.GetKeyUp(KeyCode.LeftShift)) running = false;

        if(Input.GetKeyDown(KeyCode.Space)) jump = true;

        if(Input.GetKeyUp(KeyCode.Space)) jump = false;

        if(Input.GetKeyDown(KeyCode.Tab)){
            crouch = !crouch;
            Crouch();
        }  
    }

    private void Move(){
        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        if (running && !(vert < 0)) vert *= runMultiplier;

        Vector3 move = player.forward * vert * movementSpeed;

        cc.Move(move*Time.deltaTime);

        player.Rotate(0f, hori*rotationSpeed*.3f*Time.deltaTime, 0f);

        anim.SetFloat("PosX", hori/2);
        anim.SetFloat("PosY", vert/2);

        if(jump && anim.GetCurrentAnimatorStateInfo(0).IsName("GroundMovement")) mb.StartCoroutine(Jump()); 
    }

    private void ApplyGravity(){
        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        if(cc.isGrounded && velocity.y < 0f) velocity.y = 0f;
    }

    public IEnumerator Jump(){
        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(.2f);
        velocity.y += jumpForce;
    }

    private void Crouch(){
        anim.SetBool("Crouch", crouch);
    }
}


