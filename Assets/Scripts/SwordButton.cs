using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordButton : MonoBehaviour
{
    public PlayerController playerController;
    public Image image;

    public Color activeColor;
    public Color inactiveColor;

    void Update() {
    if (playerController.defaultAbility)
        {
            image.color = activeColor;
        }
        else
        {
            image.color = inactiveColor;
        }
    }

    public void OnClick(){
        playerController.defaultAbility = true;
        playerController.doubleJumpAbility = false;
        playerController.specialAttackAbility = false;
    }
}
