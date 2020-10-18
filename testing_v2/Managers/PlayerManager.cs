using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using testing_v2.Models;
using System.Xml.Serialization;

namespace testing_v2.Managers
{
    public class PlayerManager
    {
        private static string _filename = "/playerInfo3.xml";

        public Player Player { get; private set; }

        public PlayerManager() : this(new Player())
        {

        }
        public PlayerManager(Player player)
        {
            Player = player;
        }


        //will return a blockstats object if the current block is full
        public BlockStats caseComplete(bool correct, char casetype)
        {
            BlockStats b = Player.caseComplete(correct, casetype);
            PlayerManager.save(this);
            return b;
        } 

        public static PlayerManager Load()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + _filename;
            if (!File.Exists(path))
                return new PlayerManager();

            using (var reader = new StreamReader(new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + _filename, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(Player));

                var readplayer = (Player)serializer.Deserialize(reader);

                return new PlayerManager(readplayer);
            }
        }
        public static void save(PlayerManager PlayerManager)
        {
            //overwirtes the old player xml file
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + _filename;
            using (var writer = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                var serializer = new XmlSerializer(typeof(Player));

                serializer.Serialize(writer, PlayerManager.Player);

            }
        }

    }
}
