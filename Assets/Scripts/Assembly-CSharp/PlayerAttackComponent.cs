using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttackComponent : AttackComponent
{
    public const string CURRENT_GAME_LEVEL = "currentLevel";
    public const string LEVELS_COMPLETED = "levelsCompleted";
    private const string SAVED_ITEM_LIST = "savedItemList";
    private const string SAVED_EQUIPPED_LIST = "savedEquippedList";

    private Camera playerCamera;

    [NonSerialized]
    public string currentLevel = string.Empty;

    [NonSerialized]
    public VNLUtil.NoNameMethod tutorialInventoryTrigger;

    private Quest currentQuest;
    private string nextQuestGiverLocations = string.Empty;
    private UISlider healthBar;
    private UISlider energyBar;
    private UILabel enemyName;
    private UISlider enemyHealthBar;
    private UISlider enemyEnergyBar;
    private UISprite lowEnergy;
    private UILabel lunchMoneyLabel;
    private List<string> questsCompleted = new List<string>();

    public static bool allowMoving = true;

    [NonSerialized]
    public VNLUtil.NoNameMethod onQuestStatusChange;

    private VNLUtil.NoNameMethod notifyPlayerThatEnemeIsDead;
    private NetworkCore networkCore;
    private Animation mapArrow;
    private int attackTut;
    private int blockTut;
    private int runTut;
    private bool alreadyStartedTiredCancellationRoutine;
    private int updateLoop;

    public static bool stopAutoZoom;
    private float distanceToStopAutoRotatingCamera = 20f;
    private UIPanel mapObjects;
    private int tapCount;

    public VNLUtil.NoNameMethod onGoToScene;

    public Shader fisheyeShader;
    public Shader twirlShader;
    public Shader overlayShader;
    public Texture2D zombieOverlayTexture;

    private Fisheye fishEye;
    private TwirlEffect twirlEffect;
    private ScreenOverlay screenOverlay;

    public float twirlSpeed = 0.3f;
    public float fisheyeSpeed = 0.02f;
    public float fisheyeStrength = 0.4f;
    public float twirlAngle = 10f;
    public float overlaySpeed = 0.25f;
    public float overlayMin = 2f;
    public float overlayMax = 4f;

    public Quest CurrentQuest
    {
        get
        {
            return currentQuest;
        }
    }

    protected override void Awake()
    {
        runTut = PlayerPrefs.GetInt("runTut", 0);
        blockTut = PlayerPrefs.GetInt("blockTut", 0);
        attackTut = PlayerPrefs.GetInt("attackTut", 0);
        mapArrow = base.transform.Find("MapArrow").GetComponent<Animation>();
        VNLUtil.toggleAll(mapArrow.transform, false);
        networkCore = GetComponent<NetworkCore>();
        notifyPlayerThatEnemeIsDead = delegate
        {
            setCurrentEnemy(null);
        };
        base.Awake();
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        Debug.Log("playerCamera: " + playerCamera);
        healthBar = GameObject.Find("HealthBar").GetComponent<UISlider>();
        energyBar = GameObject.Find("EnergyBar").GetComponent<UISlider>();
        enemyName = GameObject.Find("EnemyName").GetComponent<UILabel>();
        enemyHealthBar = GameObject.Find("EnemyHealthBar").GetComponent<UISlider>();
        enemyEnergyBar = GameObject.Find("EnemyEnergyBar").GetComponent<UISlider>();
        lowEnergy = GameObject.Find("lowEnergy").GetComponent<UISprite>();
        lowEnergy.enabled = false;
        lunchMoneyLabel = GameObject.Find("LunchMoneyLabel").GetComponent<UILabel>();
        if (GameStart.resumeGame)
        {
            load();
        }
    }

    public void toggleMapArrow(bool on)
    {
        VNLUtil.toggleAll(mapArrow.transform, on);
        mapArrow.enabled = on;
        mapArrow["mapArrow"].speed = 1f / Time.timeScale;
        mapArrow.Play();
    }

    public string getQuestMarker()
    {
        if (CurrentQuest != null)
        {
            return CurrentQuest.levelToGoTo;
        }
        if (nextQuestGiverLocations != null)
        {
            return nextQuestGiverLocations;
        }
        return null;
    }

    public void enableNetwork()
    {
        networkCore.enabled = true;
    }

    public void disableNetwork()
    {
        if (NetworkCore.isNetworkMode)
        {
            networkCore.disconnect();
        }
    }

    private void Start()
    {
        toggleEnemyStatus(false);
        updateHealthBar();
        updateEnergyBar();
        updateLunchMoneyLabel();
    }

    public void setPosition(float x, float y, float z)
    {
        base.transform.position = new Vector3(x, (!(y < 2.5f)) ? y : 2.5f, z);
    }

    public void resetPosition(bool cameraFaceForward)
    {
        setPosition(0f, 0f, 0f);
        resetCameraRotation();
        faceForward(cameraFaceForward);
    }

    public void rotate(float angle)
    {
        resetRotation();
        myTransform.Rotate(Vector3.up, angle);
    }

    public void resetRotation()
    {
        Quaternion rotation = myTransform.rotation;
        rotation.y = 0f;
        myTransform.rotation = rotation;
    }

    public void rotateCamera(float angle)
    {
        resetCameraRotation();
        Transform transform = GameObject.Find("CameraPivot").transform;
        transform.Rotate(Vector3.up, angle);
    }

    public void resetCameraRotation()
    {
        Transform transform = GameObject.Find("CameraPivot").transform;
        transform.LookAt(transform.position + myTransform.forward);
    }

    public void faceForward(bool faceForward)
    {
        GameObject.Find("CameraPivot").GetComponent<MyFollowTransform>().faceForward = faceForward;
    }

    private void updateLunchMoneyLabel()
    {
        lunchMoneyLabel.text = "$" + lunchMoney;
    }

    public override void addLunchMoney(int newValue)
    {
        base.addLunchMoney(newValue);
        updateLunchMoneyLabel();
    }

    private void updateHealthBar()
    {
        healthBar.sliderValue = life / maxLife;
    }

    private void updateEnergyBar()
    {
        energyBar.sliderValue = energy / maxEnergy;
    }

    public override void setCurrentEnemy(AttackComponent newEnemy)
    {
        if (base.CurrentEnemy == newEnemy)
        {
            return;
        }
        if (base.CurrentEnemy != null)
        {
            AttackComponent attackComponent = base.CurrentEnemy;
            attackComponent.onDead = (VNLUtil.NoNameMethod)Delegate.Remove(attackComponent.onDead, notifyPlayerThatEnemeIsDead);
            base.CurrentEnemy.onEnergyChanged = null;
            base.CurrentEnemy.onHealthChanged = null;
        }
        if (newEnemy == null || newEnemy.isDead)
        {
            base.setCurrentEnemy(null);
            toggleEnemyStatus(false);
            return;
        }
        base.setCurrentEnemy(newEnemy);
        enemyName.text = newEnemy.nickName;
        toggleEnemyStatus(true);
        newEnemy.onDead = (VNLUtil.NoNameMethod)Delegate.Combine(newEnemy.onDead, notifyPlayerThatEnemeIsDead);
        newEnemy.onHealthChanged = (VNLUtil.NoNameMethod)Delegate.Combine(newEnemy.onHealthChanged, (VNLUtil.NoNameMethod)delegate
        {
            updateEnemyHealthBar();
        });
        newEnemy.onEnergyChanged = (VNLUtil.NoNameMethod)Delegate.Combine(newEnemy.onEnergyChanged, (VNLUtil.NoNameMethod)delegate
        {
            updateEnemyEnergyBar();
        });
        updateEnemyHealthBar();
        updateEnemyEnergyBar();
    }

    private void updateEnemyHealthBar()
    {
        enemyHealthBar.sliderValue = base.CurrentEnemy.life / base.CurrentEnemy.maxLife;
    }

    private void updateEnemyEnergyBar()
    {
        enemyEnergyBar.sliderValue = base.CurrentEnemy.energy / base.CurrentEnemy.maxEnergy;
    }

    private void toggleEnemyStatus(bool enabled)
    {
        VNLUtil.toggle(enemyName.transform, enabled);
        VNLUtil.toggle(enemyEnergyBar.transform, enabled);
        VNLUtil.toggle(enemyHealthBar.transform, enabled);
    }

    public override void setLife(float newLife)
    {
        base.setLife(newLife);
        updateHealthBar();
        if (NetworkCore.isLoggedIn)
        {
            networkCore.announceStatus(this);
        }
    }

    public override void setEnergy(float increment)
    {
        base.setEnergy(increment);
        updateEnergyBar();
        if (energy > 14f)
        {
            lowEnergy.enabled = false;
        }
        else
        {
            lowEnergy.enabled = true;
        }
    }

    protected override void indicateLowEnergy()
    {
        lastHurt = AttackComponent.ANIM_HURT + "99";
        anim.CrossFade(lastHurt);
        toggleAttackColliders(false);
        if (!alreadyStartedTiredCancellationRoutine)
        {
            alreadyStartedTiredCancellationRoutine = true;
            VNLUtil.getInstance().doStartCoRoutine(delegate
            {
                alreadyStartedTiredCancellationRoutine = false;
                onWalkPressed(Vector3.zero);
            }, 2f);
        }
    }

    public override void onWalkPressed(Vector3 speed)
    {
        base.onWalkPressed(speed);
        Vector3 vector = myTransform.position + Vector3.Normalize(speed);
        if (NetworkCore.isLoggedIn)
        {
            networkCore.announcePositionQueued(vector.x, vector.y, vector.z, isRun);
        }
    }

    // Added left mouse click binding for attack:
    protected override void Update()
    {
        /*
          if (GameStart.isZeemoteConnected)
          {
              if (ZeemoteInput.GetButtonDown(1, 0))
              {
                  onAttackButtonPressed();
              }
              if (ZeemoteInput.GetButtonDown(1, 1))
              {
                  onBlockPressed();
              }
              if (ZeemoteInput.GetButtonUp(1, 1))
              {
                  onBlockReleased();
              }
          }
          */

        // Bind left mouse click to attack
        if (Input.GetMouseButtonDown(0))
        {
            onAttackButtonPressed();
        }

        // bind right click to block
        if (Input.GetMouseButtonDown(1))
        {
            onBlockPressed();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            APIService.exitGame("Exit", "Are you sure?");
        }
        if (isDead)
        {
            return;
        }
        base.Update();
        updateLoop++;
        if (updateLoop % 50 == 0)
        {
            GameObject[] array = GameObject.FindGameObjectsWithTag("enemy");
            bool flag = false;
            bool flag2 = false;
            GameObject[] array2 = array;
            foreach (GameObject gameObject in array2)
            {
                EnemyAttackComponent component = gameObject.GetComponent<EnemyAttackComponent>();
                if (!component.isEnemyWithPlayer || component.isDead)
                {
                    continue;
                }
                float num = Vector3.Distance(myTransform.position, component.transform.position);
                if (num > component.seekDistance)
                {
                    component.stopSeeking();
                    continue;
                }
                flag = true;
                if (num <= distanceToStopAutoRotatingCamera)
                {
                    flag2 = true;
                }
            }
            if (isZombiefied || flag2)
            {
                VNLUtil.getInstance().playActionMusic();
            }
            else
            {
                VNLUtil.getInstance().playNormalMusic();
            }
            stopAutoZoom = flag2;
        }
        if (touchedCountroller || Input.touchCount <= 0 || anim.IsPlaying(lastWalkAnim) || isDead || Camera.main == null || VNLUtil.getInstance().activeUIID != 0)
        {
            return;
        }
        Vector3 touchPoint = getTouchPoint();
        if (!(touchPoint != Vector3.zero))
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(touchPoint);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo) && (bool)hitInfo.transform.gameObject.GetComponent<AttackComponent>())
        {
            Transform transform = hitInfo.transform;
            onThrow(transform.position);
            if (NetworkCore.isLoggedIn)
            {
                networkCore.announceThrow(transform.position);
            }
        }
    }

    private Vector3 getTouchPoint()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended)
        {
            Vector3 result = new Vector3(touch.position.x, touch.position.y, 0f);
            return result;
        }
        return Vector3.zero;
    }

    protected override AttackComponent getEnemyToLookAtBeforeAttacking()
    {
        float num = 9999f;
        GameObject gameObject = null;
        GameObject[] array = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject gameObject2 in array)
        {
            if (!gameObject2.GetComponent<AttackComponent>().isDead)
            {
                float num2 = Vector3.Distance(myTransform.position, gameObject2.transform.position);
                if (num2 < num)
                {
                    num = num2;
                    gameObject = gameObject2;
                }
            }
        }
        if (gameObject == null)
        {
            return null;
        }
        return gameObject.GetComponent<AttackComponent>();
    }

    public void inventory()
    {
        if (VNLUtil.getInstance().activeUIID == 0 && GameObject.Find("InventoryUI") == null)
        {
            InventoryUI inventoryUI = VNLUtil.instantiate<InventoryUI>();
            inventoryUI.tutorialInventoryTrigger = tutorialInventoryTrigger;
        }
    }

    public void showMap()
    {
        if (VNLUtil.getInstance().activeUIID == 0 && !(VNLUtil.getInstance<QuestInitializer>().map == null) && GameObject.Find("mapObjects") == null)
        {
            mapObjects = VNLUtil.instantiate<UIPanel>("mapObjects");
        }
    }

    public virtual void onAttackButtonPressed()
    {
        if (VNLUtil.getInstance().activeUIID != 0 || isHurting())
        {
            return;
        }
        tapCount++;
        onAttackPressed(tapCount);
        if (NetworkCore.isLoggedIn)
        {
            networkCore.announceStatus(this);
            networkCore.announceAttack(tapCount);
        }
        if (attackTut == 0)
        {
            VNLUtil.getInstance().displayMessage(new string[4] { "tut_4", "tut_5", "tut_6", "tut_7" }, delegate
            {
                attackTut = 1;
                PlayerPrefs.SetInt("attackTut", 1);
            }, false, true, null);
        }
        VNLUtil.getInstance().doStartCoRoutine(delegate
        {
            tapCount = 0;
        }, 0.5f);
    }

    public override void onBlockPressed()
    {
        if (VNLUtil.getInstance().activeUIID != 0)
        {
            return;
        }
        if (anim.IsPlaying(lastWalkAnim))
        {
            isRun = true;
        }
        else
        {
            base.onBlockPressed();
            if (NetworkCore.isLoggedIn)
            {
                networkCore.announceBlockStart();
            }
        }
        if (blockTut == 0)
        {
            VNLUtil.getInstance().displayMessage(new string[2] { "blockTut1", "blockTut2" }, delegate
            {
                blockTut = 1;
                PlayerPrefs.SetInt("blockTut", 1);
            }, false, true, null);
        }
        if (runTut == 0)
        {
            VNLUtil.getInstance().displayMessage(new string[1] { "runTut" }, delegate
            {
                runTut = 1;
                PlayerPrefs.SetInt("runTut", 1);
            }, false, true, null);
        }
    }

    public override void onBlockReleased()
    {
        if (VNLUtil.getInstance().activeUIID == 0)
        {
            isRun = false;
            base.onBlockReleased();
            if (NetworkCore.isLoggedIn)
            {
                networkCore.announceBlockEnd();
            }
        }
    }

    public override void doCounterAttack()
    {
        base.doCounterAttack();
        if (NetworkCore.isLoggedIn)
        {
            networkCore.announceCounterAttack();
        }
    }

    public void save(string levelToSaveAt)
    {
        ItemSerializer.IS_GAME_STATE_MODE = true;
        if (GameStart.enableNetworking)
        {
            levelToSaveAt = null;
        }
        if (levelToSaveAt != null)
        {
            PlayerPrefs.SetString("currentLevel", levelToSaveAt);
        }
        PlayerPrefs.SetInt("hasSavedGame", 1);
        PlayerPrefs.SetString("nextQuestGiverLocations", nextQuestGiverLocations);
        PlayerPrefs.SetInt("lunchMoney", lunchMoney);

        // Save health and energy values:
        PlayerPrefs.SetFloat("life", life);
        PlayerPrefs.SetFloat("maxLife", maxLife);
        PlayerPrefs.SetFloat("energy", energy);      // Add this line if 'energy' is your current energy
        PlayerPrefs.SetFloat("maxEnergy", maxEnergy);
        PlayerPrefs.SetFloat("recoveryRate", recoveryRate);
        PlayerPrefs.SetFloat("strength", strength);
        PlayerPrefs.SetFloat("capacity", capacity);
        PlayerPrefs.SetFloat("knockDownProbabiliy", knockDownProbabiliy);
        PlayerPrefs.SetFloat("attack1Level", attack1Level);
        PlayerPrefs.SetFloat("attack2Level", attack2Level);
        PlayerPrefs.SetFloat("attack3Level", attack3Level);
        PlayerPrefs.SetString("savedItemList", "");
        // (Additional saving logic omitted for brevity)
        Debug.Log("LEVELS_COMPLETED: " + string.Join(",", questsCompleted.ToArray()));
    }


    public string getEquippedItemsAsString()
    {
        string text = string.Empty;
        for (int i = 0; i < equippedItems.Count; i++)
        {
            BadNerdItem badNerdItem = equippedItems[i];
            text = text + badNerdItem.name + ",";
        }
        Debug.Log("getEquippedItemsAsString(): " + text);
        return text;
    }

    public string getEquippedItemsPropertiesAsString()
    {
        ItemSerializer.IS_GAME_STATE_MODE = false;
        ItemSerializer.clearMap();
        Debug.Log("getEquippedItemsPropertiesAsStrings() serializing...");
        for (int i = 0; i < equippedItems.Count; i++)
        {
            BadNerdItem badNerdItem = equippedItems[i];
            badNerdItem.saveInItemList(i);
        }
        return ItemSerializer.getMapAsString();
    }

    public void announceEquipments()
    {
        if (NetworkCore.isLoggedIn)
        {
            string equippedItemsAsString = getEquippedItemsAsString();
            string equippedItemsPropertiesAsString = getEquippedItemsPropertiesAsString();
            networkCore.announceEquipments(equippedItemsAsString, equippedItemsPropertiesAsString);
        }
    }

    public void load()
    {
        if (PlayerPrefs.GetInt("hasSavedGame") == 0)
        {
            return;
        }
        ItemSerializer.IS_GAME_STATE_MODE = true;
        VNLUtil.soundFXOff = true;
        nextQuestGiverLocations = PlayerPrefs.GetString("nextQuestGiverLocations");
        lunchMoney = PlayerPrefs.GetInt("lunchMoney");
        life = PlayerPrefs.GetFloat("life");
        maxLife = PlayerPrefs.GetFloat("maxLife");
        maxEnergy = PlayerPrefs.GetFloat("maxEnergy");
        capacity = PlayerPrefs.GetFloat("capacity");
        knockDownProbabiliy = PlayerPrefs.GetInt("knockDownProbabiliy");
        strength = PlayerPrefs.GetFloat("strength");
        recoveryRate = PlayerPrefs.GetFloat("recoveryRate");
        attack1Level = PlayerPrefs.GetInt("attack1Level");
        attack2Level = PlayerPrefs.GetInt("attack2Level");
        attack3Level = PlayerPrefs.GetInt("attack3Level");
        weaponAttack1Level = PlayerPrefs.GetInt("weaponAttack1Level", 1);
        weaponAttack2Level = PlayerPrefs.GetInt("weaponAttack2Level", 1);
        weaponAttack3Level = PlayerPrefs.GetInt("weaponAttack3Level", 1);
        counterAttackLevel = PlayerPrefs.GetInt("counterAttackLevel", 0);
        updateHealthBar();
        updateLunchMoneyLabel();
        // (Loading items logic omitted for brevity)
        questsCompleted = PlayerPrefs.GetString("levelsCompleted").Split(',').ToList();
        GameStart.resumeGame = false;
        VNLUtil.soundFXOff = false;
    }

    protected override float getMaxSpeed()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<MyCameraRelativeControl>().speed;
    }

    protected override void onHurt(AttackComponent enemy, Weapon weapon)
    {
        base.onHurt(enemy, weapon);
        updateHealthBar();
    }

    protected override void animateHurt()
    {
        base.animateHurt();
        if (NetworkCore.isLoggedIn)
        {
            networkCore.announceHurtAnum(lastHurt);
        }
    }

    public void setCurrentQuest(Quest q)
    {
        currentQuest = q;
        currentQuest.Player = this;
        save(Application.loadedLevelName);
        bool flag = q.autoJumpToLocation;
        if (flag && Application.loadedLevelName.Equals(q.levelToGoTo))
        {
            flag = false;
        }
        if (flag)
        {
            gotoLevel(q.levelToGoTo);
        }
        if (!flag)
        {
            q.init();
            if (onQuestStatusChange != null)
            {
                onQuestStatusChange();
            }
        }
    }

    public bool gotoLevel(string level)
    {
        if (Application.loadedLevelName.Equals(level))
        {
            return false;
        }
        base.GetComponent<Collider>().enabled = false;
        if (VNLUtil.getInstance().activeUIID != 0)
        {
            VNLUtil.getInstance().doStartCoRoutine(delegate
            {
                gotoLevel(level);
            }, 0.3f);
            return true;
        }
        VNLUtil.getInstance().doStartCoRoutine(delegate
        {
            togglePlayerCamera(false);
            Vector3 position = myTransform.position;
            position.y = 100f;
            carryItemsAcrossLevel();
            myTransform.position = position;
            setCurrentEnemy(null);
            if (onGoToScene != null)
            {
                onGoToScene();
            }
            VNLUtil.getInstance<LevelLoader>().loadLevel(level);
        }, 1f);
        return true;
    }

    public void togglePlayerCamera(bool on)
    {
        playerCamera.enabled = on;
        if (on)
        {
            GameObject.Find("HudCamera").GetComponent<Camera>().enabled = true;
        }
    }

    public void carryItemsAcrossLevel()
    {
        foreach (BadNerdItem item in itemList)
        {
            item.carryItemsAcrossLevel(base.transform);
        }
    }

    public void unCarryItemsAcrossLevel()
    {
        foreach (BadNerdItem item in itemList)
        {
            item.unCarryItemsAcrossLevel(base.transform);
        }
    }

    public bool isQuestComplete(Quest quest)
    {
        if (quest.considerCompleteIfDifferentEpisode && VNLUtil.getInstance().episode > quest.episode)
        {
            return true;
        }
        return questsCompleted.Contains(quest.name);
    }

    public int getCompletedQuestCount()
    {
        return questsCompleted.Count();
    }

    protected override void dieDetachItems()
    {
        setCurrentEnemy(null);
        APIService.logFlurryEvent("Died");
        //  APIService.showFullScreenAd();
        if (NetworkCore.isLoggedIn)
        {
            networkCore.announceDeath();
        }
    }

    protected override void dieDoDestroy()
    {
        disableNetwork();
        gotoLevel(getEpisodeRelaventScene("Infirmary"));
    }

    private string getEpisodeRelaventScene(string theSceneToGoTo)
    {
        string text = string.Empty;
        if (VNLUtil.getInstance().episodeForScene > 0)
        {
            text = (VNLUtil.getInstance().episodeForScene + 1).ToString();
        }
        return theSceneToGoTo + text;
    }

    public void completeQuest()
    {
        if (currentQuest == null)
        {
            return;
        }
        Debug.Log("Quests Completed: " + currentQuest.getRootQuest().name);
        if (!currentQuest.getRootQuest().isDoNotPersistCompletion)
        {
            questsCompleted.Add(currentQuest.getRootQuest().name);
        }
        string levelToGoToWhenDone = currentQuest.getRootQuest().levelToGoToWhenDone;
        if (currentQuest.getRootQuest().nextQuest != null)
        {
            string text = nextQuestGiverLocations;
            nextQuestGiverLocations = text + currentQuest.getRootQuest().nextQuest.name + "," + currentQuest.getRootQuest().nextQuestGiverLocation + "|";
        }
        string[] array = nextQuestGiverLocations.Split('|');
        nextQuestGiverLocations = string.Empty;
        string[] array2 = array;
        foreach (string text2 in array2)
        {
            if (!text2.StartsWith(currentQuest.getRootQuest().name + ",") && !string.Empty.Equals(text2))
            {
                nextQuestGiverLocations = nextQuestGiverLocations + text2 + "|";
            }
        }
        Debug.Log("nextQuestGiverLocations= " + nextQuestGiverLocations);
        Quest quest = currentQuest;
        currentQuest = null;
        save(levelToGoToWhenDone);
        bool flag = quest.getRootQuest().autoJumpToLocation;
        if (flag)
        {
            flag = gotoLevel(levelToGoToWhenDone);
        }
        if (!flag)
        {
            Debug.Log("Not jumping to scene");
            if (onQuestStatusChange != null)
            {
                Debug.Log("calling onQuestStatusChange");
                onQuestStatusChange();
            }
            VNLUtil.getInstance<QuestInitializer>().reEnableQuestGiversIncaseTheyAreNext(quest);
        }
    }

    public override void becomeZombie(float seconds)
    {
        base.becomeZombie(seconds);
        if (NetworkCore.isLoggedIn)
        {
            networkCore.announceZombieMode(seconds);
        }
    }

    protected override void startZombieEffect()
    {
        base.startZombieEffect();
        fishEye = playerCamera.gameObject.AddComponent(typeof(Fisheye)) as Fisheye;
        fishEye.fishEyeShader = fisheyeShader;
        twirlEffect = playerCamera.gameObject.AddComponent(typeof(TwirlEffect)) as TwirlEffect;
        twirlEffect.shader = twirlShader;
        screenOverlay = playerCamera.gameObject.AddComponent(typeof(ScreenOverlay)) as ScreenOverlay;
        screenOverlay.texture = zombieOverlayTexture;
        screenOverlay.overlayShader = overlayShader;
        VNLUtil.getInstance().playActionMusic();
        StartCoroutine(zombieEffect());
    }

    protected override void endZombieEffect()
    {
        base.endZombieEffect();
        UnityEngine.Object.Destroy(fishEye);
        UnityEngine.Object.Destroy(twirlEffect);
        UnityEngine.Object.Destroy(screenOverlay);
    }

    private IEnumerator zombieEffect()
    {
        fishEye.strengthY = -0.09f;
        fishEye.strengthX = 0f;
        bool fishEyeGrowing = false;
        twirlEffect.radius.x = 0.6f;
        twirlEffect.radius.y = 0.6f;
        twirlEffect.angle = 0f;
        bool twirlGrowing = false;
        screenOverlay.intensity = 0f;
        screenOverlay.blendMode = ScreenOverlay.OverlayBlendMode.Multiply;
        bool overlayGrowing = false;
        while (isZombiefied)
        {
            yield return new WaitForSeconds(0.05f);
            if (fishEye.strengthX <= 0f - fisheyeStrength)
            {
                fishEyeGrowing = true;
            }
            if (fishEye.strengthX >= fisheyeStrength)
            {
                fishEyeGrowing = false;
            }
            if (fishEyeGrowing)
            {
                fishEye.strengthX += fisheyeSpeed;
            }
            else
            {
                fishEye.strengthX -= fisheyeSpeed;
            }
            if (twirlEffect.angle <= 0f - twirlAngle)
            {
                twirlGrowing = true;
            }
            if (twirlEffect.angle >= twirlAngle)
            {
                twirlGrowing = false;
            }
            if (twirlGrowing)
            {
                twirlEffect.angle += twirlSpeed;
            }
            else
            {
                twirlEffect.angle -= twirlSpeed;
            }
            if (screenOverlay.intensity <= overlayMin)
            {
                overlayGrowing = true;
            }
            if (screenOverlay.intensity >= overlayMax)
            {
                overlayGrowing = false;
            }
            if (overlayGrowing)
            {
                screenOverlay.intensity += overlaySpeed;
            }
            else
            {
                screenOverlay.intensity -= overlaySpeed;
            }
        }
    }

    public void onGotTapPoints(string points)
    {
        if (int.Parse(points) > 0)
        {
            addLunchMoney(int.Parse(points));
            save(Application.loadedLevelName);
        }
    }

    public void onVideoAdReward()
    {
        onPurchaseSuccessful("30");
    }

    public void onPurchaseSuccessful()
    {
        onPurchaseSuccessful("1500");
    }

    public void onPurchaseSuccessful(string amount)
    {
        Debug.Log("onPurchaseSuccessful");
        int newValue = int.Parse(amount);
        addLunchMoney(newValue);
        VNLUtil.getInstance().displayMessage(new string[1] { "receivedMoney" }, null, true, true, newValue.ToString());
        save(null);
        GameStart.setNeedToFinishPurchase(false);
    }

    public void onPurchaseFailed()
    {
        VNLUtil.getInstance().displayMessage("momSaidNo", true);
    }

    public void apprequestsFailed()
    {
        APIService.logFlurryEvent("apprequestsFailed");
        VNLUtil.getInstance().displayMessage("I guess you don't like money!", true);
    }

    public void apprequestsSuccess(string result)
    {
        string @string = PlayerPrefs.GetString("apprequestsSuccess", string.Empty);
        Debug.Log("previouslySaved: " + @string);
        string[] collection = @string.Split(',');
        List<string> list = new List<string>(collection);
        Debug.Log("result: " + result);
        string[] array = result.Split(',');
        int num = 0;
        string[] array2 = array;
        foreach (string item in array2)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
                num++;
            }
        }
        string text = string.Empty;
        int num2 = 0;
        foreach (string item2 in list)
        {
            num2++;
            if (!string.Empty.Equals(item2))
            {
                text += item2;
                if (num2 < list.Count)
                {
                    text += ",";
                }
            }
        }
        Debug.Log("new list: " + text);
        PlayerPrefs.SetString("apprequestsSuccess", text);
        int num3 = num * 5;
        APIService.logFlurryEvent("apprequestsSuccess" + num);
        VNLUtil.getInstance().displayMessage("You shared with " + num + " new friends.  So I'll pay you $" + num3 + "!", true);
        addLunchMoney(num3);
    }
}
