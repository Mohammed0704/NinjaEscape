using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireballButton : MonoBehaviour
{
    public PlayerController playerController;
    public Image image;

    public Color activeColor = Color.red;
    public Color inactiveColor = Color.white;

    void Update() {
    if (playerController.specialAttackAbility)
        {
            image.color = activeColor;
        }
        else
        {
            image.color = inactiveColor;
        }
    }

    public void OnClick(){
        playerController.specialAttackAbility = true;
        playerController.doubleJumpAbility = false;
    }
}
