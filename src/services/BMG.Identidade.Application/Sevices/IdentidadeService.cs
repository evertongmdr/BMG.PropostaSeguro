using AutoMapper;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using BMG.Identidade.Application.Interfaces;
using BMG.Identidade.Domain.DTOs;
using BMG.Identidade.Domain.Entities;
using BMG.Identidade.Domain.Interfaces.Repositories;

namespace BMG.Identidade.Application.Sevices
{
    public class IdentidadeService : ErrorNotifier, IIdentidadeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public IdentidadeService(NotificationContext notificationContext, IMapper mapper, IUserRepository userRepository) : base(notificationContext)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            return await _userRepository.ObterPorIdAsync(id);
        }

        public async Task<PagedResult<Usuario>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters)
        {
            return await _userRepository.ObterUsuariosAsync(usuarioQueryParameters);
        }

        public async Task<Guid> RegistrarUsuario(RegistrarUsuarioDTO registrarUsuario)
        {

            var usuarioExistente = await _userRepository.ObterPorLoginAsync(registrarUsuario.Login);

            if (usuarioExistente != null)
            {
                _notificationContext.AddNotification($"Já existe um usuário cadastrado com este e-email.");
                return Guid.Empty;
            }

            var usuario = _mapper.Map<Usuario>(registrarUsuario);

            _userRepository.Adicionar(usuario);

            await PersistirDados(_userRepository.UnitOfWork);

            return usuario.Id;
        }
    }
}
