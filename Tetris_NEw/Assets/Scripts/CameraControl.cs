using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviourSingleton<CameraControl>
{
    [SerializeField] private Camera camera;
    public void ChangeSizeCamera(int value)
    {
        camera.orthographicSize = value;
    }

  
}
