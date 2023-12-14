using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScMapManagor : MonoBehaviour
{
    public static ScMapManagor Instance;
    public int roomCount = 0;

    public UnityEvent getYourNeighbors;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void RoomReady()
    {
        roomCount--;
        if (roomCount == 0)
        {
            getYourNeighbors.Invoke();
        }
    }
}