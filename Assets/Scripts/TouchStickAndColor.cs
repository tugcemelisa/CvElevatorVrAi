using UnityEngine;

public class TouchStickAndColor : MonoBehaviour
{
    public string[] handTags = { "PlayerHand", "Controller" };

    private Transform originalParent;
    private Rigidbody rb;
    private Renderer objRenderer;

    void Start()
    {
        originalParent = transform.parent;
        rb = GetComponent<Rigidbody>();
        objRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (var tag in handTags)
        {
            if (other.CompareTag(tag))
            {
                Debug.Log($"{gameObject.name} touched by {other.name}");

              
                if (objRenderer != null)
                {
                    objRenderer.material.color = new Color(Random.value, Random.value, Random.value);
                }

                
                if (rb != null)
                {
                    transform.SetParent(other.transform);
                    rb.isKinematic = true;
                }

                break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (var tag in handTags)
        {
            if (other.CompareTag(tag))
            {
                if (rb != null)
                {
                    transform.SetParent(originalParent);
                    rb.isKinematic = false;
                }
                break;
            }
        }
    }
}