using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICheck : MonoBehaviour
{
    [SerializeField] GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(true);
    }
}
