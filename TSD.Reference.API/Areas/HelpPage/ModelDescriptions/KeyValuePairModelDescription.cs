using System.Diagnostics.CodeAnalysis;

namespace TSD.Reference.API.Areas.HelpPage.ModelDescriptions
{
	[ExcludeFromCodeCoverage]
	public class KeyValuePairModelDescription : ModelDescription
    {
        public ModelDescription KeyModelDescription { get; set; }

        public ModelDescription ValueModelDescription { get; set; }
    }
}