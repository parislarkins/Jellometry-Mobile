using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
    // Initialize
	private Animator _animator;
	private CanvasGroup _canvasGroup;
	
    // Animation state
	public bool IsOpen{
		get { return _animator.GetBool ("IsOpen");}
		set { _animator.SetBool ("IsOpen", value);}
		
	}
	
	public void Awake(){
        // Set variables
		_animator = GetComponent<Animator> ();
		_canvasGroup = GetComponent<CanvasGroup> ();
		
        // Centre GUI panel on screen
		var rect = GetComponent<RectTransform> ();
		rect.offsetMax = rect.offsetMin = new Vector2 (0, 0);
		
	}
	
	public void Update(){

		// Dissable and enable canvases/panels
		if (!_animator.GetCurrentAnimatorStateInfo (0).IsName ("Open")) {
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
		} 
		else {
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;			
		}
	}
}
