using arc2d.Services;
using System;
using System.Threading.Tasks;

namespace arc2d.Runner
{
	class Program
	{
		static void Main(string[] args)
		{
			MainAsync(args).Wait();

			Console.WriteLine("Done.");

			if (!AppSettings.IsProduction)
				Console.ReadKey();
		}

		static async Task MainAsync(string[] args)
		{
			await TestMailer.SendTestMessage();

			//Console.WriteLine(result);

			
		}
	}
}
