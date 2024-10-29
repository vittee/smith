const int magic1 = 759;

var lics = new string[3];

for (var i = 0; i < 3; i++)
{
    Console.Write($"Enter License Info {i+1}: ");
    lics[i] = Console.ReadLine() ?? "";
}

var key = MakeKey(lics[0], lics[1], lics[2]);
File.WriteAllLines("smith.key", lics.Concat([key]));

Console.WriteLine("Done, copy the smith.key file to Smith directory");

return;

string Crypt0(string s, int[] divisors)
{
    var sum = magic1 + s.Aggregate(0, (current, c) => current + (sbyte) c);
    return string.Join("", divisors.Select(d =>
    {
        var r = sum % d;

        if (r == 0)
        {
            r = 33;
        }

        while (r < 33)
        {
            r *= 2;
        }

        while (r >= 127)
        {
            r /= 2;
        }

        return r.ToString();
    }));
}

string Crypt1(string s)
{
    return Crypt0(s, [7, 21, 59, 29, 13, 19]);
}

string Crypt2(string s)
{
    return Crypt0(s, [71, 83, 37, 11, 61, 53, 47]);
}

string Crypt3(string s)
{
    return Crypt0(s, [97, 67, 41, 17, 79, 89, 2, 3]);
}

string MakeKey(string lic1, string lic2, string lic3)
{
    var key = Crypt1(lic1) + Crypt2(lic2) + Crypt3(lic3);
    return string.Join("", key.Chunk(2).Select(cc => Convert.ToChar(Convert.ToInt32(string.Join("", cc)))));
}

