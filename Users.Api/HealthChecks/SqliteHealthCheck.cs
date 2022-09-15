using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Users.Api.HealthChecks
{
	public class SqliteHealthCheck : IHealthCheck
	{
		private readonly string _connectionString;

		public SqliteHealthCheck(string connectionString)
		{
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
		}

		public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
		{
			try
			{
				using (var conn = new SqliteConnection(_connectionString))
				{
					await conn.OpenAsync(cancellationToken);
					await conn.CloseAsync();
					return HealthCheckResult.Healthy();
				}
			}
			catch (Exception ex)
			{
				return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
			}
		}
	}
}
