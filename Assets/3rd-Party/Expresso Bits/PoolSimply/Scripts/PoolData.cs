using UnityEngine;

namespace ExpressoBits.PoolSimply
{
    [CreateAssetMenu(fileName = "PoolData", menuName = "ExpressoBits/PoolSimply/PoolData", order = 0)]
    public class PoolData : ScriptableObject 
    {

        public int initialAmount = 20;
        public int increaseAmount = 5;

        [Header("Unstable configurations")]
        public bool willGrow = true;
        public int maxAmountObjects = 50;

    }
}