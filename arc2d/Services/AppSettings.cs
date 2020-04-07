using System;

namespace arc2d.Services
{
	public static class AppSettings
	{
		private static bool _isProductionIsSet = false;
		private static bool _isProductionValue = false;
		public static bool IsProduction
		{
			get
			{
				if (!_isProductionIsSet)
				{
					string isProductionVal = Environment.GetEnvironmentVariable("IS_PRODUCTION");

					if (isProductionVal == null)
						throw new ArgumentNullException("IS_PRODUCTION environment variable is missing.");

					_isProductionValue = Convert.ToBoolean(isProductionVal);
					_isProductionIsSet = true;
				}
				return _isProductionValue;
			}

			set
			{
				_isProductionValue = value;
				_isProductionIsSet = true;
			}
		}


		/***  MAILGUN ***/

		private static string _mailgunFromDomain;
		public static string MailgunFromDomain
		{
			get
			{
				if (_mailgunFromDomain == null)
				{
					_mailgunFromDomain = Environment.GetEnvironmentVariable("MAILGUN_FROM_DOMAIN");

					if (_mailgunFromDomain == null)
						throw new ArgumentNullException("MAILGUN_FROM_DOMAIN environment variable is missing.");
				}
				return _mailgunFromDomain;
			}
		}

		private static string _mailgunAuthValue;
		public static string MailgunAuthValue
		{
			get
			{
				if (_mailgunAuthValue == null)
				{
					_mailgunAuthValue = Environment.GetEnvironmentVariable("MAILGUN_AUTH_VALUE");

					if (_mailgunAuthValue == null)
						throw new ArgumentNullException("MAILGUN_AUTH_VALUE environment variable is missing.");
				}
				return _mailgunAuthValue;
			}
		}
	}
}
