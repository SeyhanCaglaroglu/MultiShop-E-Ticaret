// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {   //ResourceCatalog ismine CatalogFullPermission  yetkisi ekledik

            new ApiResource("ResourceCatalog") {Scopes={"CatalogFullPermission","CatalogReadPermission"}},

            new ApiResource("ResourceDiscount") {Scopes ={"DiscountFullPermission"}},

            new ApiResource("ResourceOrder") {Scopes = {"OrderFullPermission"}},

            new ApiResource("ResourceCargo") {Scopes = {"CargoFullPermission"}},

            new ApiResource("ResourceBasket") {Scopes = {"BasketFullPermission"}},

            new ApiResource("ResourceComment") {Scopes = {"CommentFullPermission"}},

            new ApiResource("ResourcePayment") {Scopes = {"PaymentFullPermission"}},

            new ApiResource("ResourceImage") {Scopes = {"ImageFullPermission"}},

            new ApiResource("ResourceMessage") {Scopes = {"MessageFullPermission"}},

            new ApiResource("ResourceNotification") {Scopes = {"NotificationFullPermission"}},

            new ApiResource("ResourceOcelot") {Scopes = {"OcelotFullPermission"}},
            

            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {     
            //Token icinde olacak bilgiler

            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            // Şu yetkiye sahip olan sunlari yapabilir

            //Catalog Mikroservisi
            new ApiScope("CatalogFullPermission","Full authority for catalog operations"), 
            new ApiScope("CatalogReadPermission","Reading authority for catalog operations"),

            //Discount
            new ApiScope("DiscountFullPermission","Full authority for discount operations"),

            //Order
            new ApiScope("OrderFullPermission","Full authority for order operations"),

            //Cargo
            new ApiScope("CargoFullPermission","Full authority for cargo operations"),

            //Basket
            new ApiScope("BasketFullPermission","Full authority for basket operations"),

            //Comment
            new ApiScope("CommentFullPermission","Full authority for comment operations"),

            //Payment
            new ApiScope("PaymentFullPermission","Full authority for payment operations"),       

            //Image
            new ApiScope("ImageFullPermission","Full authority for image operations"),

            //Message
            new ApiScope("MessageFullPermission","Full authority for message operations"),

            //Notification
            new ApiScope("NotificationFullPermission","Full authority for notification operations"),

            //Ocelot
            new ApiScope("OcelotFullPermission","Full authority for ocelot operations"),

            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)  // Yerel Api Erisim Izni
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            //Visitor
            new Client
            {
                ClientId = "MultiShopVisitorId",
                ClientName = "MultiShopVisitorUser",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("multishopsecret".Sha256())}, 
                AllowedScopes = { "CatalogFullPermission","CatalogReadPermission", "OcelotFullPermission", "CommentFullPermission", "NotificationFullPermission", IdentityServerConstants.LocalApi.ScopeName }
            },

            //Manager
            new Client
            {
                ClientId = "MultiShopManagerId",
                ClientName = "MultiShopManagerUser",
                AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission", "BasketFullPermission", "OcelotFullPermission", "CommentFullPermission", "PaymentFullPermission", "ImageFullPermission","DiscountFullPermission","OrderFullPermission","MessageFullPermission","CargoFullPermission","NotificationFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile }
            },

            //Admin
            new Client
            {
                ClientId = "MultiShopAdminId",
                ClientName = "MultiShopAdminUser",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission", "DiscountFullPermission", "OrderFullPermission","CargoFullPermission","BasketFullPermission","OcelotFullPermission","CommentFullPermission","PaymentFullPermission","ImageFullPermission","MessageFullPermission","NotificationFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime = 600
            }
        };

    }
}