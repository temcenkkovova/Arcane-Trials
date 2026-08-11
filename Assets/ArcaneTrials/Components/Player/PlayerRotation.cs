using UnityEngine;


public class PlayerRotation : MonoBehaviour
{
  private PlayerStats playerStats;

  public void Init(PlayerStats playerStats)
  {
    this.playerStats = playerStats;
  }

  public void RotateByMouse(Vector2 input)
  {
    Vector3 mouseDir = new Vector3();
    Ray ray = Camera.main.ScreenPointToRay(input);
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);


    if (groundPlane.Raycast(ray, out float distance))
    {
      Vector3 mouseWorldPos = ray.GetPoint(distance);
      mouseDir = mouseWorldPos - transform.position;
    }
    mouseDir.y = 0f;
    if (mouseDir.sqrMagnitude < 0.01)
      return;

    mouseDir.Normalize();


    Quaternion targetRotation = Quaternion.LookRotation(mouseDir);

    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerStats.RotationSpeed * Time.deltaTime);


  }
}