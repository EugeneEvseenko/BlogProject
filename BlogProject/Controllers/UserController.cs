using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogProject.Models.Database.Users;
using BlogProject.Models.ViewModels;
using BlogProject.Models.ViewModels.Users;
using BlogProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userService = userService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #region Randomizer

        private string[] userFirstNames =
            "Abraham, Addison, Adrian, Albert, Alec, Alfred, Alvin, Andrew, Andy, Archibald, Archie, Arlo, Arthur, Arthur, Austen, Barnabe, Bartholomew, Bertram, Bramwell, Byam, Cardew, Chad, Chance, Colin, Coloman, Curtis, Cuthbert, Daniel, Darryl, David, Dickon, Donald, Dougie, Douglas, Earl, Ebenezer, Edgar, Edmund, Edward, Edwin, Elliot, Emil, Floyd, Franklin, Frederick, Gabriel, Galton, Gareth, George, Gerard, Gilbert, Gorden, Gordon, Graham, Grant, Henry, Hervey, Hudson, Hugh, Ian, Jack, Jaime, James, Jason, Jeffrey, Joey, John, Jolyon, Jonas, Joseph, Joshua, Julian, Justin, Kurt, Lanny, Larry, Laurence, Lawton, Lester, Malcolm, Marcus, Mark, Marshall, Martin, Marvin, Matt, Maximilian, Michael, Miles, Murray, Myron, Nate, Nathan, Neil, Nicholas, Nicolas, Norman, Oliver, Oscar, Osric, Owen, Patrick, Paul, Peleg, Philip, Phillipps, Raymond, Reginald, Rhys, Richard, Robert, Roderick, Rodger, Roger, Ronald, Rowland, Rufus, Russell, Sebastian, Shahaf, Simon, Stephen, Swaine, Thomas, Tobias, Travis, Victor, Vincent, Vincent, Vivian, Wayne, Wilfred, William, Winston, Zadoc"
                .Split(',').Select(x => x.Trim()).ToArray();

        private string[] userLastNames =
            "SMITH,BROWN,WILSON,ROBERTSON,CAMPBELL,STEWART,THOMSON,ANDERSON,SCOTT,MACDONALD,REID,MURRAY,CLARK,TAYLOR,ROSS,YOUNG,PATERSON,WATSON,MITCHELL,FRASER,MORRISON,WALKER,MCDONALD,GRAHAM,MILLER,JOHNSTON,HENDERSON,CAMERON,DUNCAN,GRAY,KERR,HAMILTON,HUNTER,DAVIDSON,FERGUSON,BELL,MACKENZIE,MARTIN,SIMPSON,GRANT,ALLAN,KELLY,MACLEOD,BLACK,MACKAY,WALLACE,MCLEAN,KENNEDY,GIBSON,RUSSELL,MARSHALL,GORDON,BURNS,STEVENSON,MILNE,CRAIG,WOOD,WRIGHT,MUNRO,JOHNSTONE,RITCHIE,SINCLAIR,WATT,MCKENZIE,MUIR,MURPHY,SUTHERLAND,MCMILLAN,WHITE,MCKAY,MILLAR,HUGHES,CRAWFORD,WILLIAMSON,DOCHERTY,MACLEAN,FLEMING,CUNNINGHAM,DICKSON,BOYLE,DOUGLAS,MCINTOSH,BRUCE,SHAW,MCGREGOR,LINDSAY,JAMIESON,HAY,CHRISTIE,BOYD,AITKEN,RAE,HILL,MCCALLUM,ALEXANDER,MCINTYRE,CURRIE,RAMSAY,MACKIE,WEIR,JONES,CAIRNS,WHYTE,MCLAUGHLIN,JACKSON,FINDLAY,FORBES,KING,DONALDSON,HUTCHISON,MCCULLOCH,MCLEOD,MCFARLANE,NICOL,BUCHANAN,PATON,MOORE,DUFFY,REILLY,RENNIE,TAIT,IRVINE,O'NEILL,THOMPSON,GREEN,MCEWAN,QUINN,HENDRY,BAIN,BEATTIE,CHALMERS,HALL,HOGG,WILLIAMS,STRACHAN,TURNER,LOGAN,COOK,ARMSTRONG,BUCHAN,COWAN,GALLAGHER,BARCLAY,WELSH,BARR,GALLACHER,GILMOUR,MURDOCH,BLAIR,FORSYTH,ADAMS,COOPER,DONNELLY,DICK,NELSON,O'DONNELL,WARD,JACK,MACPHERSON,DONALD,BAIRD,MCLAREN,PARK,DRUMMOND,INNES,GILLESPIE,LAWSON,DUNN,MAXWELL,MCPHERSON,COLLINS,SPENCE,HIGGINS,ROBERTS,DUFF,MCGOWAN,MCGUIRE,MOFFAT,MORGAN,BAXTER,MACFARLANE,MCBRIDE,MCDOUGALL,INGLIS,RICHARDSON,STEPHEN,SHARP,MACKINNON,MCALLISTER,GILLIES,LAIRD,RODGER,GREIG,LAING,MORTON,SWEENEY,ADAM,MONTGOMERY,MORRIS,TODD,CASSIDY,ROBB,STIRLING,WEBSTER,STUART,KANE,MCKENNA,FALCONER,GIBB,MCNEILL,TURNBULL,HOUSTON,HARVEY,DOWNIE,COCHRANE,LITTLE,MCARTHUR,POLLOCK,MACMILLAN,ORR,STEELE,HARRIS,LYNCH,DUNLOP,LOW,MCMAHON,NICHOLSON,ALLISON,MCCANN"
                .Split(',').Select(x => $"{x[0]}{x.Substring(1).ToLower()}").ToArray();

        private Random _rnd = new Random();
        private string GetRandomName() => userFirstNames[_rnd.Next(0, userFirstNames.Length - 1)];
        private string GetRandomLastName() => userLastNames[_rnd.Next(0, userLastNames.Length - 1)];

        #endregion
        
        /// <summary>
        /// Создание рандомных пользователей определенного количества
        /// </summary>
        /// <param name="count">Количество пользователей</param>
        [HttpPost]
        [Route("generate")]
        [ProducesDefaultResponseType(typeof(GenerateUsersViewModel))]
        public async Task<IActionResult> Generate([FromQuery] int count)
        {
            if (count > 0)
            {
                var listUsers = new Dictionary<string, string>();
                foreach (var i in Enumerable.Range(0, count))
                {
                    var user = new User()
                    {
                        FirstName = GetRandomName(),
                        LastName = GetRandomLastName()
                    };
                    user.Email = $"{user.FirstName.ToLower()}_{user.LastName.ToLower()}@generated.com";
                    user.UserName = user.Email;
                    listUsers.Add($"{user.FirstName} {user.LastName}", user.UserName);
                    await _userManager.CreateAsync(user, $"{user.FirstName}{user.LastName}");
                }
                
                return Ok(new GenerateUsersViewModel()
                {
                    UsersAdded = listUsers.Count,
                    Users = listUsers
                });
            }
            return BadRequest();
        }

        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        [HttpGet]
        [Route("get-all")]
        [ProducesDefaultResponseType(typeof(User[]))]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        [HttpGet]
        [Route("get")]
        [ProducesDefaultResponseType(typeof(User))]
        public async Task<IActionResult> Get([FromQuery] string guid)
        {
            var user = await _userService.GetUser(guid);
            return user != null ? Ok(user) : NotFound();
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        [HttpPost]
        [Route("add")]
        [ProducesDefaultResponseType(typeof(IActionResult))]
        public async Task<IActionResult> Post([FromBody] RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(register);
                var result = await _userManager.CreateAsync(user, register.PasswordReg);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            } 
            return BadRequest();
        }

        /// <summary>
        /// Обновление существующего пользователя
        /// </summary>
        /// <param name="user"></param>
        [HttpPut("update")]
        public async Task Put([FromBody] User user)
        {
            var result = await _userService.UpdateUser(user);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("delete")]
        public async Task Delete([FromQuery] string guid)
        {
            var result = await _userService.DeleteUser(guid);
        }
    }
}
