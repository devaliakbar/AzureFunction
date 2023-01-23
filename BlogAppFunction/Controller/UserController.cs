// using BlogAppFunction.Data;
// using Microsoft.Azure.WebJobs;

// namespace BlogAppFunction.Controller
// {
//     public class UserController
//     {
//         private readonly DataContext _context;

//         public UserController(DataContext context)
//         {
//             _context = context;
//         }

//         [FunctionName("GetBlogs")]
//         public async Task<ActionResult<UserDTO>> Signup(SignUpDTO signUpDTO)
//         {
//             if (await _userRepository.GetUser(signUpDTO.UserName) != null)
//             {
//                 return BadRequest("User already taken");
//             }

//             User user = await _userRepository.CreateUser(signUpDTO);

//             return StatusCode(((int)HttpStatusCode.Created), new SignInResponseDTO
//             {
//                 Id = user.Id,
//                 UserName = user.UserName,
//                 Token = _tokenService.CreateToken(user)
//             });
//         }
//     }
// }