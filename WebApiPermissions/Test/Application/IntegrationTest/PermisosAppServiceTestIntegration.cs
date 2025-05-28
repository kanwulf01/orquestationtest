using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using Application.Permisos.Commands.CreatePermisos;
using Application.Permisos.Commands.UpdatePermisos;
using Application.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application.IntegrationTest
{
    public class PermisosAppServiceTestIntegration
    {
        private readonly ServiceProvider _sp;
        private readonly ApplicationDbContext _db;
        private readonly Mock<IKafkaServices> _kafkaMock;
        private readonly Mock<IElasticService> _elasticMock;
        private readonly IPermisosAppService _service;

        public PermisosAppServiceTestIntegration()
        {
            var sc = new ServiceCollection();

            sc.AddDbContext<ApplicationDbContext>( static opt => opt.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}"));

            sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePermisosCommand).Assembly));

            sc.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            //Mocks

            _kafkaMock = new Mock<IKafkaServices>();
            _elasticMock = new Mock<IElasticService>();

            sc.AddSingleton(_kafkaMock.Object);
            sc.AddSingleton(_elasticMock.Object);

            sc.AddAutoMapper(typeof(PermisosProfile).Assembly);

            sc.AddScoped<IPermisosAppService, PermisosAppService>();

            sc.AddScoped<IPermisosRepository, PermisoRepository>();
            sc.AddScoped<ITipoPermisosRepository, TipoPermisoRepository>();
            sc.AddScoped<IUnitOfWork, UnitOfWork>();
            _sp = sc.BuildServiceProvider();

            _db = _sp.GetRequiredService<ApplicationDbContext>();
            _service = _sp.GetRequiredService<IPermisosAppService>();

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db.Database.EnsureDeleted();
            _db.Dispose();
        }

        [Fact]
        public async Task CreatePermiso_Integratino_WritesIntoDb_And_InvkeKafkaElastic()
        {
            //Arrange

            var cmd = new CreatePermisosCommand
            {
                ApellidoEmpleado = "Lopez",
                NombreEmpleado = "cARLOS",
                TipoPermisoId = 5,
                FechaPermiso = new DateTime(2025, 06, 15)
            };

            //Act

            var result = await _service.CreatePermiso(cmd, CancellationToken.None);

            //Assert
            Assert.True(result.Id > 0);

            //Assert
            var entidad = _db.Permisos.Find(result.Id);
            Assert.NotNull(entidad);
            Assert.Equal("cARLOS", entidad.NombreEmpleado);


            _kafkaMock.Verify(x => x.ProduceAsync("permisos", It.Is<object>(
                o => o.GetType().GetProperty("NameOperation").GetValue(o).ToString() == "REQUEST"))
                , Times.Once);

            _elasticMock.Verify(x => x.EnsureIndexExistAsync(), Times.Once);
            _elasticMock.Verify(x => x.IndexMessageCreatePermisosAsync(
                It.Is<PermisosPostDtos>(p => p.Id == result.Id && p.NombreEmpleado == cmd.NombreEmpleado)), Times.Once);
        }

        [Fact]
        public async Task GetAllPermisos_Integration_ReturnsPersistedEntites()
        {
            var listTipoPermiso = new List<TipoPermiso>()
            {
                new TipoPermiso { Descripcion = "Vaciones" },
                new TipoPermiso { Descripcion = "Permiso medico" }
            };

            await _db.TipoPermisos.AddRangeAsync(listTipoPermiso);
            await _db.SaveChangesAsync();

            //Arrange

            var cmd = new CreatePermisosCommand
            {
                NombreEmpleado = "A",
                ApellidoEmpleado = "X",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Today
            };

            
            var result = await _service.CreatePermiso(cmd, CancellationToken.None);
            //Act
            var list = await _service.GetlllPermisos(CancellationToken.None);
            //Assert

            Assert.Contains(list, dto => dto.NombreEmpleado == "A");

            _kafkaMock.Verify(x => x.ProduceAsync("permisos", It.Is<object>(
                o => o.GetType().GetProperty("NameOperation").GetValue(o).ToString() == "GET"))
                , Times.Once);
        }

        [Fact]
        public async Task UpdatePermiso_Integration_UpdateDatabase_And_InvokeKafka()
        {
            //Arrange

            var listTipoPermiso = new List<TipoPermiso>()
            {
                new TipoPermiso { Descripcion = "Vaciones" },
                new TipoPermiso { Descripcion = "Permiso medico" }
            };

            await _db.TipoPermisos.AddRangeAsync(listTipoPermiso);
            await _db.SaveChangesAsync();

            var cmd = new CreatePermisosCommand
            {
                NombreEmpleado = "A",
                ApellidoEmpleado = "X",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Today
            };
            
            // Act

            var result = await _service.CreatePermiso(cmd, CancellationToken.None);
            var list = await _service.GetlllPermisos(CancellationToken.None);
            var updateCmd = new UpdatePermisosCommand
            {
                Id = result.Id,
                NombreEmpleado = "Modificado",
                ApellidoEmpleado = cmd.ApellidoEmpleado,
                TipoPermisoId = 1,
                FechaPermiso = cmd.FechaPermiso,
            };

            //Act

            await _service.UpdatePermiso(updateCmd, CancellationToken.None);

            // Assert
            var db2 = await _db.Permisos.FindAsync(result.Id);
            Assert.Equal("Modificado", db2!.NombreEmpleado);

            _kafkaMock.Verify(x => x.ProduceAsync("permisos", It.Is<object>(
                o => o.GetType().GetProperty("NameOperation").GetValue(o).ToString() == "UPDATE"))
                , Times.Once);

        }
    }
}
