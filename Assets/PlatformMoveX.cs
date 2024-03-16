using UnityEngine;

public class PlatformMoveX : MonoBehaviour
{
    public float speed = 0f;
    public float minX = 0f;
    public float maxX = 0f;

    private void Update()
    {
        // Get the current position of the platform.
        Vector3 position = transform.position;

        // Move the platform to the left.
        position.x -= speed * Time.deltaTime;

        // Check if the platform has reached the left edge of the range.
        if (position.x < minX)
        {
            // Reverse the direction of the platform.
            speed = -speed;
        }

        // Check if the platform has reached the right edge of the range.
        if (position.x > maxX)
        {
            // Reverse the direction of the platform.
            speed = -speed;
        }

        // Set the new position of the platform.
        transform.position = position;
    }
}
