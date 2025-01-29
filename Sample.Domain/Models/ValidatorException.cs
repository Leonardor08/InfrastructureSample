using System.Runtime.Serialization;

namespace Sample.Domain.Models;

	[Serializable]
	public class ValidatorException : Exception
	{
		public IEnumerable<Error>? Errors { get; private set; }

		public ValidatorException(string code, string message)
		{
			this.Errors = new List<Error>
		{
			{
				new Error
				{
					Code= code,
					Message = message
				}
			}
		};
		}

		public ValidatorException(IEnumerable<Error> errors)
		{
			this.Errors = errors;
		}

		public ValidatorException()
		{
		}

		public ValidatorException(string message)
			: base(message)
		{
			this.Errors = new List<Error>
		{
			{
				new Error
				{
					Code= "00",
					Message = message
				}
			}
		};
		}

		public ValidatorException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected ValidatorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

