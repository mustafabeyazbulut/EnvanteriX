﻿using FluentValidation;
using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;
using EnvanteriX.Application.Behaviors;

namespace EnvanteriX.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddTransient<ExceptionMiddleware>();

            // BaseRules, tüm rule'ların base class'ıdır. Tüm rule'lar BaseRules'dan türemelidir.
            services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules)); // tüm rule'ları register etmek için BaseRules'dan türeyenleri alıyoruz

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // FluentValidation, .NET platformu için bir doğrulama kütüphanesidir. 
            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        }
        // tüm rule'ları register etmek için
        private static IServiceCollection AddRulesFromAssemblyContaining(
          this IServiceCollection services,
          Assembly assembly,
          Type type)
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types)
                services.AddTransient(item);

            return services;
        }
    }
}
