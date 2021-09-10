using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PARTSTYPE_Component : MonoBehaviour
{
    public PARTSTYPE PARTSTYPE_cp;
    [SpineAttachment] public string partsname;

    public PARTSTYPE_Component()
    {
        
    }
    public PARTSTYPE_Component(PARTSTYPE _type, string _name)
    {
        PARTSTYPE_cp = _type;
        partsname = _name;
    }
}
