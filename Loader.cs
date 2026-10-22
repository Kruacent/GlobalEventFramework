using Exiled.API.Features;
using Exiled.Events;
using GlobalEventFramework.API.Features;
using InventorySystem.Items.Firearms;
using KE.Utils.API.Features;
using LabApi.Features.Wrappers;
using MEC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GlobalEventFramework
{
    internal class Loader
    {
        public static Loader Instance { get; private set; } = null;
        public const char Separator = '_';
        public string Path => System.IO.Path.Combine(Paths.Configs, MainPlugin.Configs.ExternalFileName);

        public Loader()
        {
            Instance = this;
        }

        public void Load()
        {
            Log.Warn("load");
            Timing.RunCoroutine(AsyncLoad());
        }

        private void CheckAndCreateFile()
        {
            List<KEEvents> keevents = KEEvents.List.OrderBy(e => e.Name).ToList();

            bool different = false;
            bool skip = false;

            Log.Warn("reading create file");
            if (!File.Exists(Path))
            {
                Log.Warn("Global event file doesn't exist");
                skip = true;
                different = true;
            }

            

            if (!skip)
            {
                Log.Warn("reading file");
                List<string> lines = File.ReadLines(Path).ToList();
                if(lines.Count != keevents.Count)
                {
                    Log.Warn($"({lines.Count} instead {keevents.Count})");
                    different = true;
                }

                for (int i = 0; i < lines.Count; i++)
                {
                    string[] word = lines[i].Split(Separator);

                    if (word.Length != 2)
                    {
                        Log.Warn("malformed (too much words)");
                        continue;
                    }
                    string name = word[0];
                    if (!int.TryParse(word[1], out int weight))
                    {
                        Log.Warn("malformed (couldn't parse weight)");
                        continue;
                    }

                    if (!KEEvents.TryGet(name, out KEEvents kEEvents))
                    {
                        Log.Warn("couldn't find KEEvents");
                        continue;
                    }

                    if (keevents[i].Name != name)
                    {
                        different = true;
                    }
                    int oldWeight = kEEvents.WeightedChance;
                    if (oldWeight != weight)
                    {
                        Log.Warn($"KEEvent {name} weights update : {oldWeight}->{weight}");
                        kEEvents.WeightedChance = weight;
                        Log.Warn($"updated {kEEvents.WeightedChance}");
                        different = true;
                    }
                }
            }



            if (different)
            {
                Rewrite(keevents);
            }

        }

        private IEnumerator<float> AsyncLoad()
        {
            try
            {
                CheckAndCreateFile();
            }
            catch(Exception e)
            {
                Log.Error(e);
            }
            yield return 0;
        }


        private void Rewrite(List<KEEvents> keevents)
        {
            Log.Warn("rewrite file");
            string[] newlines = new string[keevents.Count];

            Log.Warn("keevent numbers = "+keevents.Count);

            for (int i = 0; i < newlines.Length; i++)
            {
                newlines[i] = keevents[i].Parse();
            }

            File.WriteAllLines(Path, newlines);
        }


    }
}
