using NUnit.Framework;
using System.Net;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

 

        [Test]
        public void TestPatientCreation()
        {
            WebClient wc = new WebClient();
            string output = wc.DownloadString("http://localhost/PatientServices/Procedure/?PatientID=1");
            if (string.IsNullOrEmpty(output))
            {
                Assert.Fail();
            }
            else {
                Assert.Pass();
            }
            
        }
    }
}