﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Entity {

    private bool isMain;
    private bool isCrouching;
    private bool isSprinting;

    public void Instantiate(string clientId, bool alive, Vector3 location, bool isMain) {
        this.Instantiate(clientId, alive, location);
        this.isMain = isMain;
        this.isCrouching = false;
        this.isSprinting = false;
    }

    public bool IsMainPlayer { get { return this.isMain; } }

}