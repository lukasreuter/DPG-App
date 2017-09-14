using System.Collections.Generic;

public static class Jars
{
	public static Dictionary<int, string> Numbers()
	{
		var mp = new Dictionary<int, string>();

		while (mp.Count < 256)
		{
			mp.Add(mp.Count, "2");
			mp.Add(mp.Count, "3");
			mp.Add(mp.Count, "4");
			mp.Add(mp.Count, "5");
			mp.Add(mp.Count, "6");
			mp.Add(mp.Count, "7");
			mp.Add(mp.Count, "8");
			mp.Add(mp.Count, "9");
		}

		return mp;
	}

	public static Dictionary<int, string> Special()
	{
		var mp = new Dictionary<int, string>();

		while (mp.Count < 256)
		{
			mp.Add(mp.Count, ".");
			mp.Add(mp.Count, ",");
			mp.Add(mp.Count, "!");
			mp.Add(mp.Count, "*");
			mp.Add(mp.Count, "#");
			mp.Add(mp.Count, "&");
			mp.Add(mp.Count, "^");
			mp.Add(mp.Count, "~");
		}

		return mp;
	}

	public static Dictionary<int, string> Lower()
	{
		var mp = new Dictionary<int, string>();

		while (mp.Count < 256)
		{
			mp.Add(mp.Count, "a");
			mp.Add(mp.Count, "b");
			mp.Add(mp.Count, "c");
			mp.Add(mp.Count, "d");
			mp.Add(mp.Count, "e");
			mp.Add(mp.Count, "f");
			mp.Add(mp.Count, "g");
			mp.Add(mp.Count, "h");
			mp.Add(mp.Count, "j");
			mp.Add(mp.Count, "k");
			mp.Add(mp.Count, "m");
			mp.Add(mp.Count, "n");
			mp.Add(mp.Count, "p");
			mp.Add(mp.Count, "r");
			mp.Add(mp.Count, "s");
			mp.Add(mp.Count, "t");
		}

		return mp;
	}

	public static Dictionary<int, string> Upper()
	{
		var mp = new Dictionary<int, string>();

		while (mp.Count < 256)
		{
			mp.Add(mp.Count, "A");
			mp.Add(mp.Count, "B");
			mp.Add(mp.Count, "C");
			mp.Add(mp.Count, "D");
			mp.Add(mp.Count, "E");
			mp.Add(mp.Count, "F");
			mp.Add(mp.Count, "G");
			mp.Add(mp.Count, "H");
			mp.Add(mp.Count, "J");
			mp.Add(mp.Count, "K");
			mp.Add(mp.Count, "M");
			mp.Add(mp.Count, "N");
			mp.Add(mp.Count, "P");
			mp.Add(mp.Count, "R");
			mp.Add(mp.Count, "S");
			mp.Add(mp.Count, "T");
		}

		return mp;
	}

	public static Dictionary<int, string> Words()
	{
		var mp = new Dictionary<int, string>();

		var word_list = new string[]
		{
			"and", "ask", "ass", "ape", "ate", "axe", "air", "aim", "ana", "awe", "act", "add", "age", "all", "ant",
			"bat", "ban", "bar", "bed", "bee", "bet", "bit", "bug", "bob", "bot", "boy", "bud", "but",
			"cab", "can", "cap", "cat", "car", "cog", "con", "cop", "cot", "cow", "coy", "cub", "cut",
			"dad", "dam", "dan", "day", "den", "did", "dig", "dip", "doc", "dog", "don", "dot", "dry", "dug",
			"ear", "eat", "egg", "ego", "elf", "elk", "elm", "end", "eye", "eve",
			"fad", "fan", "far", "fat", "fax", "fig", "fit", "fix", "fly", "few", "foe", "fog", "for", "fur",
			"gag", "gap", "gel", "gem", "get", "god", "goo", "got", "gum", "gun", "gut", "guy", "gym",
			"hot", "how", "has", "had", "ham", "hat", "him", "her", "hit", "hop",
			"ice", "icy", "ill", "ink", "inn", "ion", "its", "ivy",
			"jam", "jar", "jaw", "jay", "jet", "jim", "joe", "jog", "jot", "joy", "jug",
			"keg", "ken", "key", "kid", "kim", "kit", "kin",
			"lab", "lad", "lap", "law", "lie", "lee", "let", "lip", "lob", "log", "lot", "low", "lug",
			"mac", "mag", "map", "man", "mat", "max", "meg", "men", "met", "mom", "moo", "mop", "mow", "mud", "mug", "mut",
			"nab", "nag", "nap", "net", "new", "nip", "nod", "not", "now", "nun", "nut",
			"oak", "oat", "oar", "off", "oil", "old", "one", "our", "out", "own",
			"pan", "pal", "pam", "pat", "pea", "pen", "pet", "pig", "pit", "pot",
			"rag", "ray", "run", "ram", "ran", "rap", "rat", "rig", "rip", "rob", "ron", "rot",
			"sad", "sag", "sam", "sat", "say", "see", "sex", "set", "she", "shy", "sin", "sir", "sit", "sky", "soy", "sun",
			"tan", "tap", "tar", "tea", "ted", "too", "the", "tim", "tip", "toe", "tom", "toy",
			"wag", "was", "wax", "way", "web", "wee", "wet", "why", "wig", "win", "wow", "won",
			"yak", "yam", "yap", "yen", "yep", "yes", "yet", "yew", "you", "yum",
			"zag", "zig", "zit", "zap", "zip", "zoo"
		};
		
		foreach (var word in word_list)
		{
			mp.Add(mp.Count, word);
		}

		return mp;
	}
}
