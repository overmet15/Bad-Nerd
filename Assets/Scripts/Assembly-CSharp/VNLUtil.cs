using System;
using System.Collections;
using UnityEngine;

public class VNLUtil : MonoBehaviour
{
	public delegate IMessageUI DisplayMessageMethod(string[] txts, NoNameMethod onComplete, bool block, bool pause, string placeHolder);

	public delegate IMessageUI DisplayConfirmationMethod(string[] txts, NoNameMethod onComplete, NoNameMethod onCancel);

	public delegate void NoNameMethod();

	public const float pauseSpeed = 1E-05f;

	public const int UNTOUCHABLE = -1;

	public const int NONE = 0;

	public const int MSG = 10;

	public const int INVENTORY_MAIN = 20;

	public const int INVENTORY_ITEM = 30;

	public const int MAP = 40;

	public static int previousPortalId = -1;

	public static int previousReuseableScenePortalId = -1;

	public static string previousLevel;

	public GameObject exclaimationMarkHolderPrefab;

	public Animation gameAnimations;

	private AudioSource audioSource;

	private AudioSource musicSource;

	public AudioClip[] punchSounds = new AudioClip[0];

	public AudioClip[] punchMetalSounds = new AudioClip[0];

	public AudioClip[] kidFightSounds = new AudioClip[0];

	public AudioClip[] nerdFightSounds = new AudioClip[0];

	public AudioClip[] zombieFightSounds = new AudioClip[0];

	public Mesh[] APCMeshes;

	private bool isActionMusic = true;

	private bool isNoMusic = true;

	private bool isFading;

	public AudioClip[] actionMusics;

	public AudioClip[] normalMusics;

	private AudioClip[] theActionMusics;

	private AudioClip[] theNormalMusics;

	public ConfirmationUI confirmationUIPrefab;

	public MoneyItem moneyPrefab;

	public int activeUIID;

	public bool isAmazonVersion;

	public int episode;

	private int musicForEpisode;

	[NonSerialized]
	public int episodeForScene;

	public Mesh[] zombieMeshes;

	private static int count;

	private bool firstTimeStartSceneMusicDone;

	public static bool soundFXOff;

	private float lastPunchSound;

	private float lastMetalSound;

	private bool forcePlayMusic;

