#define SOME_SYMBOL
#if SOME_SYMBOX
// Symbol is already defined
#else
// Symbol is undefined
#endif

#undef SOME_SYMBOL
#if SOME_SYMBOL
// Symbol is already defined
#else
//Symbol is undefined
#endif

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;


public class CodeBranch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
