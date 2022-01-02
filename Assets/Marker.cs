using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public int mark {get; private set;}
    public int index;
    public void SetMark()
    {
        
        mark = int.Parse(GetComponent<TMPro.TMP_InputField>().text);
        
    }

}
