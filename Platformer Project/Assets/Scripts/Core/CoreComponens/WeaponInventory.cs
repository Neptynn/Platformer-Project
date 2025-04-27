using System;
using Platformer.CoreSystem;
using Platformer.Weapons;
using UnityEngine;

namespace Platformer.Core.CoreComponens
{
    public class WeaponInventory : CoreComponent
    {
        public event Action<int, WeaponDataSO> OnWeaponDataChanged;

        [field: SerializeField] public WeaponDataSO[] WeaponData { get; private set; }

        public bool TrySetWeapon(WeaponDataSO newData, int index, out WeaponDataSO oldData)
        {
            if (index >= WeaponData.Length)
            {
                oldData = null;
                return false;
            }

            oldData = WeaponData[index];
            WeaponData[index] = newData;

            OnWeaponDataChanged?.Invoke(index, newData);

            return true;
        }

        public bool TryGetWeapon(int index, out WeaponDataSO data)
        {
            if (index >= WeaponData.Length)
            {
                data = null;
                return false;
            }

            data = WeaponData[index];
            return true;
        }

        public bool TryGetEmptyIndex(out int index)
        {
            for (var i = 0; i < WeaponData.Length; i++)
            {
                if (WeaponData[i] is not null)
                    continue;

                index = i;
                return true;
            }

            index = -1;
            return false;
        }

        public WeaponSwapChoice[] GetWeaponSwapChoices()
        {
            var choices = new WeaponSwapChoice[WeaponData.Length];

            for (var i = 0; i < WeaponData.Length; i++)
            {
                var data = WeaponData[i];

                choices[i] = new WeaponSwapChoice(data, i);
            }

            return choices;
        }
    }
}