using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    public class DepositSlotsModule : MonoBehaviour
    {
        [SerializeField] private DepositSlotBlock blockPrefab;
        private DepositSlots depositSlots;

        // Update is called once per frame
        void Update()
        {

        }

        public void Init(DepositSlots depositSlots)
        {
            this.depositSlots = depositSlots;

            DepositSlotBlock block;

            foreach (ResourceDeposit deposit in depositSlots.Slots.Keys)
            {
                block = Instantiate(blockPrefab);

                block.transform.SetParent(transform);

                block.Init(depositSlots, deposit);
            }
        }
    }
}