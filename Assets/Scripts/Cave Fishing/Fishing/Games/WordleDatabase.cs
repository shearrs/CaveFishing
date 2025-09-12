using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public static class WordleDatabase
    {
        private static readonly string[] words = new string[]
        { 
            "hooks", "lines", "sinks", "flies", "float", 
            "ropes", "cable", "winch", "angle", "casts", 
            "drift", "catch", "chums", "swing", "canoe", 
            "ferry", "yacht", "kayak", "decks", "sails",
            "coast", "shore", "tides", "river", "caves",
            "depth", "lakes", "water", "swamp", "hooks",
            "guppy", "bream", "poach", "fried", "worms"
        };

        public static string GetWord()
        {
            return words[Random.Range(0, words.Length)];
        }
    }
}
