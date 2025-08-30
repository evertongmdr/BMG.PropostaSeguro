using BMG.Core.Communication;
using BMG.Core.Notifications;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BMG.WebAPI.Core.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected readonly NotificationContext _notificationContext;

        public MainController(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            var modelState = new ModelStateDictionary();
            foreach (var notification in _notificationContext.Notifications)
            {
                modelState.AddModelError("Mensagens", notification.Message);
            }

            return BadRequest(new ValidationProblemDetails(modelState)
            {
                Title = "Ocorreram erros na validação",
                Status = StatusCodes.Status400BadRequest
            });
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult resposta)
        {
            ResponsePossuiErros(resposta);

            return CustomResponse();
        }

        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta == null || !resposta.Errors.Mensagens.Any()) return false;

            foreach (var mensagem in resposta.Errors.Mensagens)
            {
                AdicionarErroProcessamento(mensagem);
            }

            return true;
        }

        protected bool OperacaoValida()
        {
            return !_notificationContext.ExistNotifications;
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            _notificationContext.AddNotification(erro);
        }
    }
}
