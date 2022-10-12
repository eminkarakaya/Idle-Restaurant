using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextParse : Text
{
    public override Color color { get => base.color; set => base.color = value; }
    public void Check(int value)
    {
        if(GameManager.instance.GetPara()<value)
            color =  Color.red;
        else
        {
            color = Color.white;
        }
    }
}
