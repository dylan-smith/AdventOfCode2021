namespace AdventOfCode.Days;

[Day(2021, 16)]
public class Day16 : BaseDay
{
    public override string PartOne(string input)
    {
        var binary = input.Trim().HexToBinary();

        var root = new Packet(binary, Log);

        return root.VersionSum().ToString();
    }

    private object GetPacket(string binary)
    {
        throw new NotImplementedException();
    }

    public override string PartTwo(string input)
    {
        var binary = input.Trim().HexToBinary();

        var root = new Packet(binary, Log);

        return root.GetValue().ToString();
    }
}

public class Packet
{
    public int Version { get; set; }
    public string TypeID { get; set; }
    public long LiteralValue { get; set; }
    public int TotalBits { get; set; }
    private readonly Action<string> _log;

    public List<Packet> Children { get; private set; }

    public Packet(string binary, Action<string> Log)
    {
        _log = Log;
        _log($"Parsing Packet...");
        Children = new List<Packet>();

        var pos = 0;

        Version = Convert.ToInt32(binary.Substring(0, 3), 2);
        pos += 3;

        TypeID = binary.Substring(pos, 3);
        pos += 3;

        pos = TypeID == "100" ? ParseLiteralPacket(binary, pos) : ParseOperatorPacket(binary, pos);

        TotalBits = pos;
    }

    private int ParseOperatorPacket(string binary, int pos)
    {
        _log($"Parsing Operator Packet");
        var lengthTypeId = binary.Substring(pos, 1);
        var result = pos + 1;

        result = lengthTypeId == "0" ? ParseOperatorBitLength(binary, result) : ParseOperatorPacketCount(binary, result);

        return result;
    }

    private int ParseOperatorPacketCount(string binary, int pos)
    {
        _log($"Parsing Operator Packet (Packet Count)");
        var packetCount = Convert.ToInt32(binary.Substring(pos, 11), 2);
        var result = pos + 11;

        packetCount.Times(() =>
        {
            var packet = new Packet(binary.Substring(result), _log);
            Children.Add(packet);
            result += packet.TotalBits;
        });

        return result;
    }

    private int ParseOperatorBitLength(string binary, int pos)
    {
        _log($"Parsing Operator Packet (Bit Length)");
        var length = Convert.ToInt32(binary.Substring(pos, 15), 2);
        var result = pos + 15;

        while (result < (pos + 15 + length))
        {
            var packet = new Packet(binary.Substring(result), _log);
            Children.Add(packet);
            result += packet.TotalBits;
        }

        return result;
    }

    private int ParseLiteralPacket(string binary, int pos)
    {
        _log($"Parsing Literal Packet");
        var group = binary.Substring(pos, 5);
        var literal = group.ShaveLeft(1);
        var result = pos + 5;

        while (group.StartsWith("1"))
        {
            group = binary.Substring(result, 5);
            literal += group.ShaveLeft(1);
            result += 5;
        }

        LiteralValue = Convert.ToInt64(literal, 2);
        return result;
    }

    public int VersionSum()
    {
        var result = Version;

        result += Children.Sum(c => c.VersionSum());

        return result;
    }

    public long GetValue()
    {
        if (TypeID == "100")
        {
            return LiteralValue;
        }

        if (TypeID == "000")
        {
            return Children.Sum(c => c.GetValue());
        }

        if (TypeID == "001")
        {
            return Children.Multiply(c => c.GetValue());
        }

        if (TypeID == "010")
        {
            return Children.Min(c => c.GetValue());
        }

        if (TypeID == "011")
        {
            return Children.Max(c => c.GetValue());
        }

        if (TypeID == "101")
        {
            return Children.First().GetValue() > Children.Last().GetValue() ? 1 : 0;
        }

        if (TypeID == "110")
        {
            return Children.First().GetValue() < Children.Last().GetValue() ? 1 : 0;
        }

        if (TypeID == "111")
        {
            return Children.First().GetValue() == Children.Last().GetValue() ? 1 : 0;
        }

        throw new Exception();
    }
}
