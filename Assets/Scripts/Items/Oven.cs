using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Item
{
    public Light ovenLight;
    public float cookingTime;
    public void VoiceCanvas()
    {
        GoldAnim.Instance.OvenAnim(transform);
    }
}
