using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    public Animator camAnim;

    public void enemyShotShake()
    {
        camAnim.SetTrigger("enemyShootShake");
    }

    public void playerRevolverShot()
    {
        camAnim.SetTrigger("RegularShoot");
    }
}
