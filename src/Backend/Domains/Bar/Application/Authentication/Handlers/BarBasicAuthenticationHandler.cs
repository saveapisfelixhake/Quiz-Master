using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Backend.Domains.Bar.Domain.Options.Authentication;
using Backend.Domains.Common.Infrastructure.Constants;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend.Domains.Bar.Application.Authentication.Handlers;

public class BarBasicAuthenticationHandler(
    IOptionsMonitor<BarBasicAuthenticationHandlerOption> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IDbContextFactory<DataContext> factory
) : AuthenticationHandler<BarBasicAuthenticationHandlerOption>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var values = Request.Headers.Authorization;
        if (!AuthenticationHeaderValue.TryParse(values, out var value) ||
            !value.Scheme.Equals(AuthenticationSchemes.BarBasicAuth))
        {
            return AuthenticateResult.NoResult();
        }

        var parameter = value.Parameter;
        if (string.IsNullOrWhiteSpace(parameter))
        {
            return AuthenticateResult.Fail("Failed to authenticate bar. Invalid authentication parameter");
        }

        var parameterString = Encoding.UTF8.GetString(Convert.FromBase64String(parameter));

        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var bar = await context.Bars
            .SingleOrDefaultAsync(entity => entity.Id == Guid.Parse(parameterString))
            .ConfigureAwait(false);
        if (bar == null)
        {
            return AuthenticateResult.Fail("Failed to authenticate bar. Bar not found");
        }

        if (!bar.IsActive)
        {
            return AuthenticateResult.Fail("Failed to authenticate bar. Bar is not active");
        }

        var identity = new ClaimsIdentity([], AuthenticationSchemes.BarBasicAuth);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationSchemes.BarBasicAuth);

        return AuthenticateResult.Success(ticket);
    }
}