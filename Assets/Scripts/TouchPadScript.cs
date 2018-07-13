using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPadScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public static TouchPadScript instance;

	private bool touched;
	private int pointerID;
	void Awake(){
		instance = this;
	}

	void OnEnable(){
		touched = false;
	}

	public void OnPointerDown(PointerEventData data){

		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			HammerControllerScript.instance.OnInput ();
		}
	}

	public void OnPointerUp(PointerEventData data){
		if (data.pointerId == pointerID) {
			touched = false;
		}
	}
}
