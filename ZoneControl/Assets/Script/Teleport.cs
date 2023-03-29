using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public UnityEvent onEnter;
    public bool stopSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = destination.position;
            onEnter.Invoke();

        }
    }
}
