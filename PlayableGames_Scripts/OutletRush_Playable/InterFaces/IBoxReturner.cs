using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public interface IBoxReturner
    {
        Box GetBoxObject(out Action doneCallback);
    }
}
