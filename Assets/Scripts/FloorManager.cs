using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject[] floorObjects;

    public void ActivateFloor(int index)
    {
        for (int i = 0; i < floorObjects.Length; i++)
        {
            floorObjects[i].SetActive(i == index);
        }
    }
}
