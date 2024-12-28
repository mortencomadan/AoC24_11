namespace AoC24_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stone stone = new("test.txt");
            for (int i = 0; i < 75; i++)
            {
                Console.WriteLine(i);
                stone.Iterate();
            } 
            Console.WriteLine(stone.StoneCount);
        }
    }

    internal class Stone
    {
        List<UInt64> stones;
        public Stone(string filename)
        {
            stones = [];
            var f = File.ReadAllText(filename);
            stones = f.Trim().Split(' ').Select(UInt64.Parse).ToList();
        }

        public Int64 StoneCount => stones.Count;
        public void Iterate()
        {
            List<UInt64> newList = [];
            for (int i = 0; i < stones.Count; i++)
            {
                var stone = stones[i];
                if (stone == 0) newList.Add(1);
                else if (CanBeSplit(stone)) {
                    var split = Split(stone);
                    newList.Add(split.first);
                    newList.Add(split.second);                    
                    
                }
                else newList.Add(stone * 2024);
            }
            stones = new(newList);
        }

        int GetDigits(UInt64 number) => number switch
        {
            < 10 => 1,
            < 100 => 2,
            < 1000 => 3,
            < 10000 => 4,
            < 100000 => 5,
            < 1000000 => 6,
            < 10000000 => 7,
            < 100000000 => 8,
            < 1000000000 => 9,
            < 10000000000 => 10,
            < 100000000000 => 11,
            < 1000000000000 => 12,
            < 10000000000000 => 13,
            < 100000000000000 => 14,
            
            _ => throw new NotImplementedException()
        };

        UInt64 GetMask(int digits) => digits switch
        {
            2 => 10,
            4 => 100,
            6 => 1000,
            8 => 10000,
            10 => 100000,
            12 => 1000000,
            14 => 10000000,
            _ => throw new NotImplementedException()
        };

        bool CanBeSplit(UInt64 number) => (GetDigits(number) % 2 == 0);
        

        (UInt64 first, UInt64 second) Split(UInt64 number)
        {
            int digits = GetDigits(number);
            UInt64 mask = GetMask(digits);

            UInt64 first = number / mask;
            UInt64 second = number - (first * mask);
            return (first,  second);
        }
    }
}
