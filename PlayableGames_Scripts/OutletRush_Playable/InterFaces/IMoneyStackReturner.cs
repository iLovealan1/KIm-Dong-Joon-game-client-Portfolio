using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public interface IMoneyStackReturner 
    {
        Stack<Money> GetMoneyStack(out Action doneCallback);
    }
}