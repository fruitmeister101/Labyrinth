using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicCharacter
{
    protected override void Awake()
    {
        base.Awake();
        nav.updateRotation = false;
        nav.updatePosition = false;
    }
}
