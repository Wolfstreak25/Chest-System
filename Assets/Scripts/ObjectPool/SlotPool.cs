using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPool : ObjectPool<ChestSlotController>
{
    ChestSlotController slotController;
    RectTransform content;
    public ChestSlotController GetSlot (ChestSlotController _controller, RectTransform _content)
    {
        slotController = _controller;
        content = _content;
        return GetItem();
    }
    protected override ChestSlotController CreateItem()
    {
        ChestSlotController controller = Instantiate<ChestSlotController>(slotController);
        controller.transform.SetParent(content);
        controller.transform.localScale = Vector3.one;
        return controller;
    }
}