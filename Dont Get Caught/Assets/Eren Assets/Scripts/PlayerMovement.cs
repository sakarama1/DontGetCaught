using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform pad;

    public Transform Player;
    private Vector3 moveDir;
    public float speed;
    //private bool isTipToe = false;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);
        moveDir = new Vector3(transform.localPosition.x, 0, transform.localPosition.y).normalized; //direction joystick points to

        //Animation sync
        //if (!isTipToe)
        //{
        //    isTipToe = true;
        //    Player.GetComponentInChildren<Animator>().SetBool("TipToe", true);
        //}
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine("Move"); //move when joystick is touched
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        moveDir = Vector3.zero;  //player doesnt move once we let go of the joystick
        StopCoroutine("Move"); //stop movement once joystick is let go

        //Animation
        //isTipToe = false;
        //Player.GetComponentInChildren<Animator>().SetBool("TipToe", false);
    }

    IEnumerator Move()
    {
        while (true)
        {
            Player.Translate(-moveDir * speed * Time.deltaTime, Space.World); //move the player in the corresponding direction

            if (moveDir != Vector3.zero)
            {
                Player.rotation = Quaternion.Slerp(Player.rotation, Quaternion.LookRotation(moveDir), 5 * Time.deltaTime); //player faces where it moves
            }
            yield return null;
        }
    }
}
