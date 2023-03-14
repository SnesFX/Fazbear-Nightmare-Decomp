using System.Globalization;

[NodeToolTip("Converts an integer to a string with advanced formatting options.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodePath("Actions/Variables/Int")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Int_To_String")]
[FriendlyName("Int To String", "Converts an integer to a string with advanced formatting options.")]
public class uScriptAct_IntToString : uScriptLogic
{
	public enum FormatType
	{
		General = 0,
		Currency = 1,
		Decimal = 2,
		Exponential = 3,
		FixedPoint = 4,
		Number = 5,
		Percent = 6,
		Hexadecimal = 7
	}

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The integer you wish to convert to a string.")] int Target, [DefaultValue(FormatType.General)][SocketState(false, false)][FriendlyName("Standard Format", "Standard numeric formatting string.\n\nThe following results will be generated when the Target value is equal to 125 and the Invariant Culture is used:\n\n\tGeneral:\t\t\t125\n\tCurrency:\t\t$125.00\n\tDecimal:\t\t\t125\n\tExponential:\t1.250000E+002\n\tFixedPoint:\t\t125.00\n\tNumber:\t\t\t125.00\n\tPercent:\t\t\t12500.00 %\n\tHexidecimal:\t7D")] FormatType StandardFormat, [SocketState(false, false)][DefaultValue("")][FriendlyName("Custom Format", "An optional custom numeric format string.  If none is specified, the chosen Standard Format will be used instead.\n\nThe following results will be generated when the Target value is equal to 125 and the Invariant Culture is used:\n\n\t#.####:\t\t\t125\n\t0.####:\t\t\t125\n\t0.0000:\t\t\t125.0000\n\t0000.0000:\t\t0125.0000\n\tC5:\t\t\t\t$125.00000")] string CustomFormat, [DefaultValue("")][FriendlyName("Custom Culture", "An optional custom culture string.  If none is specified, the Invariant Culture will be used.\n\nThe following results will be generated when the Target value is equal to 125 and the custom culture is set to \"sv-SE\".  Note the use of ',' instead of '.' for the decimal separator and the currency symbol for Swedish Krona:\n\n\tGeneral:\t\t\t125\n\tCurrency:\t\t125,00 kr")][SocketState(false, false)] string CustomCulture, [FriendlyName("Result", "The string representation of the Target value as specified by format and culture.")] out string Result)
	{
		string text = CustomFormat;
		CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(CustomCulture);
		if (string.IsNullOrEmpty(text))
		{
			switch (StandardFormat)
			{
			case FormatType.Currency:
				text = "C";
				break;
			case FormatType.Decimal:
				text = "D";
				break;
			case FormatType.Exponential:
				text = "E";
				break;
			case FormatType.FixedPoint:
				text = "F";
				break;
			case FormatType.Number:
				text = "N";
				break;
			case FormatType.Percent:
				text = "P";
				break;
			case FormatType.Hexadecimal:
				text = "X";
				break;
			default:
				text = "G";
				break;
			}
		}
		Result = Target.ToString(text, cultureInfo);
	}
}
