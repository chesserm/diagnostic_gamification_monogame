using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using testing_v2.Models;
using System.Xml.Serialization;

namespace testing_v2.Managers
{
    class BlockStatsManager
    {
        private static string _filename = "completedBlocks.xml";

        public List<BlockStats> Blocks { get; private set; }

        public BlockStatsManager() : this(new List<BlockStats>())
        {

        }
            public BlockStatsManager(List<BlockStats> blocks)
        {
            Blocks = blocks;
        }

        public static BlockStatsManager Load()
        {
            if (!File.Exists(_filename))
                return new BlockStatsManager();

            using (var reader = new StreamReader(new FileStream(_filename, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(List<BlockStats>));

                var readBlocks = (List<BlockStats>)serializer.Deserialize(reader);

                return new BlockStatsManager(readBlocks);
            }
        }
        public static void save(BlockStatsManager BlockStatsManager)
        {
            //overwirtes the old blocks xml file
            using (var writer = new StreamWriter(new FileStream(_filename, FileMode.Create)))
            {
                var serializer = new XmlSerializer(typeof(List<BlockStats>));

                serializer.Serialize(writer, BlockStatsManager.Blocks);

            }
        }
    }
}
