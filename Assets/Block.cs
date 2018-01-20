using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){
		//衝突判定
		if (collision.gameObject.tag == "Ball") {
			//相手のタグがBallならば、自分を消す
			this.gameObject.SetActive(false);
//			Debug.Log (GameObject.FindGameObjectsWithTag("Block").Length);
//			Destroy(this.gameObject);
//			if(GameObject.FindGameObjectsWithTag("Block").Length == 1){
//				GameObject.FindWithTag ("Ball").transform.position = new Vector3 (0, 0, 0);
//				GameObject.FindWithTag ("Ball").GetComponent<Rigidbody> ().velocity = Vector3.zero;
//				GameObject.Find ("Message").GetComponent<Text>().text = "Clear!!";
//			}
		}
	}
}
