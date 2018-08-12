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
<<<<<<< HEAD:World of code/Assets/PlayerMovement.cs
<<<<<<< HEAD:World of code/Assets/Scripts/PlayerMovement.cs
    /// <summary>
    /// Animator to animate the character
    /// </summary>
    private Animator animator = null;
=======
>>>>>>> parent of 3193f37... 7/5 lifestream:World of code/Assets/PlayerMovement.cs

    public Vector2Int playerPosition { get; private set; }
=======
>>>>>>> parent of 3193f37... 7/5 lifestream:World of code/Assets/PlayerMovement.cs

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
<<<<<<< HEAD:World of code/Assets/PlayerMovement.cs
<<<<<<< HEAD:World of code/Assets/Scripts/PlayerMovement.cs
        //NOTE: PositionChange.magnitude / Time.deltaTime = movementspeed
        if (positionChange.magnitude / Time.deltaTime >= 3)
        {
            animator.SetBool("Running", true);
            UpdateRotation(positionChange);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        
        if ((int)transform.position.x - positionChange.x != (int)transform.position.x ||
            (int)transform.position.z - positionChange.z != (int)transform.position.z)
        {
            Map.Instance.UpdateMap();
        }
=======
        UpdateRotation(positionChange);
>>>>>>> parent of 3193f37... 7/5 lifestream:World of code/Assets/PlayerMovement.cs
=======
        UpdateRotation(positionChange);
>>>>>>> parent of 3193f37... 7/5 lifestream:World of code/Assets/PlayerMovement.cs
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
        Vector3 positionChange = Input.GetAxis("Horizontal") * Vector3.right
            + Input.GetAxis("Vertical") * Vector3.forward;

        positionChange = positionChange * movementSpeed * Time.deltaTime;

        //if (positionChange + playerPosition != playerPosition)
        {

        }
        playerPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
        transform.position += positionChange;

        return positionChange;
    }

}
