using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public enum GameState{
		Opening,
		Playing,
		Clear,
		Over
	}

	// 現在のゲーム進行状態
	public static GameState currentState = GameState.Opening;
	// パネルオブジェクト
	private GameObject panel;
	// メッセージオブジェクト
	private GameObject message;
	// タイトルオブジェクト
	private GameObject title;
	// タイマーオブジェクト
	private GameObject timer;
	// ラケットオブジェクト
	private GameObject racket;
	// ボールオブジェクト
	private GameObject ball;
	// ブロックオブジェクト
	private GameObject[] blocks;
	// タイトルテキスト
	private Text titleText;
	// タイマーテキスト
	private Text timerText;
	// ステージ
	private int stage = 1;
	// タイマー
	private float timerCount;

	private Vector3[] blocksPosition = new Vector3[25];
	private Quaternion[] blocksRotation = new Quaternion[25];

	// Use this for initialization
	void Start () {
		
		//ゲームオブジェクトを検索し取得する
		panel = GameObject.Find("Panel");
		message = GameObject.Find("Message");       
		title = GameObject.Find("Title");
		timer = GameObject.Find("Timer");
		racket = GameObject.Find("Racket");
		ball = GameObject.Find("Ball");
		blocks = GameObject.FindGameObjectsWithTag ("Block");
		for (int i = 0; i < blocks.Length; i++) {
			blocksPosition [i] = blocks [i].transform.position;
			blocksRotation [i] = blocks [i].transform.rotation;
		}
		// タイトルテキスト
		titleText = title.GetComponent<Text> ();
		// タイマーテキスト
		timerText = timer.GetComponent<Text> ();

		// オープニング
		GameOpening ();
	}

	// Update is called once per frame
	void Update () {
		// ゲーム中ではなく、Spaceキーが押されたらtrueを返す。
		if(currentState == GameState.Opening && Input.GetKeyDown (KeyCode.Space)) {
			dispatch (GameState.Playing);                   
		}
		if (currentState == GameState.Playing) {
			timerCount -= Time.deltaTime; //スタートしてからの秒数を格納
			timerText.text = timerCount.ToString("F0"); //小数2桁にして表示
			if(timerCount < 0){
				dispatch (GameState.Over);
			}
			// ゲームクリアーの判定
			if (GameObject.FindGameObjectsWithTag ("Block").Length == 0) {
				dispatch (GameState.Clear);                 
			}
		}
	}

	// 状態による振り分け処理
	public void dispatch (GameState state){
		GameState oldState = currentState;

		currentState = state;
		switch (state) {
		case GameState.Opening:
			GameOpening ();
			break;
		case GameState.Playing:
			GameStart ();
			break;
		case GameState.Clear :
			GameClear ();
			break;
		case GameState.Over:
			if (oldState == GameState.Playing) {
				GameOver ();
			}
			break;
		}

	}

	// オープニング処理
	void GameOpening ()
	{
		currentState = GameState.Opening;

		// ボールの動作停止
		Time.timeScale = 0;

		// タイトル名のセット
		SetTitle ("Breakout!!", Color.green);

		//パネル・メッセージの表示
		this.timer.SetActive (false);
		this.panel.SetActive (true);
		this.message.SetActive (true);

		//タイマーリセット
		timerCount = 60;
		timerText.text = timerCount.ToString("F0"); //小数2桁にして表示

		//ブロックの復活
//		foreach(GameObject block in blocks){
//			block.SetActive (true);
//		}
		for (int i = 0; i < blocks.Length; i++) {
			blocks [i].SetActive (true);
			blocks [i].transform.position = blocksPosition [i];
			blocks [i].transform.rotation = blocksRotation [i];
			blocks [i].GetComponent<Rigidbody> ().velocity = Vector3.zero;
			blocks [i].GetComponent<Rigidbody> ().angularVelocity= Vector3.zero;
			blocks [i].GetComponent<Rigidbody> ().ResetInertiaTensor();
		}

		// ラケットの初期位置をセット
		racket.transform.position = new Vector3 (2.0f, 0.0f, -7.0f);
		// ボールの初期位置をセット
		ball.transform.position = new Vector3 (2.0f, 0.0f, -5.9f);

	}

	// ゲームスタート処理
	void GameStart ()
	{
		// パネル非活性化
		this.timer.SetActive (true);
		this.panel.SetActive (false);
		this.message.SetActive (false);
		this.title.SetActive (false);

		// ボールの動作開始
		Time.timeScale = 1.0f;

		// ボールの初期化
		FindObjectOfType<Ball> ().Init ();
	}

	// ゲームクリアー処理
	void GameClear ()
	{

		stage++;

		// タイトル名のセット
		SetTitle ("Game Clear", Color.yellow);

		// 3秒後にオープニング処理を呼び出す
		Invoke("GameOpening", 3f);
	}

	// ゲームオーバー処理
	void GameOver ()
	{
		// タイトル名のセット
		SetTitle ("Game Over", Color.red);
		// ステージ初期値
		stage = 1;

		// 3秒後にオープニング処理を呼び出す
		Invoke("GameOpening", 3f);
	}

	// オープニング処理
	void SetTitle (string message, Color color)
	{
		// タイトル名のセット
		titleText.text = message;
		titleText.color = color;
		// パネル活性化
		this.title.SetActive (true);
	}
}
