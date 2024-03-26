using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSettings", menuName = "Player Settings", order = 0)]

public class PlayerSettings : ScriptableObject
{
    public float Speed = 2f;
    public LayerMask ComputerLayerMask, BedLayerMask;
    public Camera MainCam;
    public Rigidbody rb;
    public Animator Anim;
    public Texture2D CursorArrow, CursorHand, ComputerArrow;

}
