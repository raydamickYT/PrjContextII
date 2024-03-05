using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSettings", menuName = "Player Settings", order = 0)]

public class PlayerSettings : ScriptableObject
{
    public float speed = 2f;
    public LayerMask computerLayerMask;
    public Camera MainCam;
    public Rigidbody rb;
    public Animator anim;
    public Texture2D cursorArrow, cursorHand;

}
