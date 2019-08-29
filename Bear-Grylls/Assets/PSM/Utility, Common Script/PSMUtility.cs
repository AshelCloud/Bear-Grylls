using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSMUtility
{
    public static object NewObjectAsType(Type t)
    {
        try
        {
            return t.GetConstructor(new Type[] { }).Invoke(new object[] { });
        }
        catch
        {
            return null;
        }
    }
}
