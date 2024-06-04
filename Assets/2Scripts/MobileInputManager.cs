using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileInputManager : MonoBehaviour
{
    Vector3 position;

	[SerializeField] GameObject left; // 왼쪽 버튼
	[SerializeField] GameObject right; // 오른쪽 버튼
	[SerializeField] GameObject up; // 위 버튼
	[SerializeField] GameObject down; // 아래 버튼
    [SerializeField] GameObject space; // 스페이스 버튼

    Vector3 touchedPos;
    int fingerId;
    bool isTouch;
    bool isLeft;
    bool isRight;
    bool isUp;
    bool isDown;
    bool isSpace;

    void Start()
    {
        EventTrigger.Entry entry_PointerDown = new ();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { LeftButtonPressed(); }); // PointerEventData)data
        left.GetComponent<EventTrigger>().triggers.Add(entry_PointerDown);
        
        // left.GetComponent<Button>().onClick.AddListener(LeftButtonPressed);
    }

    void Update()
    {
        isLeft = false;
        isRight = false;
        isUp = false;
        isDown = false;

        for (int i=0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase)
            {
                case TouchPhase.Began: // at first
                    
                    break;
                case TouchPhase.Moved: // moving
                    if (fingerId == touch.fingerId)
                    {
                        // touchedPos = touch.position;
						// fingerId = touch.fingerId;
                    }
                    break;
                case TouchPhase.Ended: // del
                    if (fingerId == touch.fingerId)
                    {
                        fingerId = -1;
                    }
                    break;
            }
            isLeft |= left.GetComponent<SpriteRenderer>().bounds.Contains(touch.position);
            isRight |= right.GetComponent<SpriteRenderer>().bounds.Contains(touch.position);
            isUp |= up.GetComponent<SpriteRenderer>().bounds.Contains(touch.position);
            isDown |= down.GetComponent<SpriteRenderer>().bounds.Contains(touch.position);
			isSpace |= space.GetComponent<SpriteRenderer>().bounds.Contains(touch.position);

            if (isLeft || isRight)
			{
				if (isLeft)
					InGameManager.IT.playerTankHandler.LeftArrowPressed();
				else if (isRight)
					InGameManager.IT.playerTankHandler.RightArrowPressed();
			}
            if (isSpace)
            {
                InGameManager.IT.playerTankHandler.SpacePressed();
            }
            else
            {
                InGameManager.IT.playerTankHandler.SpaceUp();
            }
        }
    }

    void LeftButtonPressed()
    {
        InGameManager.IT.playerTankHandler.LeftArrowPressed();
    }
}