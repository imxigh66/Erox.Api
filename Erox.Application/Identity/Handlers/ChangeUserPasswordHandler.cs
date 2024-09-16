using Erox.Application.Enums;
using Erox.Application.Identity.Commands;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Aggregates.CardAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Handlers
{
    public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPasswordCommand, OperationResult<bool>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<bool> _result;
        private readonly UserManager<IdentityUser> _userManager;
        public ChangeUserPasswordHandler(DataContext ctx, UserManager<IdentityUser> userManager)
        {
            _ctx = ctx;
            _result = new OperationResult<bool>();
            _userManager = userManager;
        }
        public async Task<OperationResult<bool>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.IdentityId.ToString());
            if (user == null)
            {
                _result.IsError = true;
                _result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessage.NonExistentIdentityUser);
                return _result;
            }

            // Проверяем текущий пароль
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!passwordValid)
            {
                _result.IsError = true;
                _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessage.IncorrectPassword);
                return _result;
            }

            // Меняем пароль
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                _result.IsError = true;
                foreach (var error in changePasswordResult.Errors)
                {
                    _result.AddError(ErrorCode.NotConfirmPassword, error.Description);
                }
                return _result;
            }

            // Возвращаем успешный результат
            _result.IsError = false;
            _result.PayLoad = true;
            return _result;
        }

       
    }
    
}
