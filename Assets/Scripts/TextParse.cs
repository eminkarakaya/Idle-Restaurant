using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextParse : Text
{
    Color _color = new Color(49,207,26,255);
    public override Color color { get => base.color; set => base.color = value; }
    public void Check(int value)
    {
        if(GameManager.instance.GetMoney()<value)
            color =  Color.red;
        else
        {
            color = _color;
        }
    }
}
