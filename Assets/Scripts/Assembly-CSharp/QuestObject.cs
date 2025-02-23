using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
	[NonSerialized]
	public Quest quest;

	public List<string> successTexts;

	public List<string> talkTexts;

	public bool completeByDestory;

	public bool completeByDeath;

	public bool completeByTouch;

	public bool willNotEndQuest;

	public bool showArrow = true;

	private bool complete;

	public float distanceToStartTalk = 10f;

	private PlayerAttackComponent player;

	private Transform playerTransform;

	private Transform myTransform;

	public float arrowVOffset = 6f;

	private GameObject arrowHolder;

	public bool noQuestMarker;

	private GameObject exclaimationMarkHolder;

	public void init()
	{
		Debug.Log("Quest Object: " + base.name + " awake.");
		if (showArrow)
		{
			GameObject gameObject = VNLUtil.instantiate("arrow");
			Transform child = base.transform;
			if (child.GetChildCount() > 0)
			{
				child = child.GetChild(0);
			}
			Vector3 position = child.position;
			position.y += arrowVOffset;
			arrowHolder = new GameObject("arrowHolder");
			arrowHolder.transform.position = position;
			arrowHolder.transform.parent = child;
			gameObject.transform.parent = arrowHolder.transform;
		}
		if (!noQuestMarker && exclaimationMarkHolder == null)
		{
			exclaimationMarkHolder = VNLUtil.getInstance().getExclaimationMark(base.transform);
		}
		if (completeByDeath)
		{
			AttackComponent atk = GetComponent<AttackComponent>();
			if (atk != null)
			{
				VNLUtil.getInstance().doStartCoRoutine(delegate
				{
					Debug.Log("Quest Object: " + base.name + " completeByDeath.");
					AttackComponent attackComponent = atk;
					attackComponent.onDead = (VNLUtil.NoNameMethod)Delegate.Combine(attackComponent.onDead, new VNLUtil.NoNameMethod(completeQuest));
				});
			}
			else
			{
				Debug.LogError(base.name + " doesn't have an attack component to do completeByDeath, maybe use complete by destroy instead");
			}
		}
		player = VNLUtil.getInstance<PlayerAttackComponent>("Player");
		PlayerAttackComponent playerAttackComponent = player;
		playerAttackComponent.onGoToScene = (VNLUtil.NoNameMethod)Delegate.Combine(playerAttackComponent.onGoToScene, new VNLUtil.NoNameMethod(onPlayerLeaveScene));
		playerTransform = player.transform;
		myTransform = base.transform;
		if (talkTexts != null && talkTexts.Count != 0)
		{
			StartCoroutine(detectPlayerInTalkingRange());
		}
	}

	private void onPlayerLeaveScene()
	{
		completeByDestory = false;
		PlayerAttackComponent playerAttackComponent = player;
		playerAttackComponent.onGoToScene = (VNLUtil.NoNameMethod)Delegate.Remove(playerAttackComponent.onGoToScene, new VNLUtil.NoNameMethod(onPlayerLeaveScene));
	}

	private IEnumerator detectPlayerInTalkingRange()
	{
		float distance;
		do
		{
			yield return new WaitForSeconds(0.5f);
			distance = Vector3.Distance(playerTransform.position, myTransform.position);
		}
		while (!(distance < distanceToStartTalk));
		VNLUtil.getInstance().displayMessage(talkTexts.ToArray(), null, false, true, null);
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (!complete && completeByTouch)
		{
			GameObject gameObject = collision.gameObject;
			if (gameObject.CompareTag("Player"))
			{
				completeQuest();
			}
		}
	}

	private void completeQuest()
	{
		if (!complete)
		{
			PlayerAttackComponent playerAttackComponent = player;
			playerAttackComponent.onGoToScene = (VNLUtil.NoNameMethod)Delegate.Remove(playerAttackComponent.onGoToScene, new VNLUtil.NoNameMethod(onPlayerLeaveScene));
			Debug.Log(base.name + " complete Quest Object");
			complete = true;
			UnityEngine.Object.Destroy(arrowHolder);
			if (exclaimationMarkHolder != null)
			{
				UnityEngine.Object.Destroy(exclaimationMarkHolder.gameObject);
			}
			if (successTexts != null && successTexts.Count > 0)
			{
				VNLUtil.getInstance().displayMessage(successTexts.ToArray(), null, false, true, null).setTitle(quest.name);
			}
			quest.notifyComplete(willNotEndQuest);
		}
	}

	private void OnDestroy()
	{
		if (completeByDestory)
		{
			completeQuest();
		}
	}
}
