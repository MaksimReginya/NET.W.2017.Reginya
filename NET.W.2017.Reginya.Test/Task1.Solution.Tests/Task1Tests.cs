using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Task1.Solution.Tests
{
    [TestFixture]
    public class Task1Tests
    {
        [TestCase(false, "")]
        [TestCase(false, "abc")]
        [TestCase(false, "abcabcabcabcabcabc")]
        [TestCase(false, "awdwadawdawd")]
        [TestCase(true, "qwerty123")]
        [TestCase(true, "AbCd9876AbCd")]
        public void PasswordCheckerService_VerifyPasswordTest(bool expected, string password)
        {
            var repositoryMock = new Mock<IRepository>();

            var verificationMock = new Mock<IVerification>(MockBehavior.Strict);            
            verificationMock.Setup(verification => verification.VerifyPassword(It.IsAny<string>()))
                .Returns(new Tuple<bool, string>(expected, string.Empty));

            var verifierMock = new Mock<IVerifier>(MockBehavior.Strict);
            verifierMock.Setup(verifier => verifier.GetEnumerator())
                .Returns(new List<IVerification>{ verificationMock.Object }.GetEnumerator());            

            var checkerService = new PasswordCheckerService(repositoryMock.Object);
            var actual = checkerService.VerifyPassword(password, verifierMock.Object).Item1;            
            Assert.AreEqual(expected, actual);
        }
    }
}
