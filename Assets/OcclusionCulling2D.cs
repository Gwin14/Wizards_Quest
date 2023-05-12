using UnityEngine;

public class ViewFrustumCulling2D : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null && GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(cam), renderer.bounds))
            {
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }
    }
}
