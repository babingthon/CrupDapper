using AutoMapper;
using CrudDapper.Dto;
using CrudDapper.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CrudDapper.Services;

public class UsuarioService : IUsuarioInterface
{
    string ConexaoPadrao = "DefaultConnection";
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public UsuarioService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }
    public async Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuario()
    {
        ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString(ConexaoPadrao)))
        {

            var usuariosBanco = await connection.QueryAsync<Usuario>("select * from Usuario");

            if (usuariosBanco.Count() == 0)
            {
                response.Mensagem = "Nenhum usuario encontrado";
                response.Status = false;
                return response;
            }

            var usuariosMapeado = _mapper.Map<List<UsuarioListarDto>>(usuariosBanco);

            response.Dados = usuariosMapeado;
            response.Mensagem = "Usuarios Localizados com sucesso!"; 

        }

        return response;
    }

    public async Task<ResponseModel<UsuarioListarDto>> BuscarUsuarioPorId(int usuarioId)
    {
        ResponseModel<UsuarioListarDto> response = new ResponseModel<UsuarioListarDto>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString(ConexaoPadrao)))
        {
            var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from usuario where Id = @Id", new {Id = usuarioId });

            if (usuarioBanco == null)
            {
                response.Mensagem = "Nenhum usuario encontrado";
                response.Status=false;
                return response;
            }

            var usuarioMapeado = _mapper.Map<UsuarioListarDto>(usuarioBanco);
            response.Dados =usuarioMapeado;
            response.Mensagem = "Usuario Localizado com sucesso!";
        }

        return response;
    }

    public async Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
    {
        ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString(ConexaoPadrao)))
        {

            var usuariosBanco = await connection.ExecuteAsync("insert into Usuario (NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) " +
                                                                "values (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", usuarioCriarDto);

            if (usuariosBanco == 0)
            {
                response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                response.Status = false;
                return response;
            }

            var usuarios = await ListarUsuarios(connection);

            var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

            response.Dados = usuariosMapeados;
            response.Mensagem = "Usuários listados com sucesso!";
        }

        return response;
    }

    private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
    {
        return await connection.QueryAsync<Usuario>("select * from Usuario");
    }

    public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
    {
        ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString(ConexaoPadrao)))
        {

            var usuariosBanco = await connection.ExecuteAsync("update Usuario set NomeCompleto = @NomeCompleto," +
                "                                                                  Email = @Email, Cargo = @Cargo, Salario = @Salario," +
                "                                                                  Situacao = @Situacao, CPF = @CPF where Id = @Id ", usuarioEditarDto);


            if (usuariosBanco == 0)
            {
                response.Mensagem = "Ocorreu um erro ao realizar a edição!";
                response.Status = false;
                return response;
            }


            var usuarios = await ListarUsuarios(connection);

            var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

            response.Dados = usuariosMapeados;
            response.Mensagem = "Usuários listados com sucesso!";

        }

        return response;
    }

    public async Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId)
    {
        ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString(ConexaoPadrao)))
        {
            var usuariosBanco = await connection.ExecuteAsync("delete from Usuario where id = @Id", new { Id = usuarioId });

            if (usuariosBanco == 0)
            {
                response.Mensagem = "Ocorreu um erro ao realizar a edição!";
                response.Status = false;
                return response;
            }

            var usuarios = await ListarUsuarios(connection);

            var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

            response.Dados = usuariosMapeados;
            response.Mensagem = "Usuários Listados com sucesso";
        }

        return response;
    }
}


