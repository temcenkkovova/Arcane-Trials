using System;
using System.Collections;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
  private CharacterController characterController;
  public float dodgeSpeed = 10f;
  public event Action OnFinishDash;

  private float dashCooldawn = 2f;
  public bool CanDash;

  void OnEnable()
  {
    CanDash = true;
  }
  void Awake()
  {
    characterController = GetComponent<CharacterController>();

  }


  public void Dodge(Vector3 direction)
  {
    CanDash = false;
    characterController.Move(direction * dodgeSpeed * Time.deltaTime);

    StartCoroutine(dashCoroutine());
    StartCoroutine(dashCooldawnCoroutine());
  }

  private IEnumerator dashCoroutine()
  {
    yield return new WaitForSeconds(0.3f);
    OnFinishDash?.Invoke();
  }
  private IEnumerator dashCooldawnCoroutine()
  {
    yield return new WaitForSeconds(dashCooldawn);
    CanDash = true;
  }
}