using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    public List<GameObject> ControllerPrefabs;
    private InputDevice _targetDevice;
    void Start()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        if(devices.Count > 0)
        {
            _targetDevice = devices[0];
            var prefab = ControllerPrefabs.Find(x => x.name == _targetDevice.name);
            if (prefab)
                Instantiate(prefab, transform);
            else
            {
                Debug.LogError("Controller prefab is missing");
                Instantiate(ControllerPrefabs[0], transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
