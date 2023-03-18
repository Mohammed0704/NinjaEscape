using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJumpButton : MonoBehaviour
{
    public PlayerController playerController;
    public Image image;

    public Color activeColor;
    public Color inactiveColor;

    void Update() {
    if (playerController.doubleJumpAbility)
        {
            image.color = activeColor;
        }
        else
        {
            image.color = inactiveColor;
        }
    }

    public void OnClick(){
        playerController.doubleJumpAbility = true;
        playerController.specialAttackAbility = false;
        playerController.defaultAbility = false;
    }

}
