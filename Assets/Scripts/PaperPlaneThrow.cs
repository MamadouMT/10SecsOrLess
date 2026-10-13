
using UnityEngine;

public class PaperPlaneThrow : MonoBehaviour
{
    public GameObject paperPlanePrefab;
    public Transform throwPoint;
    public Camera gameCamera;
    public RectTransform crosshair;

    public float throwForce = 15f;
	public float throwCooldown = 0.5f;
    private float nextThrowTime = 0f;

    void Start()
    {
        // Keep the mouse visible so the player can aim
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        // Make the crosshair follow the mouse
        if (crosshair != null)
        {
            crosshair.position = Input.mousePosition;
        }

        // Throw the plane with left-click
		if (Input.GetMouseButtonDown(0) && Time.time >= nextThrowTime)
		{
    		ThrowPlane();
    		nextThrowTime = Time.time + throwCooldown;
		}
    }

    void ThrowPlane()
    {
        if (paperPlanePrefab == null ||
            throwPoint == null ||
            gameCamera == null)
        {
            Debug.LogWarning("Assign the throwing references in the Inspector!");
            return;
        }

        // Find the direction from the camera through the mouse
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        Vector3 targetPoint;

        // Aim at an object if the ray hits one
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            targetPoint = hit.point;
        }
        else
        {
            // Otherwise, aim into the distance
            targetPoint = ray.GetPoint(100f);
        }

        // Calculate the direction from the plane's spawn point
        Vector3 direction = (targetPoint - throwPoint.position).normalized;
        Quaternion aimRotation = Quaternion.LookRotation(direction);
        //aimRotation *= Quaternion.Euler(0f, 180f, 0f);

        // Spawn the plane facing the target
        GameObject plane = Instantiate(
            paperPlanePrefab,
            throwPoint.position,
            aimRotation
        );
        
        Destroy(plane, 5f);

        // Play the existing animation
        Animator animator = plane.GetComponentInChildren<Animator>(true);

        if (animator != null && animator.gameObject.activeInHierarchy)
        {
            animator.Play("Take 001", 0, 0f);
        }

        // Launch toward the crosshair
        Rigidbody rb = plane.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
        }
    }
}
