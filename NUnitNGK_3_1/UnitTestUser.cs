using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System.Text.Json;
using static BCrypt.Net.BCrypt;
using NGK_aflevering_3_1;


namespace NGK3_UnitTest
{
    public class UserUnitTest
    {
        private AccountController _uut;

        //[SetUp]
        //public void Setup()
        //{

        //}

        [Test]
        public void LoginFails_UserDoesNotExist()
        {
            //Arrange
            var request = new UserDto()
            {
                Email = "BadEmail@nederen.dk",
                Password = "123"
            };

            //Act
            var result = _uut.Login(request).Result;

            //Assert
            var expected = new { message = "Username or password is incorrect" };


            var jsonActual = JsonSerializer.Serialize(result.Value);
            var jsonExpected = JsonSerializer.Serialize(expected);

            Assert.That(expected, Is.EqualTo(result));

        }

        //        [Test]
        //        public void LoginFails_WrongPassword()
        //        {
        //            //Arrange
        //            var request = new UserDTO()
        //            {
        //                Username = "SebastianVejrmand",
        //                Password = "DettePasswordFindesIkke"
        //            };

        //            var account = new User
        //            {
        //                Id = 3,
        //                Username = "SebastianVejrmand",
        //                Password = HashPassword("EtGodtPassword")
        //            };
        //            _unit.UserRepository.Get("SebastianVejrmand").ReturnsNull();
        //            //Act
        //            var result = _uut.Authenticate(request).Result as ObjectResult;

        //            //Assert
        //            var expected = new { message = "Username or password is incorrect" };
        //            var jsonActual = JsonSerializer.Serialize(result.Value);
        //            var jsonExpected = JsonSerializer.Serialize(expected);

        //            Assert.That(jsonActual, Is.EqualTo(jsonExpected));


        //        }

        //        [Test]
        //        public void Register_Fails_UserAlreadyExists()
        //        {
        //            //Arrange
        //            var request = new UserDTO()
        //            {
        //                Username = "Bente",
        //                Password = "4321"
        //            };

        //            var account = new User
        //            {
        //                Id = 3,
        //                Username = "Bente",
        //                Password = HashPassword("4321")
        //            };

        //            _unit.UserRepository.Get("Bente").Returns(account);

        //            //Act
        //            var result = _uut.Register(request).Result as ObjectResult;

        //            //Assert
        //            var expected = new { message = "User already exists" };
        //            var jsonActual = JsonSerializer.Serialize(result.Value);
        //            var jsonExpected = JsonSerializer.Serialize(expected);

        //            Assert.That(jsonActual, Is.EqualTo(jsonExpected));
    }
}
//}