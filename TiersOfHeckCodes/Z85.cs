using System.Text;

namespace TiersOfHeckCodes;

public class Z85
{
    public static string ToZ85String(byte[] inArray, bool autoPad = false)
	{
		if (inArray == null)
		{
			throw new ArgumentNullException("inArray");
		}
		if (inArray.Length == 0)
		{
			return string.Empty;
		}
		int num = inArray.Length % 4;
		bool flag = num != 0;
		if (!autoPad && flag)
		{
			throw new ArgumentException("Array length invalid for encoding. Must be a multiple of 4.");
		}
		byte[] array = inArray;
		int num2 = 0;
		if (autoPad && flag)
		{
			num2 = 4 - num;
			array = new byte[inArray.Length + num2];
			Array.Copy(inArray, 0, array, 0, inArray.Length);
		}
		string text = Z85.ToZ85String(array);
		if (flag)
		{
			text += num2.ToString();
		}
		return text;
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x0003E1DC File Offset: 0x0003C3DC
	private static string ToZ85String(byte[] inArray)
	{
		StringBuilder stringBuilder = new StringBuilder(inArray.Length / 4 * 5);
		for (int i = 0; i < inArray.Length; i += 4)
		{
			uint num = (uint)((int)inArray[i] << 24 | (int)inArray[i + 1] << 16 | (int)inArray[i + 2] << 8 | (int)inArray[i + 3]);
			char[] array = new char[5];
			uint num2 = 52200625U;
			for (int j = 0; j < 5; j++)
			{
				uint num3 = num / num2 % 85U;
				array[j] = Z85._chars[(int)num3];
				num -= num3 * num2;
				num2 /= 85U;
			}
			stringBuilder.Append(array);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x0003E274 File Offset: 0x0003C474
	public static byte[] FromZ85String(string s)
	{
		int num = s.Length % 5;
		if (num != 0 && (s.Length - 1) % 5 != 0)
		{
			throw new ArgumentException("Invalid length for a Z85 string.");
		}
		int num2 = 0;
		if (num != 0 && (!int.TryParse(s[s.Length - 1].ToString(), out num2) || num2 < 1 || num2 > 3))
		{
			throw new ArgumentException("Invalid padding character for a Z85 string.");
		}
		List<byte> list = new List<byte>(s.Length / 5 * 4);
		int num3 = 0;
		for (int i = 0; i < s.Length - 1; i += 5)
		{
			uint num4 = (uint)Z85._base256[(int)(s[i] - ' ' & '\u007f')];
			num4 = num4 * 85U + (uint)Z85._base256[(int)(s[i + 1] - ' ' & '\u007f')];
			num4 = num4 * 85U + (uint)Z85._base256[(int)(s[i + 2] - ' ' & '\u007f')];
			num4 = num4 * 85U + (uint)Z85._base256[(int)(s[i + 3] - ' ' & '\u007f')];
			num4 = num4 * 85U + (uint)Z85._base256[(int)(s[i + 4] - ' ' & '\u007f')];
			list.Insert(num3++, (byte)(num4 >> 24));
			num4 = num4 << 8 >> 8;
			list.Insert(num3++, (byte)(num4 >> 16));
			num4 = num4 << 16 >> 16;
			list.Insert(num3++, (byte)(num4 >> 8));
			num4 = num4 << 24 >> 24;
			list.Insert(num3++, (byte)num4);
		}
		while (num2-- > 0)
		{
			list.RemoveAt(list.Count - 1);
		}
		return list.ToArray();
	}

	// Token: 0x04000A25 RID: 2597
	private static char[] _chars = new char[]
	{
		'0',
		'1',
		'2',
		'3',
		'4',
		'5',
		'6',
		'7',
		'8',
		'9',
		'a',
		'b',
		'c',
		'd',
		'e',
		'f',
		'g',
		'h',
		'i',
		'j',
		'k',
		'l',
		'm',
		'n',
		'o',
		'p',
		'q',
		'r',
		's',
		't',
		'u',
		'v',
		'w',
		'x',
		'y',
		'z',
		'A',
		'B',
		'C',
		'D',
		'E',
		'F',
		'G',
		'H',
		'I',
		'J',
		'K',
		'L',
		'M',
		'N',
		'O',
		'P',
		'Q',
		'R',
		'S',
		'T',
		'U',
		'V',
		'W',
		'X',
		'Y',
		'Z',
		'.',
		'-',
		':',
		'+',
		'=',
		'^',
		'!',
		'/',
		'*',
		'?',
		'&',
		'<',
		'>',
		'(',
		')',
		'[',
		']',
		'{',
		'}',
		'@',
		'%',
		'$',
		'#'
	};

	// Token: 0x04000A26 RID: 2598
	private static byte[] _base256 = new byte[]
	{
		0,
		68,
		0,
		84,
		83,
		82,
		72,
		0,
		75,
		76,
		70,
		65,
		0,
		63,
		62,
		69,
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		64,
		0,
		73,
		66,
		74,
		71,
		81,
		36,
		37,
		38,
		39,
		40,
		41,
		42,
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		52,
		53,
		54,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		77,
		0,
		78,
		67,
		0,
		0,
		10,
		11,
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19,
		20,
		21,
		22,
		23,
		24,
		25,
		26,
		27,
		28,
		29,
		30,
		31,
		32,
		33,
		34,
		35,
		79,
		0,
		80,
		0,
		0
	};
}