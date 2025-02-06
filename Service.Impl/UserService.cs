using Repository.Contract;
using Repository.Contract.Models;
using Service.Contract;

namespace Service.Impl;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    public UserService(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }
    
    public async Task<User> RegisterAsync(string username, string password)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(username);
        if (existingUser != null) throw new Exception("User already exists");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User { Username = username, PasswordHash = hashedPassword };
        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Invalid credentials");
        
        return _authService.GenerateJwtToken(user);
    }
}