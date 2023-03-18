using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireballListener : MonoBehaviour
{
    public FireballShooter myShooter;
    public void FireReady()
    {
        myShooter.FireBullet();
    }
}
