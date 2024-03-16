using UnityEngine;

public class PlatformMoveY : MonoBehaviour
{
    public float speed = 0f;
    public float minY = 0f;
    public float maxY = 0f;

    private void Update()
    {
        // Get the current position of the platform.
        Vector3 position = transform.position;

        // Move the platform to the left.
        position.y -= speed * Time.deltaTime;

        // Check if the platform has reached the left edge of the range.
        if (position.y < minY)
        {
            // Reverse the direction of the platform.
            speed = -speed;
        }

        // Check if the platform has reached the right edge of the range.
        if (position.y > maxY)
        {
            // Reverse the direction of the platform.
            speed = -speed;
        }

        // Set the new position of the platform.
        transform.position = position;
    }
}
