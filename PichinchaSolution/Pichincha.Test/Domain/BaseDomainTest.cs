namespace Pichincha.Test.Domain
{
    using AutoMapper;
    using Pichincha.Domain.Common;
    using Pichincha.Domain.Entities;
    using Pichincha.Infrastructure.DataAccess.Interfaces;
    using Pichincha.Infrastructure.Models;
    using Moq;
    using NUnit.Framework;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class BaseDomainTest
    {
        /// <summary>
        /// The baseDomain.
        /// </summary>
        private BaseDomain<Client, ClientDto> baseDomain;

        /// <summary>
        /// The mappernMock.
        /// </summary>
        private Mock<IMapper> mappernMock;

        /// <summary>
        /// The sqlDataMock.
        /// </summary>
        private Mock<IRepository<Client>> sqlDataMock;

        [SetUp]
        public void Setup()
        {
            mappernMock = new Mock<IMapper>();
            sqlDataMock = new Mock<IRepository<Client>>();

            baseDomain = new(mappernMock.Object, sqlDataMock.Object);
        }

        [Test]
        public async Task GetAllTestAsync()
        {
            sqlDataMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new[] { new Client { ClientId = 1 } });

            var result = await baseDomain.GetAllAsync();

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetByIdTestAsync()
        {
            sqlDataMock.Setup(x => x.GetByIdAsync(It.IsAny<object>())).ReturnsAsync(new Client { ClientId = 1 });

            var result = await baseDomain.GetByIdAsync(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task InsertTestAsync()
        {
            sqlDataMock.Setup(x => x.GetByIdAsync(It.IsAny<object>())).ReturnsAsync(new Client { ClientId = 1 });
            ClientDto ownerDto = new(1, "123", true, 1);

            var result = await baseDomain.InsertAsync(ownerDto);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTestAsync()
        {
            sqlDataMock.Setup(x => x.SaveAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            ClientDto ownerDto = new(1, "123", true, 1);

            var result = await baseDomain.UpdateAsync(ownerDto);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DeleteGetByIdTestAsync()
        {
            sqlDataMock.Setup(x => x.GetByIdAsync(It.IsAny<object>())).ReturnsAsync(new Client { ClientId = 1 });
            sqlDataMock.Setup(x => x.SaveAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            ClientDto ownerDto = new(1, "123", true, 1);

            var result = await baseDomain.DeleteGetByIdAsync(1);

            Assert.IsNotNull(result);
        }
    }
}
