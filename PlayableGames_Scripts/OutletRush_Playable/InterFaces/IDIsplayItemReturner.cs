using System;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public interface IDIsplayItemReturner 
    {
       List<Item> GetItemsFromDisplayShelf(Collider customerCol , out Action doneCallback);
    }
}