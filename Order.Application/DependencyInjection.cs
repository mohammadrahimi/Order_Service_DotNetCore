

using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.UseCase.Order.Commands.Create;
using Order.Domain.Contract.Commands.Order.Create;
using Order.Framework.Core.Bus;

namespace Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<ICommadnHandler<CreateOrderCommand, CreateOrderCommandResult>, CreateOrderCommandHandler>();
        services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
        
        return services;
    }
}
