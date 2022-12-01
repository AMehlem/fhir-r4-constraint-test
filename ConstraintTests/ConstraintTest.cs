using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Validation;

namespace ConstraintTests
{
    [TestClass]
    public class ConstraintTest
    {
        private const string BundleFilePath = "Resources/Bundle.xml";
        private const string StructureDefinitionsDirectory = "Resources/StructureDefinitions";

        [TestMethod]
        public async Task ConstraintValidationWorks()
        {
            // Arrange:
            var parser = new FhirXmlParser();
            var source = new DirectorySource(StructureDefinitionsDirectory, new DirectorySourceSettings { IncludeSubDirectories = true });
            var resourceResolver = new CachedResolver(source);
            var terminologyService = new LocalTerminologyService(resourceResolver);
            var settings = new ValidationSettings
            {
                GenerateSnapshot = true,
                ResourceResolver = resourceResolver,
                TerminologyService = terminologyService,
            };
            var validator = new Validator(settings);
            var xml = await File.ReadAllTextAsync(BundleFilePath);
            var parsedEntity = parser.Parse<Hl7.Fhir.Model.Bundle>(xml);

            // Act:
            var validationResult = validator.Validate(parsedEntity);

            // Assert:
            Assert.IsTrue(validationResult.Success);
        }
    }
}