using System.Globalization;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Float To String", "Converts a float to a string with advanced formatting options.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Float_To_String")]
[NodeToolTip("Converts a float to a string with advanced formatting options.")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodePath("Actions/Variables/Float")]
public class uScriptAct_FloatToString : uScriptLogic
{
	public enum FormatType
	{
		General = 0,
		Currency = 1,
		Exponential = 2,
		FixedPoint = 3,
		Number = 4,
		Percent = 5,
		RoundTrip = 6
	}

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The float you wish to convert to a string.")] float Target, [DefaultValue(FormatType.General)][SocketState(false, false)][FriendlyName("Standard Format", "Standard numeric formatting string.\n\nThe following results will be generated when the Target value is equal to 0.12345 and the Invariant Culture is used:\n\n\tGeneral:\t\t\t0.12345\n\tCurrency:\t\t$0.12\n\tExponential:\t1.234500E-001\n\tFixedPoint:\t\t0.12\n\tNumber:\t\t\t0.12\n\tPercent:\t\t\t12.35 %\n\tRoundTrip:\t\t0.12345")] FormatType StandardFormat, [FriendlyName("Custom Format", "An optional custom numeric format string.  If none is specified, the chosen Standard Format will be used instead.\n\nThe following results will be generated when the Target value is equal to 0.123 and the Invariant Culture is used:\n\n\t#.####:\t\t\t.123\n\t0.####:\t\t\t0.123\n\t0.0000:\t\t\t0.1230\n\t0000.0000:\t\t0000.1230\n\tC5:\t\t\t\t$0.12300")][DefaultValue("")][SocketState(false, false)] string CustomFormat, [DefaultValue("")][SocketState(false, false)][FriendlyName("Custom Culture", "An optional custom culture string.  If none is specified, the Invariant Culture will be used.\n\nThe following results will be generated when the Target value is equal to 0.12345 and the custom culture is set to \"sv-SE\".  Note the use of ',' instead of '.' for the decimal separator and the currency symbol for Swedish Krona:\n\n\tGeneral:\t\t\t0,12345\n\tCurrency:\t\t0,12 kr")] string CustomCulture, [FriendlyName("Result", "The string representation of the Target value as specified by format and culture.")] out string Result)
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
			case FormatType.RoundTrip:
				text = "R";
				break;
			default:
				text = "G";
				break;
			}
		}
		Result = Target.ToString(text, cultureInfo);
	}
}
