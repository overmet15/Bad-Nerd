namespace Electrotank.Electroserver5.Api
{
	public class Number
	{
		private double value;

		public Number(double value)
		{
			setValue(value);
		}

		public Number()
		{
		}

		public double getValue()
		{
			return value;
		}

		public void setValue(double value)
		{
			this.value = value;
		}
	}
}
