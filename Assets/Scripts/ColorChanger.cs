using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public string handTag = "PlayerHand";

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} touched by {other.name}");

        if (other.CompareTag(handTag))
        {
            Color newColor = new Color(Random.value, Random.value, Random.value);
            GetComponent<Renderer>().material.color = newColor;
            Debug.Log($"{gameObject.name} color changed to {newColor}");
        }
    }
}