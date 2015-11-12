using System.Diagnostics.CodeAnalysis;

namespace TSD.Reference.API.Areas.HelpPage.ModelDescriptions
{
	[ExcludeFromCodeCoverage]
	public class CollectionModelDescription : ModelDescription
    {
        public ModelDescription ElementDescription { get; set; }
    }
}