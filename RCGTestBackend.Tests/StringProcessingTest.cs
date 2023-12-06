using Microsoft.Extensions.DependencyInjection;
using RCGTestBackend.Domain.Interfaces.Services;

namespace RCGTestBackend.Tests
{
    [TestFixture]
	public class StringProcessingTest : BaseTest
	{
		IStringProcessingService StringProcessingService;

        [SetUp]
		public void SetUp()
		{
			StringProcessingService = ServiceProvider.GetRequiredService<IStringProcessingService>();
		}

		public static IEnumerable<TestCaseData> EncodeStringToBase64Cases()
		{
			yield return new TestCaseData("Hello world!", "SGVsbG8gd29ybGQh");
            yield return new TestCaseData("This is a test.", "VGhpcyBpcyBhIHRlc3Qu");
			yield return new TestCaseData("I hope this works.", "SSBob3BlIHRoaXMgd29ya3Mu");
        }

		[Test, TestCaseSource(nameof(EncodeStringToBase64Cases))]
		public void Encode_String_To_Base64(string plainValue, string encodedValue)
		{
			var encodedString = StringProcessingService.EncodeToBase64(plainValue);

			Assert.That(encodedString, Is.EqualTo(encodedValue));
		}
	}
}

