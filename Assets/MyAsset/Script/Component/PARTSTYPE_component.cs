using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PARTSTYPE_component : MonoBehaviour
{
    public PARTSTYPE PARTSTYPE_cp;
    [SpineAttachment] public string partsname;

    public PARTSTYPE_component()
    {
        
    }
    public PARTSTYPE_component(PARTSTYPE _type, string _name)
    {
        PARTSTYPE_cp = _type;
        partsname = _name;
    }
}
