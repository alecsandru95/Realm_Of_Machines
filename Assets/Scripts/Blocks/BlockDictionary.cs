using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Blocks
{
    public class BlockDictionary
    {
        private static readonly LogHelper _Log = new LogHelper(typeof(BlockDictionary));

        public static readonly BlockDictionary Instance = new BlockDictionary();

        private Dictionary<long, Block> _BlockMap;

        private BlockDictionary()
		{
            
        }

        public void LoadDictionary()
		{
            if(_BlockMap == null)
			{
                var stopWatch = Stopwatch.StartNew();

                var allBlockPrefabs = Resources.LoadAll<Block>("Blocks");
                _BlockMap = allBlockPrefabs.ToDictionary(b => b.BlockId, b => b);
                stopWatch.Stop();

                _Log.WriteInfo($"{allBlockPrefabs.Length} blocks loaded in {stopWatch.ElapsedMilliseconds}ms");
            }
		}

        public static Block GetBlock(long id)
		{
            if(Instance._BlockMap == null)
			{
                _Log.WriteError("Not initialized");
                return null;
			}

			if (Instance._BlockMap.ContainsKey(id))
			{
                return Instance._BlockMap[id];
			}

            return null;
		}
    }
}