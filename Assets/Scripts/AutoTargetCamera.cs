using UnityEngine;
using Unity.Cinemachine; // Use Cinemachine for 3.x, or UnityEngine.Cinemachine for 2.x

public class AutoTargetCamera : MonoBehaviour
{
    private CinemachineCamera cmCamera; // Change to CinemachineVirtualCamera if using 2.x

    void Awake()
    {
        cmCamera = GetComponent<CinemachineCamera>();
    }

    void Start()
    {
        // Find the player in the new scene by their Tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && cmCamera != null)
        {
            cmCamera.Follow = player.transform;
            cmCamera.LookAt = player.transform;
        }
        else
        {
            Debug.LogWarning("AutoTargetCamera: Could not find an object tagged 'Player'.");
        }
    }
}
