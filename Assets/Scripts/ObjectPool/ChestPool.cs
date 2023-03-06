using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPool : ObjectPool<ChestController>
{
    private ChestModel model;
    private ChestView view;
    private Transform spawn;
    public ChestController GetChest (ChestObject _object, Transform _spawn)
    {
        model = new ChestModel(_object);
        view = _object.chestPrefab;
        spawn = _spawn;
        return GetItem();
    }
    protected override ChestController CreateItem()
    {
        ChestController controller = new ChestController(model, view, spawn);
        return controller;
    }
}
