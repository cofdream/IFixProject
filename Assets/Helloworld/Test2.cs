using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Func();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [IFix.Patch]
    void Func()
    {
        Debug.Log("2222");
    }
}
