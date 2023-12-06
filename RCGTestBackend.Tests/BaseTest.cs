using Microsoft.Extensions.DependencyInjection;
using RCGTestBackend.Application.Services;
using RCGTestBackend.Domain.Interfaces.Services;

namespace RCGTestBackend.Tests
{
	[TestFixture]
    public abstract class BaseTest
	{
		protected IServiceProvider ServiceProvider;

		[SetUp]
		public void BaseSetUp()
		{
			var services = new ServiceCollection();

			services.AddSingleton<IStringProcessingService, StringProcessingService>();

			ServiceProvider = services.BuildServiceProvider();
		}

		[TearDown]
		public void BaseTearDown()
		{
			if (ServiceProvider is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}
}

