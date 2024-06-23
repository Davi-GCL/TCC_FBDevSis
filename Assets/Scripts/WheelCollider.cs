using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Roda entrou em: {other.gameObject.name}");
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Roda colidindo em: {collision.gameObject.name}");
    }
}
