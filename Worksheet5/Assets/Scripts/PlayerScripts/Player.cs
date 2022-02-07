using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Patterns;
using PGGE;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [Header("Component references")]
    public Transform camera;
    public Transform player;
    public CharacterController cc;
    public Animator anim;
    public Text ammoTxt;

    [Header("Values for character movement")]
    [Space(10)]
    public float movementSpeed = 2f;
    public float rotationSpeed = 240f;
    public float runMultiplier = 2f;
    public float gravity = -20f;
    public float jumpForce = 8f;

    [Header("The X, Z and Z axis offsets for the third person camera")]
    public float xOffset;
    public float yOffset;
    public float zOffset;

    FSM fsm = new FSM();
    State moveState, attackState, reloadState;
    PhotonView photonView;

    private void Start(){
        moveState = new PlayerMovementState(fsm, camera, player, this, cc, anim, movementSpeed, rotationSpeed, runMultiplier, gravity, jumpForce, xOffset, yOffset, zOffset);

        attackState = new PlayerAttackState(fsm, player, anim);

        reloadState = new PlayerReloadState(fsm, anim);
        
        fsm.Add(0, moveState);
        fsm.Add(1, attackState);
        fsm.Add(2, reloadState);

        fsm.SetCurrentState(moveState);

        photonView = GetComponent<PhotonView>();
    }

    private void Update(){
        if(photonView == null){
            Update_Internal();
            return;
        }
        else{
            if(photonView.IsMine == true){
                Update_Internal();
            }
        }
    }

    private void Update_Internal(){
        if(fsm.GetCurrentState() != fsm.GetState(2)){
            if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.C)) fsm.SetCurrentState(fsm.GetState(1));
            else if(Input.GetKey(KeyCode.R)) fsm.SetCurrentState(fsm.GetState(2));
            else fsm.SetCurrentState(fsm.GetState(0));
        }

        fsm.Update();
        ammoTxt.text = GameConstants.ammoTxt;
    }

    private void FixedUpdate(){
        if(photonView == null){
            fsm.FixedUpdate();
            return;
        }
        else{
            if(photonView.IsMine == true){
                fsm.FixedUpdate();
            }
        }
    }

    private void LateUpdate(){
        if(photonView == null){
            fsm.LateUpdate();
            return;
        }
        else{
            if(photonView.IsMine == true){
                fsm.LateUpdate();
            }
        }
    }
}
