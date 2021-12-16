namespace AdventOfCode.Days;

[Day(2021, 16)]
public class Day16 : BaseDay
{
    public override string PartOne(string input)
    {
        var binary = input.Trim().HexToBinary();
        var root = new Packet(binary);

        return root.VersionSum().ToString();
    }

    public override string PartTwo(string input)
    {
        var binary = input.Trim().HexToBinary();
        var root = new Packet(binary);

        return root.GetValue().ToString();
    }
}

public class Packet
{
    public int Version { get; set; }
    public string TypeID { get; set; }
    public int TotalBits { get; set; }
    public long LiteralValue { get; set; }

    public List<Packet> Children { get; private set; }

    public Packet(ReadOnlySpan<char> binary)
    {
        Children = new List<Packet>();

        var pos = 0;

        Version = Convert.ToInt32(binary.Slice(pos, 3).ToString(), 2);
        pos += 3;

        TypeID = binary.Slice(pos, 3).ToString();
        pos += 3;

        pos += TypeID == "100" ? ParseLiteralPacket(binary[pos..]) : ParseOperatorPacket(binary[pos..]);

        TotalBits = pos;
    }

    private int ParseOperatorPacket(ReadOnlySpan<char> binary)
    {
        var lengthTypeId = binary[..1].ToString();
        var pos = 1;

        pos += lengthTypeId == "0" ? ParseOperatorBitLength(binary[pos..]) : ParseOperatorPacketCount(binary[pos..]);

        return pos;
    }

    private int ParseOperatorPacketCount(ReadOnlySpan<char> binary)
    {
        var packetCount = Convert.ToInt32(binary[..11].ToString(), 2);
        var pos = 11;

        for (var i = 0; i < packetCount; i++)
        {
            var packet = new Packet(binary[pos..]);
            Children.Add(packet);
            pos += packet.TotalBits;
        }

        return pos;
    }

    private int ParseOperatorBitLength(ReadOnlySpan<char> binary)
    {
        var length = Convert.ToInt32(binary[..15].ToString(), 2);
        var pos = 15;

        while (pos < (15 + length))
        {
            var packet = new Packet(binary[pos..]);
            Children.Add(packet);
            pos += packet.TotalBits;
        }

        return pos;
    }

    private int ParseLiteralPacket(ReadOnlySpan<char> binary)
    {
        var group = binary[..5].ToString();
        var literal = group.ShaveLeft(1);
        var pos = 5;

        while (group.StartsWith("1"))
        {
            group = binary.Slice(pos, 5).ToString();
            literal += group.ShaveLeft(1);
            pos += 5;
        }

        LiteralValue = Convert.ToInt64(literal, 2);
        return pos;
    }

    public int VersionSum()
    {
        var result = Version;
        result += Children.Sum(c => c.VersionSum());

        return result;
    }

    public long GetValue()
    {
        return TypeID switch
        {
            "100" => LiteralValue,
            "000" => Children.Sum(c => c.GetValue()),
            "001" => Children.Multiply(c => c.GetValue()),
            "010" => Children.Min(c => c.GetValue()),
            "011" => Children.Max(c => c.GetValue()),
            "101" => Children.First().GetValue() > Children.Last().GetValue() ? 1 : 0,
            "110" => Children.First().GetValue() < Children.Last().GetValue() ? 1 : 0,
            "111" => Children.First().GetValue() == Children.Last().GetValue() ? 1 : 0,
            _ => throw new Exception(),
        };
    }
}
