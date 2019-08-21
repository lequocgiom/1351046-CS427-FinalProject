using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour {

    public static PoolingManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject UseObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject temp = Instantiate(obj, pos, rot);
        temp.SetActive(true);
        return temp;

    }

    public void ReturnObject(GameObject obj, float delay = 0f)
    {
        Destroy(obj, delay);
    }
}
