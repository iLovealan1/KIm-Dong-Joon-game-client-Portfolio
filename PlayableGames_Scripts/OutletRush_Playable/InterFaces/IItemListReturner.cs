using System;
using System.Collections.Generic;

namespace luna_sportshop.Playable002
{
    public interface IItemListReturner
    {
        List<Item> GetItemList(out Action doneCallBack);
    }
}