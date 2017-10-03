﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public CameraInScene camera_in_scene;
	public Tiles tiles;

    static World mInstance = null;

    public static World Instance
    {
        get
        {
            return mInstance;
        }
    }
    void Awake()
    {
       mInstance = this;
		tiles = GetComponent<Tiles> ();
    }
}