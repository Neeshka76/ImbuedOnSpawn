using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace ImbuedOnSpawn
{
    public class ImbuedOnSpawnLevelModule : LevelModule
    {
        public bool keepImbued = true;
        public string[] imbueType =
        {
            "Fire",
            "Lightning",
            "Gravity"
        };
        public bool useWhitelistInstead = false;
        public string[] blacklistItemsId =
        {
            ""
        };
        public string[] whitelistItemsId =
        {
            ""
        };
        

        public override IEnumerator OnLoadCoroutine()
        {
            EventManager.onItemSpawn += EventManager_onItemSpawn;
            return base.OnLoadCoroutine();
        }

        private void EventManager_onItemSpawn(Item item)
        {
            if (IsToImbued(item))
                GiveRandomImbueEffect(item);
        }

        /// <summary>
        /// Give the Random Imbue Effect to the Item
        /// </summary>
        private void GiveRandomImbueEffect(Item item)
        {
            string id = imbueType[UnityEngine.Random.Range(0, imbueType.Length)];
            if (item.gameObject.GetComponent<RandomImbueEffect>() == null)
                item.gameObject.AddComponent<RandomImbueEffect>().Init(item, id, keepImbued);
            else
            {
                item.gameObject.GetComponent<RandomImbueEffect>().Dispose();
                item.gameObject.AddComponent<RandomImbueEffect>().Init(item, id, keepImbued);
            }
        }

        private bool IsToImbued(Item item)
        {
            if (!useWhitelistInstead)
            {
                if (!blacklistItemsId.Contains(item.itemId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (whitelistItemsId.Contains(item.itemId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override void OnUnload()
        {
            base.OnUnload();
            EventManager.onItemSpawn -= EventManager_onItemSpawn;
        }
    }
}
