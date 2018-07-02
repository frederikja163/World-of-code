using UnityEngine;

/// <summary>
/// Controlls the movement and rotation of the player.
/// TODO: Make this component also controll the animation of the player character.
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    /// <summary>
    /// Movement speed in all directions
    /// </summary>
    [SerializeField]
    private float movementSpeed = 10f;

    /// <summary>
    /// To use for rotation, camera will only follow movement of this transform.
    /// </summary>
    [SerializeField]
    private Transform playerTransform = null;

    /// <summary>
    /// Checks if we have the required components upon startup
    /// </summary>
    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = transform;
        }
    }

    /// <summary>
    /// Calls the different updates, every frame
    /// </summary>
    private void Update()
    {
        Vector3 positionChange = UpdateMovement();
        UpdateRotation(positionChange);
    }

    /// <summary>
    /// Updates the rotation, called every frame
    /// </summary>
    /// <param name="positionChange">The change in positon AKA velocity used to calculate rotation</param>
    private void UpdateRotation(Vector3 positionChange)
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan2(positionChange.x, positionChange.z);

        playerTransform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    /// <summary>
    /// Updates the movement based on input axis horizontal and vertical
    /// </summary>
    /// <returns>The change in position used for UpdateRotation</returns>
    private Vector3 UpdateMovement()
    {
        Vector3 positionChange = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime * Vector3.right
            + Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime * Vector3.forward;

        transform.position += positionChange;

        return positionChange;
    }

}
