
namespace Domain.Services
{
	/// <summary>
	/// Алгоритм сокращения ссылки.
	/// </summary>
    public interface IShortLinkGenerationAlgorithm
    {
		/// <summary>
		/// Название алгоритма.
		/// </summary>
		/// <remarks>
		/// Идентифицирует применённый алгоритм. Это имя может быть полезно указывать в <see cref="Domain.Models.Link"/>.
		/// </remarks>
		string AlgorithmName { get; }

		/// <summary>
		/// Алгоритм сокращения.
		/// </summary>
		/// <param name="url">Строка, содержащая url для сокращения.</param>
		/// <returns>Сокращенная стока - код ссылки.</returns>
		string Shorten(string url);
	}

	
}
