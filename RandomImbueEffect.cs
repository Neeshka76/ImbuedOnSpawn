using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace ImbuedOnSpawn
{
    public class RandomImbueEffect : MonoBehaviour
    {
        private string imbueInstance;
        private Item item;
        private bool keepImbued;

        public void Init(Item item, string idImbue, bool keepImbued)
        {
            this.item = item;
            imbueInstance = idImbue;
            this.keepImbued = keepImbued;
            this.item.OnDespawnEvent += Item_OnDespawnEvent;
        }

        private void Item_OnDespawnEvent(EventTime eventTime)
        {
            if (eventTime == EventTime.OnStart)
                Dispose();
        }

        public void Update()
        {
            if (!keepImbued) return;
            if (Snippet.imbueBelowLevelItem(item, 75))
            {
                Snippet.ImbueItem(item, imbueInstance);
            }
        }
        public void Dispose()
        {
            item.OnDespawnEvent -= Item_OnDespawnEvent;
            Destroy(this);
        }
    }
}
