using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    public Transform objetivo;       // Drag player here in Inspector
    public float velocidadSuave = 0.125f; 
    public Vector3 offset = new Vector3(0, 0, -10); // Maintain distance on Z-axis

    void FixedUpdate()
    { // LateUpdate ensures player movement is finished first
        if (objetivo != null)
        {
            Vector3 posicionBuscada = objetivo.position + offset;
            // Use Lerp for smooth movement
            transform.position = Vector3.Lerp(transform.position, posicionBuscada, velocidadSuave);
        }
    }


}
