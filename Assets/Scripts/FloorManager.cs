using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [Header("Floor Settings")]
    public GameObject[] floorObjects;

    [Header("Skybox Settings")]
    public Material[] skyboxes;           
    public Material defaultSkybox;        

    public void ActivateFloor(int index)
    {
       
        for (int i = 0; i < floorObjects.Length; i++)
        {
            floorObjects[i].SetActive(i == index);
        }

        
        ChangeSkybox(index);
    }

    private void ChangeSkybox(int index)
    {
        if (skyboxes != null && index >= 0 && index < skyboxes.Length)
        {
            RenderSettings.skybox = skyboxes[index];
        }
        else if (defaultSkybox != null)
        {
            RenderSettings.skybox = defaultSkybox;
        }

        DynamicGI.UpdateEnvironment(); 
    }
}
