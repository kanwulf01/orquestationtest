using Application.DTOs;
using Application.Interfaces;
using Application.Permisos.Commands.CreatePermisos;
using Application.Permisos.Commands.UpdatePermisos;
using Application.Permisos.Queries.GetAllProducts;
using Application.Services;
using Infrastructure.Elastic;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application.UnitTest
{
    public class PermisosAppServiceTestUnit
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IKafkaServices> _mockKafkaService;
        private readonly Mock<ElasticService> _mockElasticService;
        private readonly PermisosAppService _permisosAppService;

        public PermisosAppServiceTestUnit(Mock<IMediator> mockMediator, Mock<IKafkaServices> mockKafkaService, Mock<ElasticService> mockElasticService, PermisosAppService permisosAppService)
        {
            _mockMediator = mockMediator;
            _mockKafkaService = mockKafkaService;
            _mockElasticService = mockElasticService;
            _permisosAppService = permisosAppService;
        }   

        [Fact]
        public async Task CreatePermiso_ShouldSendCommandAndProducerMessageAndIndexToElastic()
        {
            //Arrange
            var command = new CreatePermisosCommand
            {
                NombreEmpleado = "Juan",
                ApellidoEmpleado = "Perez",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.UtcNow,
            };

            var cancellationToken = new CancellationToken();
            var expectedPermisosDtos = new PermisosDto
            {
                Id = 1,
                NombreEmpleado = command.NombreEmpleado,
                ApellidoEmpleado = command.ApellidoEmpleado,
                TipoPermisoId = command.TipoPermisoId,
                FechaPermiso = command.FechaPermiso,
                TipoPermiso = null
            };

            _mockMediator.Setup(m => m.Send(command, cancellationToken))
            .ReturnsAsync(expectedPermisosDtos);

            _mockKafkaService.Setup(k => k.ProduceAsync(It.Is<string>(s => s == "permisos"), It.IsAny<object>()));

            _mockElasticService.Setup(e => e.EnsureIndexExistAsync());

            var result = await _permisosAppService.CreatePermiso(command, cancellationToken);

            // Assert

            Assert.NotNull(result);
            Assert.Equal(expectedPermisosDtos.Id, result.Id);
            Assert.Equal(expectedPermisosDtos.NombreEmpleado, result.NombreEmpleado);

            _mockMediator.Verify(e => e.Send(command, cancellationToken), Times.Once);
            _mockKafkaService.Verify(k=>k.ProduceAsync("permisos", It.Is<object>(o => 
                o.GetType().GetProperty("NameOperation") != null &&
                o.GetType().GetProperty("NameOperation").GetValue(o).ToString() == "REQUEST"
                )), Times.Once);    
        }

        [Fact]
        public async Task UpdatePermiso_ShouldSendCommandAndProduceMessage()
        {
            //Arragne

            var command = new UpdatePermisosCommand
            {
                Id = 1,
                NombreEmpleado = "Juan Actualizado",
                ApellidoEmpleado = "Perez Actualizado",
                TipoPermisoId = 2,
                FechaPermiso = DateTime.UtcNow.AddDays(1),
            };
            var cancellationToken = new CancellationToken();
            var expectedPermisosDtos = new PermisosDto
            {
                Id = 1,
                NombreEmpleado = command.NombreEmpleado,
                ApellidoEmpleado = command.ApellidoEmpleado,
                TipoPermisoId = command.TipoPermisoId,
                FechaPermiso = command.FechaPermiso,
                TipoPermiso = null
            };

            _mockMediator.Setup(m => m.Send(command, cancellationToken))
            .ReturnsAsync(expectedPermisosDtos);

            _mockKafkaService.Setup(k => k.ProduceAsync(It.Is<string>(s => s == "permisos"), It.IsAny<object>()));

            _mockElasticService.Setup(e => e.EnsureIndexExistAsync());

            //Act
            var result = await _permisosAppService.UpdatePermiso(command, cancellationToken);

            //Assret

            Assert.NotNull(result);
            Assert.Equal(expectedPermisosDtos.Id, result.Id);
            Assert.Equal(command.NombreEmpleado, result.NombreEmpleado);

            _mockMediator.Verify(e => e.Send(command, cancellationToken), Times.Once);
            _mockKafkaService.Verify(k => k.ProduceAsync("permisos", It.Is<object>(o =>
                o.GetType().GetProperty("NameOperation") != null &&
                o.GetType().GetProperty("NameOperation").GetValue(o).ToString() == "UPDATE"
                )), Times.Once);
        }

        [Fact]
        public async Task GetAllPermisos_InvokesProducerAndMediator()
        {
            //arrnge

            var expected = new List<PermisosDto> { new PermisosDto { Id = 100, NombreEmpleado = "Kamila", ApellidoEmpleado = "Linder",
            TipoPermisoId = 1, FechaPermiso = DateTime.UtcNow.AddDays(1), TipoPermiso = null} };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllPermisosQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

            //Act

            var result = await _permisosAppService.GetlllPermisos(CancellationToken.None);

            //Assert
            Assert.Same(expected, result);
            _mockKafkaService.Verify(k => k.ProduceAsync("permisos", It.Is<object>(o =>
                o.GetType().GetProperty("NameOperation") != null &&
                o.GetType().GetProperty("NameOperation").GetValue(o).ToString() == "GET"
                )), Times.Once);
        }
    }
}
