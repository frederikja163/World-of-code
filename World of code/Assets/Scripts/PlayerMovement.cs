using UnityEngine;

/// <summary>
/// Controlls the movement and rotation of the player.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables
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
    /// Animator, used to animate the player
    /// </summary>
    private Animator animator = null;
    private new Collider collider = null;

    /// <summary>
    /// Map position of the player character
    /// </summary>
    public Vector2Int mapPosition = Vector2Int.zero;
    #endregion Variables

    #region Unity calls
    /// <summary>
    /// Checks if we have the required components upon startup
    /// </summary>
    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = transform;
        }

        animator = playerTransform.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("PlayerMovement can use an animator component, however none was assigned.");
        }
        collider = GetComponent<Collider>();
    }

    /// <summary>
    /// Calls the different updates, every frame
    /// </summary>
    private void Update()
    {
        Vector3 positionChange = UpdateMovement();
        UpdateAnimation(positionChange);
    }
    #endregion Unity calls

    #region Updates
    /// <summary>
    /// Updates the animation of the player
    /// </summary>
    /// <param name="positionChange">How much the position has changed over the last frame. See UpdateMovement</param>
    private void UpdateAnimation(Vector3 positionChange)
    {
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
    }

    /// <summary>
    /// Updates the rotation, called every frame
    /// </summary>
    /// <param name="positionChange">The change in positon AKA velocity used to calculate rotation</param>
    private void UpdateRotation(Vector3 positionChange)
    {
        float newAngle = Mathf.Rad2Deg * Mathf.Atan2(positionChange.x, positionChange.z);
        
        playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, 
            Quaternion.AngleAxis(newAngle, Vector3.up), 10);
    }

    /// <summary>
    /// Updates the movement based on input axis horizontal and vertical
    /// </summary>
    /// <returns>The change in position used for UpdateRotation</returns>
    private Vector3 UpdateMovement()
    {
        Vector3 positionChange = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime * Vector3.right
            + Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime * Vector3.forward;


        Vector2Int newPosition = Map.WorldToMapPosition(transform.position + positionChange);
        if (newPosition != mapPosition)
        {
            if (EnterTile(newPosition))
            {
                transform.position += positionChange;
            }
            else
            {
                Vector2Int mapPositionChange = newPosition - mapPosition;
                transform.position += new Vector3(mapPositionChange.x == 0 ? positionChange.x : 0, 0,
                    mapPositionChange.y == 0 ? positionChange.z : 0);

            }
        }
        else
        {
            transform.position += positionChange;
        }

        return positionChange;
    }
    #endregion Updates

    /// <summary>
    /// Called upon entering a new tile, manages stuff like collision checking ext.
    /// </summary>
    /// <param name="tilePosition">the position of the new tile</param>
    private bool EnterTile(Vector2Int tilePosition)
    {

        if (!Collider.CheckPosition(tilePosition))
        {
            return false;
        }

        mapPosition = tilePosition;
        collider.position = mapPosition;
        Map.Instance.UpdateMap();
        return true;
    }

}
