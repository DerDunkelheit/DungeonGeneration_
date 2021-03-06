﻿using System.Collections;
using Objects.InteractiveObjects;
using Player_Abilities_Stats.Hunger;
using TMPro;
using UnityEngine;

namespace Objects.Core_Game_Objects
{
    public class Exit : InteractiveObjectBase
    {
        [Header("Fade Fields")]
        [SerializeField] GameObject fadePanel = null;

        [SerializeField] bool shouldHaveEnemyCount = true;
        [SerializeField] GameObject logMessagePanel = null;
        [TextArea(3, 5)] [SerializeField] string logMessage = "";

        Transform playerTrans;
        TextMeshProUGUI textVar;

        protected override void Start()
        {
            base.Start();

            logMessagePanel.SetActive(false);
            textVar = logMessagePanel.GetComponentInChildren<TextMeshProUGUI>();

            coll2D.size = Vector2.one * 0.1f;
            coll2D.isTrigger = true;

        }

        private void SpawnFadePanel()
        {
            GameObject fadePanelGo = Instantiate(fadePanel, Vector3.zero, Quaternion.identity);
        }

        private void UpdateFloorCount()
        {
            LevelManager.instance.UpdateAdditionalFloorCount();
            LevelManager.instance.currentLevelID++;
            LevelManager.instance.ManageDungeonType();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))

                if (shouldHaveEnemyCount)
                {
                    {
                        if (LevelManager.instance.CurrentEnemiesCount == 0)
                        {
                            playerTrans = other.GetComponent<Transform>();
                            UpdateFloorCount();
                            StartCoroutine(waitBeforLoadNextScene(1, false));
                        }
                        else
                        {
                            logMessagePanel.SetActive(true);
                            textVar.text = $"{logMessage}:{LevelManager.instance.CurrentEnemiesCount}";
                        }
                    }
                }
                else
                {
                    playerTrans = other.GetComponent<Transform>();
                    playerTrans.GetComponent<Hunger>().enableHunger = true; //Enable Hunger in next level after level with NPC
                    StartCoroutine(waitBeforLoadNextScene(1,true));
                }
        }

        private IEnumerator waitBeforLoadNextScene(float time,bool loadSceneOrReloadSCene)
        {
            SpawnFadePanel();
            yield return new WaitForSeconds(time);
            if(loadSceneOrReloadSCene)
            {
                SceneTransition.LoadScene("CavernsBeginning");
                playerTrans.position = Vector2.zero;
            }
            else
            {
                SceneTransition.ReloadCurrentScene();
                playerTrans.position = Vector2.zero;
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                logMessagePanel.SetActive(false);
            }
        }
    }
}
