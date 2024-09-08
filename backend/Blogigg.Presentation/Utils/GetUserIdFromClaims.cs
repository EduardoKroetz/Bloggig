using System.Security.Claims;

namespace Bloggig.Presentation.Utils;

public static partial class Utils
{
    public static Guid GetUserIdFromClaim(ClaimsPrincipal User)
    {
        return new Guid
       (
           User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new System.Exception("Não foi possível obter o id do usuário, faça login novamente")
       );
    }
}
