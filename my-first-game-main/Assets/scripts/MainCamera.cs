using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Animator _cameraAnim;

    private void Start()
    {
        _cameraAnim = gameObject.GetComponent<Animator>();
    }
    public void CameraShake()
    {
        _cameraAnim.SetTrigger("PlayerHit");
    }
}
