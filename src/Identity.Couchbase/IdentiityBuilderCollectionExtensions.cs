﻿using Identity.Couchbase.Stores;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Identity.Couchbase
{
    public static class IdentiityBuilderCollectionExtensions
    {
        public static IdentityBuilder AddCouchbaseStores<TUser, TRole>(this IdentityBuilder builder)
            where TUser : IUser
            where TRole : IRole
        {
            builder.Services.AddSingleton<UserStore<TUser, TRole>>();
            builder.Services.AddSingleton<IUserStore<TUser>>(p => p.GetService<UserStore<TUser, TRole>>());
            builder.Services.AddSingleton<IRoleStore<TRole>, RoleStore<TRole>>();
            builder.Services.AddSingleton<ILookupNormalizer, LookupNormalizer>();

            return builder;
        }

        public static IdentityBuilder AddCouchbaseSessionStores(this IdentityBuilder builder)
        {
            builder.Services.TryAddSingleton<ITicketStore, SessionStore>();
            return builder;
        }
    }
}