	private void Awake()
	{
		Debug.Log("VNLUtil instantiated");
		if (count > 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			Debug.Log("VNLUtil root is duplicate, destroying it");
			return;
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		AudioSource[] components = GetComponents<AudioSource>();
		audioSource = components[0];
		musicSource = components[1];
		changeEpisodeMusic(0);
		Debug.Log("VNLUtil fully initialized");
		count++;
	}

	public void updateEpisodeForSceneBasedOnCurrentScene()
	{
		int result = 0;
		if (int.TryParse(Application.loadedLevelName.Substring(Application.loadedLevelName.Length - 1), out result))
		{
			result--;
		}
		episodeForScene = result;
		initGameStartEpisodeMusicBasedOnLoadedScene();
	}

	public string getIntroScene()
	{
		if (episode == 0)
		{
			return "Intro1";
		}
		return "ZombieIntro";
	}

	public string getEpisodeAgnosticSceneName(string originalSceneName)
	{
		if (originalSceneName == null || originalSceneName.Length == 0)
		{
			return originalSceneName;
		}
		if (episodeForScene > 0)
		{
			originalSceneName = originalSceneName.Replace((episodeForScene + 1).ToString(), string.Empty);
		}
		return originalSceneName;
	}

	public void buyLunchMoney()
	{
		//GoogleIAB.init("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2UtpTW5QEOHn6wtURYq+xPrEDHvD+hBWRrIL37VVyhEdVtQrak3Zz11O75b6+a8uAHQaJTmn9GSFVOTYhFrs4kq3Fa7vHmbLSMry5LzfNfNVLkyafxRT7hOjk/XcuGva1m+07jVBBXovcfOzaj0U+QrKyELqna17ui9QRkaOw3J70RVXe2tkHG56Jd8TziMGcB9Dl2hIR7re8acaLUIwp8QgJTB2tqvzkEeueRmZWm8O4OR9DQ1Nk/F7WnbupEdUd5JL+0gTsKOX/sk236rQIECCynZ9b6iKnOPkuIG90wATyh3cBibJI/uD3PcFmLQ6QyytkJMkWPsaJ4e7lLWLcwIDAQAB");
	}

	private void initGameStartEpisodeMusicBasedOnLoadedScene()
	{
		if (!firstTimeStartSceneMusicDone)
		{
			firstTimeStartSceneMusicDone = true;
			changeEpisodeMusic(episodeForScene);
			playNormalMusicForced();
		}
	}

	public void resetFirstTimeStartSceneMusicDone()
	{
		firstTimeStartSceneMusicDone = false;
	}

	public void changeEpisodeMusic(int episodeMusic)
	{
		musicForEpisode = episodeMusic;
		theNormalMusics = new AudioClip[2];
		int num = musicForEpisode * 2;
		for (int i = 0; i < 2; i++)
		{
			theNormalMusics[i] = normalMusics[num];
			num++;
		}
		theActionMusics = new AudioClip[1];
		int num2 = musicForEpisode;
		for (int j = 0; j < 1; j++)
		{
			theActionMusics[j] = actionMusics[num2];
			num2++;
		}
	}

	public Animation getGameAnimations()
	{
		return gameAnimations;
	}

	public static Vector3 getSize(BadNerdItem thisObject)
	{
		Mesh mesh = thisObject.getMesh();
		Vector3 size = mesh.bounds.size;
		Vector3 localScale = thisObject.transform.localScale;
		return new Vector3(size.x * localScale.x, size.y * localScale.y, size.z * localScale.z);
	}

	public static bool isMovingFast(GameObject go)
	{
		Rigidbody rigidbody = go.GetComponent<Rigidbody>();
		if ((bool)rigidbody)
		{
			if (Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.z) > 1f)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public static void LookAt(Transform a, Transform b)
	{
		a.LookAt(b);
		Quaternion rotation = a.rotation;
		rotation.x = 0f;
		rotation.z = 0f;
		a.rotation = rotation;
	}

	public static void setLayer(Transform transform, string layerName)
	{
		for (int i = 0; i < transform.GetChildCount(); i++)
		{
			setLayer(transform.GetChild(i), layerName);
		}
		transform.gameObject.layer = LayerMask.NameToLayer(layerName);
	}

	public static void toggleAll(Transform transform, bool enabled)
	{
		toggleComponent(transform, enabled);
		toggle(transform, enabled);
	}

	public static void toggle(Transform transform, bool enabled)
	{
		for (int i = 0; i < transform.GetChildCount(); i++)
		{
			toggle(transform.GetChild(i), enabled);
		}
		transform.gameObject.active = enabled;
	}

	public static void toggleComponent(Transform transform, bool enabled)
	{
		for (int i = 0; i < transform.GetChildCount(); i++)
		{
			toggleComponent(transform.GetChild(i), enabled);
		}
		MonoBehaviour[] componentsInChildren = transform.GetComponentsInChildren<MonoBehaviour>(true);
		foreach (MonoBehaviour monoBehaviour in componentsInChildren)
		{
			monoBehaviour.enabled = enabled;
		}
	}

	public static VNLUtil getInstance()
	{
		return getInstance<VNLUtil>();
	}

	public static T getInstance<T>() where T : Component
	{
		string resourceName = typeof(T).Name;
		return getInstance<T>(resourceName);
	}

	public static GameObject getInstance(string resourceName)
	{
		GameObject gameObject = GameObject.Find(resourceName);
		if (gameObject == null)
		{
			gameObject = instantiate(resourceName);
		}
		return gameObject;
	}

	public static T getInstance<T>(string resourceName) where T : Component
	{
		return getInstance(resourceName).GetComponent<T>();
	}

	public static GameObject instantiate(string resourceName)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(loadResource(resourceName));
		gameObject.name = resourceName;
		return gameObject;
	}

	public static T instantiate<T>() where T : Component
	{
		string resourceName = typeof(T).Name;
		return instantiate(resourceName).GetComponent<T>();
	}

	public static T instantiate<T>(string resourceName) where T : Component
	{
		return instantiate(resourceName).GetComponent<T>();
	}

	public IMessageUI displayMessage(string txt, bool block)
	{
		return displayMessage(txt, null, block, true);
	}

	public IMessageUI displayMessage(string txt, NoNameMethod onComplete, bool block, bool pause)
	{
		return displayMessage(new string[1] { txt }, onComplete, block, pause, null);
	}

	public IMessageUI displayMessage(string[] txts, NoNameMethod onComplete, bool block, bool pause, string placeHolder)
	{
		if (getInstance().activeUIID == 10)
		{
			QueuedMessageHolder queuedMessageHolder = new QueuedMessageHolder();
			queuedMessageHolder.setBlock(block);
			queueMessage(displayMessage, queuedMessageHolder, txts, onComplete, block, pause, placeHolder);
			return queuedMessageHolder;
		}
		MessageUI instance = getInstance<MessageUI>();
		instance.setMessage(txts, onComplete, pause, placeHolder);
		instance.setBlock(block);
		return instance;
	}

	private void queueMessage(DisplayMessageMethod method, IMessageUI msgObj, string[] txts, NoNameMethod onComplete, bool block, bool pause, string placeHolder)
	{
		doStartCoRoutine(delegate
		{
			IMessageUI messageUI = method(txts, onComplete, block, pause, placeHolder);
			messageUI.setTitle(msgObj.getQueuedTitle());
			messageUI.setBlock(msgObj.getQueuedBlock());
		}, 0.1f);
	}

	public IMessageUI displayConfirmation(string txt, NoNameMethod onComplete, NoNameMethod onCancel)
	{
		return displayConfirmation(new string[1] { txt }, onComplete, onCancel);
	}

	public IMessageUI displayConfirmation(string[] txts, NoNameMethod onComplete, NoNameMethod onCancel)
	{
		if (getInstance().activeUIID == 10)
		{
			QueuedMessageHolder queuedMessageHolder = new QueuedMessageHolder();
			queueConfirmationMessage(displayConfirmation, queuedMessageHolder, txts, onComplete, onCancel);
			return queuedMessageHolder;
		}
		ConfirmationUI instance = getInstance<ConfirmationUI>();
		instance.setMessage(txts, onComplete, onCancel);
		return instance;
	}

	private void queueConfirmationMessage(DisplayConfirmationMethod method, IMessageUI msgObj, string[] txts, NoNameMethod onComplete, NoNameMethod onCancel)
	{
		doStartCoRoutine(delegate
		{
			IMessageUI messageUI = method(txts, onComplete, onCancel);
			messageUI.setTitle(msgObj.getQueuedTitle());
			messageUI.setBlock(msgObj.getQueuedBlock());
		}, 0.1f);
	}

	public BadNerdItem createRandomItem(AttackComponent dood, int percentChance)
	{
		return createRandomItem(dood, percentChance, 3, 0);
	}

	public BadNerdItem createRandomItem(AttackComponent dood, int percentChance, int chanceOfItemDividedBy, int maxValue)
	{
		BadNerdItem badNerdItem = null;
		if (UnityEngine.Random.Range(0, 100) < percentChance)
		{
			if (UnityEngine.Random.Range(0, chanceOfItemDividedBy) == 0)
			{
				int num = UnityEngine.Random.Range(0, 3);
				string resourceName = null;
				if (num == 0)
				{
					resourceName = "Apple";
				}
				if (num == 1)
				{
					resourceName = "Chocolate Bar";
				}
				if (num == 2)
				{
					resourceName = "Energy Drink";
				}
				badNerdItem = instantiate<BadNerdItem>(resourceName);
				if (maxValue > 0 && badNerdItem.price > maxValue)
				{
					badNerdItem.price = UnityEngine.Random.Range(maxValue / 4, maxValue + 1);
				}
			}
			else
			{
				MoneyItem moneyItem = instantiate<MoneyItem>();
				if (maxValue > 0 && moneyItem.count > maxValue)
				{
					moneyItem.count = UnityEngine.Random.Range(maxValue / 4, maxValue + 1);
				}
				badNerdItem = moneyItem;
			}
			badNerdItem.isForPlayerOnly = true;
			badNerdItem.transform.position = dood.transform.position;
		}
		return badNerdItem;
	}

	public static UnityEngine.Object loadResource(string name)
	{
		UnityEngine.Object @object = Resources.Load("armor/" + name);
		if (@object == null)
		{
			@object = Resources.Load("weapons/" + name);
		}
		if (@object == null)
		{
			@object = Resources.Load("items/" + name);
		}
		if (@object == null)
		{
			@object = Resources.Load("props/" + name);
		}
		if (@object == null)
		{
			@object = Resources.Load("misc/" + name);
		}
		if (@object == null)
		{
			@object = Resources.Load("ui/" + name);
		}
		if (@object == null)
		{
			@object = Resources.Load("characters/" + name);
		}
		if (@object == null)
		{
			@object = Resources.Load("stores/" + name);
		}
		return @object;
	}

	public static void unloadResources()
	{
		Resources.UnloadUnusedAssets();
	}

	public void playAudio(AudioClip clip, bool ignoreTimeScale)
	{
		if (!soundFXOff && !(clip == null) && !(audioSource == null))
		{
			if (ignoreTimeScale || Time.timeScale == 1f)
			{
				base.GetComponent<AudioSource>().pitch = 1f;
			}
			else
			{
				base.GetComponent<AudioSource>().pitch = Time.timeScale * 2f;
			}
			audioSource.PlayOneShot(clip);
		}
	}

	public void playAudioLoop(AudioClip clip, ulong delay)
	{
		if (!(clip == null) && !(audioSource == null))
		{
			isNoMusic = false;
			musicSource.clip = clip;
			musicSource.Play(delay);
		}
	}

	public void playPunchSound()
	{
		if (Time.time > lastPunchSound + 0.1f)
		{
			playAudio(punchSounds[UnityEngine.Random.Range(0, punchSounds.Length)], false);
			lastPunchSound = Time.time;
		}
	}

	public void playMetalPunchSound()
	{
		if (Time.time > lastMetalSound + 0.1f)
		{
			playAudio(punchMetalSounds[UnityEngine.Random.Range(0, punchMetalSounds.Length)], false);
			lastMetalSound = Time.time;
		}
	}

	public void playKidFightSound()
	{
		playAudio(kidFightSounds[UnityEngine.Random.Range(0, kidFightSounds.Length)], false);
	}

	public void playZombieFightSound()
	{
		playAudio(zombieFightSounds[UnityEngine.Random.Range(0, zombieFightSounds.Length)], false);
	}

	public void playNerdFightSound()
	{
		playAudio(nerdFightSounds[UnityEngine.Random.Range(0, nerdFightSounds.Length)], false);
	}

	public void playActionMusic()
	{
		if (isNoMusic || (!isActionMusic && !isFading))
		{
			isActionMusic = true;
			StartCoroutine(fadeInMusic(theActionMusics[UnityEngine.Random.Range(0, theActionMusics.Length)], 0.1f, 0.1f));
		}
	}

	public void playNormalMusic()
	{
		playNormalMusic(UnityEngine.Random.Range(0, theNormalMusics.Length));
	}

	public void playNormalMusicForced()
	{
		forcePlayMusic = true;
		playNormalMusic();
		forcePlayMusic = false;
	}

	public void playNormalMusicForced(int index)
	{
		forcePlayMusic = true;
		playNormalMusic(index);
		forcePlayMusic = false;
	}

	public void playNormalMusic(int index)
	{
		AudioClip audioClip = theNormalMusics[index];
		if (!(musicSource.clip == audioClip) && (forcePlayMusic || isNoMusic || (isActionMusic && !isFading)))
		{
			isActionMusic = false;
			StartCoroutine(fadeInMusic(audioClip, 0.075f, 0.03f));
		}
	}

	private IEnumerator fadeInMusic(AudioClip clip, float inRate, float outRate)
	{
		isFading = true;
		if (!isNoMusic)
		{
			for (float v2 = 1f; v2 > 0f; v2 -= outRate)
			{
				musicSource.volume = v2;
				yield return new WaitForSeconds(0.1f);
			}
		}
		playAudioLoop(clip, 0uL);
		for (float v = 0f; v < 1f; v += inRate)
		{
			musicSource.volume = v;
			yield return new WaitForSeconds(0.1f);
		}
		isFading = false;
	}

	public void pause()
	{
		if (!NetworkCore.isNetworkMode)
		{
			Time.timeScale = 1E-05f;
		}
		toggleController(false);
		GameObject gameObject = GameObject.Find("Player");
		if (gameObject != null)
		{
			PlayerAttackComponent component = gameObject.GetComponent<PlayerAttackComponent>();
			component.isRun = false;
		}
	}

	public void resume()
	{
		Time.timeScale = 1f;
		toggleController(true);
	}

	public static void toggleController(bool on)
	{
		GameObject gameObject = GameObject.Find("Player");
		if ((bool)gameObject)
		{
			gameObject.GetComponent<MyCameraRelativeControl>().toggle(on);
		}
	}

	public void doStartCoRoutine(NoNameMethod onClick, float delay)
	{
		StartCoroutine(theCoRoutine(onClick, delay));
	}

	public void doStartCoRoutine(NoNameMethod onClick)
	{
		StartCoroutine(theCoRoutine(onClick, 0f));
	}

	private IEnumerator theCoRoutine(NoNameMethod onClick, float delay)
	{
		if (delay == 0f)
		{
			yield return null;
		}
		else
		{
			yield return new WaitForSeconds(delay);
		}
		onClick();
	}

	public GameObject getExclaimationMark(Transform parent)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(exclaimationMarkHolderPrefab);
		Vector3 position = parent.position;
		position.y += 35f;
		gameObject.transform.position = position;
		gameObject.transform.parent = parent;
		return gameObject;
	}

	public static int compareByOder(BadNerdItem a, BadNerdItem b)
	{
		return (a.storeOrder + a.name).CompareTo(b.storeOrder + b.name);
	}

	public static int compareByPrice(BadNerdItem a, BadNerdItem b)
	{
		return a.price.CompareTo(b.price);
	}
}
