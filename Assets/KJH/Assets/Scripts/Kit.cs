using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kit : MonoBehaviour
{
    public static Kit instance;
    void Awake()
    {
        instance = this;
        COUNT = 2;
    }

    int count;

    public int COUNT
    {
        get { return count; }
        set
        {
            count = value;
        }
    }

    void start()
    {
        //COUNT = 2;
    }
}
