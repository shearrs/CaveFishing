using UnityEngine;

namespace CaveFishing.Games.FishTypingGame
{
    public static class WordDatabase
    {
        private static readonly string[] words = new string[]
        {
            "angler", "bait", "hook", "line", "reel", "rod", "lure", "sinkers", "bobber", "tackle",
            "fly", "spinner", "jig", "net", "float", "cast", "catch", "fish", "fishing", "trout",
            "bass", "salmon", "catfish", "carp", "perch", "pike", "walleye", "bluegill", "crappie", "mackerel",
            "snapper", "tuna", "marlin", "swordfish", "barracuda", "cod", "halibut", "flounder", "shrimp", "lobster",
            "crab", "baitfish", "minnow", "chum", "tacklebox", "hookset", "drag", "leader", "knot", "bobberstop",
            "sink", "floatline", "backlash", "spool", "fisherman", "fisherwoman", "harbor", "dock", "pier", "boat",
            "canoe", "kayak", "raft", "oar", "anchor", "buoy", "current", "wave", "tide", "ripple",
            "stream", "river", "lake", "pond", "ocean", "sea", "bay", "lagoon", "reef", "shore",
            "coast", "beach", "jetty", "wharf", "marina", "wetlands", "estuary", "waterfall", "brook", "creek",
            "dam", "reservoir", "harpoon", "gillnet", "trawler", "baitcaster", "spincast", "flyrod", "bobberfloat", "fishfinder"
        };

        public static string GetWord() => words[Random.Range(0, words.Length)];
    }
}
