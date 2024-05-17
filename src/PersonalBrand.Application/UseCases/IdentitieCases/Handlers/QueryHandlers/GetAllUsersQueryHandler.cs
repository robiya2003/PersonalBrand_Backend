using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;
using PersonalBrand.Application.UseCases.IdentitieCases.Queries;
using PersonalBrand.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBrand.Application.UseCases.IdentitieCases.Handlers.QueryHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<UserModel> _userManager;

        public GetAllUsersQueryHandler(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            object cacheXotira = _memoryCache.Get("getallusers");
            
            if(cacheXotira == null)
            {
                List<UserModel> userModels = _userManager.Users.ToList();
                _memoryCache.Set("getallusers", userModels, options: new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(5),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                    Size = 1024
                });
                
               
                
                return userModels;
                
            }
            
            
            
           

            return cacheXotira as List<UserModel>;
        }
    }
}
