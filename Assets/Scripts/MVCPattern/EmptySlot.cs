using System;
using System.Collections.Generic;
using UnityEngine;

public class EmptySlot : MonoBehaviour
{
    private ChestController Controller;
    public void SetController(ChestController _controller)
    {
        this.Controller = _controller;
    }
}