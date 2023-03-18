using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDestroy : MonoBehaviour
{

    public void destroy()
    {
        // Need to instantiate a smoke object here
        Destroy(gameObject);
    }


}
