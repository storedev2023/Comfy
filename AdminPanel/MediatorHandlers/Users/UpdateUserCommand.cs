using Microsoft.AspNetCore.Identity;
using MediatR;
using AdminPanel.Models.Identity;

namespace AdminPanel.Handlers.Users
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Position { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }


    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                throw new HttpRequestException("User with given id was not found");
            }

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            await _userManager.UpdateAsync(user);

            if (user.UserName != request.UserName)
            {
                await _userManager.UpdateNormalizedUserNameAsync(user);
            }
            if (user.Email != request.Email)
            {
                await _userManager.UpdateNormalizedEmailAsync(user);
            }
            await _userManager.UpdateSecurityStampAsync(user);

            return true;
        }
    }
}
