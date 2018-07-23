using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnnouncerManager : MonoBehaviour {

	public Text text;
	public Animator anim;
	public Image bg;

	void Start(){
		anim = GetComponent<Animator>();
		bg.color = Color.white;
	}

	public void BroadcastMessage(Color color, string message){
		anim.Rebind();
		bg.color = color;
		text.text = message;
		anim.Play("show");
	}
}
