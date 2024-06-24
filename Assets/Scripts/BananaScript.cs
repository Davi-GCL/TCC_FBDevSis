using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Velocidade da rotação

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player")) // Verifica se o objeto colidido é uma banana
    //    {
    //        Debug.Log("ESCOREGOU NA BANANINHA");
    //        StartCoroutine(RotateCar(other.GetComponentInParent<Transform>()));
    //    }
    //}

    //private IEnumerator RotateCar(Transform colliderObj)
    //{
    //    float angle = 0f;
    //    while (angle < 360f)
    //    {
    //        float rotationStep = rotationSpeed * Time.deltaTime * 360f;
    //        colliderObj.transform.Rotate(Vector3.up, rotationStep);
    //        angle += rotationStep;
    //        yield return null;
    //    }
    //}
}
