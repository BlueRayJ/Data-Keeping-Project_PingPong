using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

    }
}
