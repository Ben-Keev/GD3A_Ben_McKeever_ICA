using UnityEngine;

/// <summary>
/// Tracks player's x and y movement
/// </summary>
public class FollowPlayer : MonoBehaviour
{

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Operations ensure player remains at center of camera
        transform.position = new Vector3(player.transform.position.x + 4, transform.position.y, player.transform.position.z - 15);
    }
}
