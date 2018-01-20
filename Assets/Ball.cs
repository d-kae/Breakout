using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	private float speed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			this.GetComponent<Rigidbody> ().AddForce(Vector3.forward * -90f);
		}

//		if (Input.GetKey (KeyCode.Space) && startFlg) {
//			startFlg = false;
//			this.GetComponent<Rigidbody> ().AddForce ((transform.forward + transform.right) * speed, ForceMode.VelocityChange);
//		}
	}

	void OnCollisionEnter(Collision collision){
		//衝突判定
		if (collision.gameObject.tag == "BottomWall") {
			FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Over);
//			this.transform.position = new Vector3(0,0,0);
//			this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
//			GameObject.Find ("Message").GetComponent<Text>().text = "Restart!\n(Push \"Space\" Key)";
//			StartCoroutine ("sleep");
		}
	}

	//「コルーチン」で呼び出すメソッド
//	IEnumerator sleep(){
//		GameObject.Find ("Message").GetComponent<Text>().text = "3";
//		yield return new WaitForSeconds(1);
//		GameObject.Find ("Message").GetComponent<Text>().text = "2";
//		yield return new WaitForSeconds(1);
//		GameObject.Find ("Message").GetComponent<Text>().text = "1";
//		yield return new WaitForSeconds(1);
//		GameObject.Find ("Message").GetComponent<Text>().text = "Restart!\n(Push \"Space\" Key)";
//		startFlg = true;
//	}

	public void Init(){
		// 加速値を初期化
		Rigidbody rd = this.GetComponent<Rigidbody> ();
		rd.velocity = Vector3.zero;
		rd.AddForce(
			(transform.forward + transform.right) * speed, 
			ForceMode.VelocityChange);
	}
}
